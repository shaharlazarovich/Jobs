//we are adding this interface to mimic what we do on the server for paging -
//which is to have an entity around our activity array, in order to add a count
//of activities - which then, we could device in the number of pages
export interface IJobsEnvelope {
    jobs: IJob[];
    jobCount: number
}

export interface IJob {
    id: string;
    jobName: string;
    company: string;
    replication: string;
    servers: string;
    lastRun: Date;
    date: Date;
    time: Date;
    rta: number;
    results: string;
    key: string;
    rtoNeeded: number
}


export interface IJobFormValues extends Partial<IJob> {
     
}

export class JobFormValues implements IJobFormValues {
    id?: string = undefined;
    jobName: string = '';
    company: string = '';
    replication: string = '';
    servers: string = '';
    lastRun?:Date = undefined;
    date?:Date = undefined;
    time?:Date = undefined;
    rta?: number = 0;
    results: string = '';
    key: string = '';
    rtoNeeded: number = 0;

    constructor(init?: IJobFormValues) {
        Object.assign(this, init); //this will initialize our object of class JobFormValues
                                  // with the above values
    }
}