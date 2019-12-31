import {observable, action, computed, runInAction, reaction, toJS} from 'mobx'
import { SyntheticEvent } from 'react'
import { IActivity } from '../models/activity';
import agent from '../api/agent';
import { history} from '../../'
import { toast } from 'react-toastify';
import { RootStore } from './rootStore';
import { setActivityProps, createAttendee } from '../common/util/util';
import {HubConnection, HubConnectionBuilder, LogLevel} from '@microsoft/signalr'

const LIMIT = 2;
//to avoid error about decorators (the @ sign before the observable)
//being expermintal - add this line to the tsconfig.json
//"experimentalDecorators": true
export default class ActivityStore {
    rootStore: RootStore;
    constructor(rootStore:RootStore) {
        this.rootStore = rootStore;

        reaction( //we're adding this reaction so we'll do something whenever one of the keys
                  //in the querystring changes
            () => this.predicate.keys(),
            () => {
              this.page = 0;
              this.activityRegistry.clear();
              this.loadActivities();
            }
        )
    }
    @observable activityRegistry = new Map(); //this is called an observable map - which has more functionality than a simple array
   // @observable activities: IActivity[] = [];
    @observable activity: IActivity | null = null; //the | operator indicates a unition type - either IActivity or null
    @observable loadingInitial = false;
   // @observable editMode = false;
    @observable submitting = false;
    @observable target = '';
    @observable loading = false;
    @observable.ref hubConnection: HubConnection | null = null;
    @observable activityCount = 0;
    @observable page = 0;
    @observable predicate = new Map(); //this will be used as configuration to do our filters 
                                //(we have several such as filter by date, by isGoing, by isHost etc.)

    @action setPredicate = (predicate: string, value: string | Date) => {
        this.predicate.clear();
        if (predicate !== 'all') {
            this.predicate.set(predicate, value);
        }
    }
    
    @computed get axiosParams() {
      const params = new URLSearchParams();
      params.append('limit', String(LIMIT));
      params.append('offset', `${this.page ? this.page * LIMIT : 0}`);
      this.predicate.forEach((value, key) => {
        if (key === 'startDate') {
          params.append(key, value.toISOString())
        } else {
          params.append(key, value)
        }
      })
      return params;
    }
    
    @computed get totalPages() {
        return Math.ceil(this.activityCount / LIMIT);
    }

    @action setPage = (page: number) => {
        this.page = page;
    }

    @action createHubConnection = (activityId: string) => {
        this.hubConnection = new HubConnectionBuilder()
            //.withUrl('http://localhost:5000/chat', {
                .withUrl(process.env.REACT_APP_API_CHAT_URL!, {
                accessTokenFactory: () => this.rootStore.commonStore.token!
            })    
            .configureLogging(LogLevel.Information)
            .build();

        this.hubConnection
            .start()
            .then(() => console.log(this.hubConnection!.state))
            .then(() => this.hubConnection!.invoke('AddToGroup', activityId))
            .catch(error => console.log('Error establishing connection: ', error));  
        
        this.hubConnection.on('ReceiveComment', comment => {
            runInAction(() => {
                this.activity!.comments.push(comment);
            })
        })  
    };

    @action stopHubConnection = () => {
        this.hubConnection!.invoke('RemoveFromGroup', this.activity!.id)
            .then(() => {
                this.hubConnection!.stop();
            })
            .then(() => console.log('Connection stopped'))
            .catch(err => console.log(err))
    }
    
    //since axios is used for rest http calls only - this is the way we communicate with
    //the server using signalR
    @action addComment = async (values: any) => {
        values.activityid = this.activity!.id; //values.activityid needs to match the name of activityid params in the create.cs class on the server
        try {
            await this.hubConnection!.invoke('SendComment', values); //the 'SendComment' string needs to match exactly the name of the method on the ChatHub.cs class on the server
        } catch (error) {
            console.log(error);
        }

    }

    @computed get activitiesByDate() {
        //return this.activities.sort( //this is before the observable map refactroing
        /*return Array.from(this.activityRegistry.values()).sort( //this is before the styling refactoring
            (a, b) => Date.parse(a.date) - Date.parse(b.date)*/
        return this.groupActivitiesByDate(Array.from(this.activityRegistry.values()));
    }

    groupActivitiesByDate(activities: IActivity[]) {
        const sortedActivities = activities.sort(
            (a, b) => a.date.getTime() - b.date.getTime()
        )
        return Object.entries(sortedActivities.reduce((activities, activity) => {
            const date = activity.date.toISOString().split('T')[0];
            activities[date] = activities[date] ? [...activities[date], activity] : [activity];
            return activities;
        }, {} as {[key: string]: IActivity[]}));
    }

    /*loadActivities is async method which returns a promise

        the below is an example of a "promise chain" , where we
        wait on the code in the .then block until the promise
        is fullfilled. we will comment it out, and replace it
        with async-await syntax which does the same
        
        @action loadActivites = () => {
            this.loadingInitial = true;
            agent.Activities.list()
            .then(activities => {
              activities.forEach(activity => {
                activity.date = activity.date.split('.')[0];
                this.activities.push(activity);
              })
            })
            .catch(error => console.log(error))
            .finally(() => this.loadingInitial = false);
        }*/
    
    @action loadActivities = async () => {
        this.loadingInitial = true;
        try {
            const activitiesEnvelope = await agent.Activities.list(this.axiosParams);
            const {activities, activityCount} = activitiesEnvelope;
            runInAction('loading activities', () => { //we use runInaction because everything after the await is considered out of the action - so strict mode fails us
                activities.forEach(activity => {
                  setActivityProps(activity, this.rootStore.userStore.user!);//since user could be null we'll use exclamation mark to allow using it
                  //this.activities.push(activity);//this is before the observable map refactroing
                  this.activityRegistry.set(activity.id,activity);
                })
                this.activityCount = activityCount;
                this.loadingInitial = false;
            })
        } catch(error) {
            runInAction('load activities error', () => {
                this.loadingInitial = false;
            })
            console.log(error);   
        }
    }

    @action loadActivity = async (id: string) => {
        let activity = this.getActivity(id);
        if (activity) {
            this.activity = activity;
            return toJS(activity); //this is an edge case where we return a mobx observable
                                   //and send it to the activity form - which makes it be "out of action"
                                   //in all other cases, we simply set the activity to this.activity - 
                                   //which in this case, we'd need it to be an observable - but here,
                                   //we need to turn it into simple js object and send it over (just like
                                   //the below else statement is returning a simple js object from axios
                                   //and returning it) so we'll use "toJS" method to turn it into JS object
        } else {
            this.loadingInitial = true;
            try {
                activity = await agent.Activities.details(id);
                runInAction('getting activity', ()=> {
                    setActivityProps(activity, this.rootStore.userStore.user!);//since user could be null we'll use exclamation mark to allow using it
                    this.activity = activity;
                    this.activityRegistry.set(activity.id,activity);//we do this in order to load the acivity from memory instead of going out to the server
                    this.loadingInitial = false;
                })
                return activity;
            } catch(error) {
                runInAction('get activity error', ()=> {
                    this.loadingInitial = false;
                })
                console.log(error);
            }
            
        }
    }

    getActivity = (id: string) => {
        return this.activityRegistry.get(id); 
        //a small helper method - no need to declare it as action since we're not mutating state
    }

    @action clearActivity = () => {
        this.activity = null;
    }

    @action createActivity = async (activity: IActivity) => {
        this.submitting = true;
        try {
            await agent.Activities.create(activity);
            // the below part (until the runInAction is basically updating our local
            //state with the attendance - having the current user as the host, since 
            //when we create an activity - we don't get the response from the server.
            //and also - since we don't update any observables here, we don't need to
            //run it inside an action)
            const attendee = createAttendee(this.rootStore.userStore.user!);
            attendee.isHost = true;
            let attendees = [];
            attendees.push(attendee);
            activity.attendees = attendees;
            activity.comments = [];
            activity.isHost = true;
            runInAction('creating activity', () => {
                //this.activities.push(activity);//this is before the observable map refactroing
                this.activityRegistry.set(activity.id,activity);
                //this.editMode = false;
                this.submitting = false;
            })
            history.push(`/activities/${activity.id}`)
        } catch (error) {
            runInAction('create activity error', () => {
                this.submitting = false;
            }) 
            toast.error('problem submitting data')
            console.log(error.response);
        }
    };

    @action editActivity = async (activity: IActivity) => {
        this.submitting = true;
        try {
            await agent.Activities.update(activity);
            runInAction('updating activity', () => {
                this.activityRegistry.set(activity.id,activity);
                this.activity = activity;
                //this.editMode = false;
                this.submitting = false;
                history.push(`/activities/${activity.id}`)
            })
        } catch (error) {
            runInAction('update activity error', () => {
                this.submitting = false;
            })
            toast.error('problem submitting data')
            console.log(error.response);
        }
    }

    @action deleteActivity = async(event: SyntheticEvent<HTMLButtonElement>,id: string) => {
        this.submitting = true;
        this.target = event.currentTarget.name;
        try{
            await agent.Activities.delete(id);
            runInAction('deleting activity', () => {
                this.activityRegistry.delete(id);
                this.submitting = false;
                this.target = '';
            })
        } catch (error) {
            runInAction('delete activity error', () => {
                this.submitting = false;
                this.target = '';
            })
            console.log(error);
        }
    }

    @action attendActivity = async () => {
        const attendee = createAttendee(this.rootStore.userStore.user!);
        this.loading = true;
        try {
            await agent.Activities.attend(this.activity!.id);
            runInAction(() => {
                if (this.activity) {
                    this.activity.attendees.push(attendee);
                    this.activity.isGoing = true;
                    this.activityRegistry.set(this.activity.id, this.activity);
                    this.loading = false;
                }
            })
        } catch (error) {
            runInAction(() => {
                this.loading = false;
            })
            toast.error('Problem signing up to activity');
        }
    }

    @action cancelAttendance = async () => {
        this.loading = true;
        try {
            await agent.Activities.unattend(this.activity!.id);
            runInAction(() => {
                if (this.activity) {
                    this.activity.attendees = this.activity.attendees.filter(
                        a => a.userName !== this.rootStore.userStore.user!.username
                    ); //this will filter out of the attendees the current loggedin user
                    this.activity.isGoing = false;
                    this.activityRegistry.set(this.activity.id, this.activity);
                    this.loading = false;
                }
            })
        } catch (error) {
            runInAction(() => {
                this.loading = false;
            })
            toast.error('Problem cancelling activity');
        }

    }

   /*
    // @action openCreateForm = () => {
        //this.editMode = true;
        //this.activity = null;
    //}

    //@action openEditForm = (id: string) => {
        //this.activity = this.activityRegistry.get(id);
        //this.editMode = true;
    //}

    //@action cancelSelectedActivity = () => {
    //    this.activity = null;
    //}

    //@action cancelFormOpen = () => {
    //    this.editMode = false;
    //}

    //@action selectActivity = (id: string) => {
        //this is before the observable map refactroing
        //this.selectedActivity = this.activities.find(a => a.id  === id); //the === operator checks value and type whereas == only checks value
        //this.activity = this.activityRegistry.get(id);
        //this.editMode = false;
    //}
    */
}
