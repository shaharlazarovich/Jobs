//we are adding this interface to mimic what we do on the server for paging -
//which is to have an entity around our jobs array, in order to add a count
//of jobs - which then, we could device in the number of pages
export interface IJobsEnvelope {
    jobs: IJob[];
    jobsCount: number
}

export interface IJob {
    id: number;
    jobName: string;
    company: string;
    replication: string;
    servers: string;
    lastRun: Date;
    date: Date;
    time: Date;
    rta: string;
    results: string;
    key: string;
    rtoNeeded: string;
    jobIP: string;
}


export interface IJobFormValues extends Partial<IJob> {
     
}

export class JobFormValues implements IJobFormValues {
    id?: number = 0;
    jobName: string = '';
    company: string = '';
    replication: string = '';
    servers: string = '';
    lastRun?:Date = undefined;
    date?:Date = undefined;
    time?:Date = undefined;
    rta?: string = '';
    results: string = '';
    key: string = '';
    rtoNeeded: string = '';
    jobIP: string = '';

    constructor(init?: IJobFormValues) {
        Object.assign(this, init); //this will initialize our object of class JobFormValues
                                  // with the above values
    }
}