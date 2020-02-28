import {observable, action, computed, runInAction, reaction, toJS} from 'mobx'
import { SyntheticEvent } from 'react'
import { IJob } from '../models/job';
import agent from '../api/agent';
import { toast } from 'react-toastify';
import { RootStore } from './rootStore';
import { history } from '../common/util/history';
import { setJobProps } from '../common/util/util';
import { TrackEvent } from '../models/trackevent';

const LIMIT = 6; //this is the amount of records per page for the paging methods

//to avoid error about decorators (the @ sign before the observable)
//being expermintal - add this line to the tsconfig.json
//"experimentalDecorators": true
export default class JobStore {
    rootStore: RootStore;
    constructor(rootStore:RootStore) {
        this.rootStore = rootStore;

        reaction( //we're adding this reaction so we'll do something whenever one of the keys
                  //in the querystring changes
            () => this.predicate.keys(),
            () => {
              this.page = 0;
              this.jobRegistry.clear();
              this.loadJobs();
            }
        )
    }
    @observable jobRegistry = new Map(); //this is called an observable map - which has more functionality than a simple array
    @observable job: IJob | null = null; //the | operator indicates a unition type - either IActivity or null
    @observable loadingInitial = false;
   // @observable editMode = false;
    @observable submitting = false;
    @observable target = '';
    @observable loading = false;
    @observable jobCount = 0;
    @observable page = 0;
    @observable predicate = new Map(); //this will be used as configuration to do our filters (we have several such as filter by date)
    
    @observable remoteRunning = false; //handles loading of remote run button

    @action runRemoteJob = async (trackevent: TrackEvent) => {
        this.remoteRunning = true;
        try {
            await agent.RemoteJob.run(trackevent);
            runInAction('running job', () => {
                this.remoteRunning = false;
            })
            toast.info('run job request sent')
        }
        catch(error) {
            runInAction('run job error', () => {
                this.remoteRunning = false;
            }) 
            toast.error('problem running job')
            console.log(error.response); 
        }
    }

    @action setPredicate = (predicate: string, value: string | Date) => {
        this.predicate.clear();
        if (predicate !== 'all') {
            this.predicate.set(predicate, value);
        }
    }
    
    //this method handles querystring params - which are passed as state
    //for example - how many recrods to show on a page with paging, what is the 
    //offset to begin the paging from, or handling the date format
    @computed get axiosParams() {
      const params = new URLSearchParams();
      params.append('limit', String(LIMIT));
      params.append('offset', `${this.page ? this.page * LIMIT : 0}`);
      this.predicate.forEach((value, key) => {
        if (key === 'lastRun') {
          params.append(key, value.toISOString())
        } else {
          params.append(key, value)
        }
      })
      return params;
    }
    
    @computed get totalPages() {
        return Math.ceil(this.jobCount / LIMIT);
    }

    @action setPage = (page: number) => {
        this.page = page;
    }

    
    @computed get jobsByDate() {
        return this.groupJobsByDate(Array.from(this.jobRegistry.values()));
    }

    groupJobsByDate(jobs: IJob[]) {
        const sortedJobs = jobs.sort(
            (a, b) => a.lastRun.getTime() - b.lastRun.getTime()
        )
        return Object.entries(sortedJobs.reduce((jobs, job) => {
            const date = job.lastRun.toISOString().split('T')[0];
            jobs[date] = jobs[date] ? [...jobs[date], job] : [job];
            return jobs;
        }, {} as {[key: string]: IJob[]}));
    }

    @action loadJobs = async () => {
        this.loadingInitial = true;
        try {
            const jobsEnvelope = await agent.Jobs.list(this.axiosParams);
            const {jobs, jobsCount} = jobsEnvelope;
            runInAction('loading jobs', () => { //we use runInaction because everything after the await is considered out of the action - so strict mode fails us
                jobs.forEach(job => {
                  setJobProps(job, this.rootStore.userStore.user!);//since user could be null we'll use exclamation mark to allow using it
                  this.jobRegistry.set(job.id,job);
                })
                this.jobCount = jobsCount;
                this.loadingInitial = false;
            })
        } catch(error) {
            runInAction('load jobs error', () => {
                this.loadingInitial = false;
            })
            console.log(error);   
        }
    }

    @action loadJob = async (id: string) => {
        let job = this.getJob(id);
        if (job) {
            this.job = job;
            return toJS(job); //this is an edge case where we return a mobx observable
                                   //and send it to the job form - which makes it be "out of action"
                                   //in all other cases, we simply set the job to this.job - 
                                   //which in this case, we'd need it to be an observable - but here,
                                   //we need to turn it into simple js object and send it over (just like
                                   //the below else statement is returning a simple js object from axios
                                   //and returning it) so we'll use "toJS" method to turn it into JS object
        } else {
            this.loadingInitial = true;
            try {
                job = await agent.Jobs.details(id);
                runInAction('getting jobs', ()=> {
                    setJobProps(job, this.rootStore.userStore.user!);//since user could be null we'll use exclamation mark to allow using it
                    this.job = job;
                    this.jobRegistry.set(job.id,job);//we do this in order to load the job from memory instead of going out to the server
                    this.loadingInitial = false;
                })
                return job;
            } catch(error) {
                runInAction('get job error', ()=> {
                    this.loadingInitial = false;
                })
                console.log(error);
            }
            
        }
    }

    getJob = (id: string) => {
        return this.jobRegistry.get(id); 
        //a small helper method - no need to declare it as action since we're not mutating state
    }

    @action clearJob = () => {
        this.job = null;
    }

    @action createJob = async (job: IJob) => {
        this.submitting = true;
        try {
            await agent.Jobs.create(job);
            // the below part (until the runInAction is basically updating our local
            //state with the attendance - having the current user as the host, since 
            //when we create a job - we don't get the response from the server.
            //and also - since we don't update any observables here, we don't need to
            //run it inside an action)
            runInAction('creating job', () => {
                this.jobRegistry.set(job.id,job);
                this.submitting = false;
            })
            history.push(`/jobs/${job.id}`)
        } catch (error) {
            runInAction('create job error', () => {
                this.submitting = false;
            }) 
            toast.error('problem submitting data')
            console.log(error.response);
        }
    };

    @action editJob = async (job: IJob) => {
        this.submitting = true;
        try {
            await agent.Jobs.update(job);
            runInAction('updating job', () => {
                this.jobRegistry.set(job.id,job);
                this.job = job;
                this.submitting = false;
                history.push(`/jobs/${job.id}`)
            })
        } catch (error) {
            runInAction('update job error', () => {
                this.submitting = false;
            })
            toast.error('problem submitting data')
            console.log(error.response);
        }
    }

    @action deleteJob = async(event: SyntheticEvent<HTMLButtonElement>,id: string) => {
        this.submitting = true;
        this.target = event.currentTarget.name;
        try{
            await agent.Jobs.delete(id);
            runInAction('deleting job', () => {
                this.jobRegistry.delete(id);
                this.submitting = false;
                this.target = '';
            })
        } catch (error) {
            runInAction('delete job error', () => {
                this.submitting = false;
                this.target = '';
            })
            console.log(error);
        }
    }

}
