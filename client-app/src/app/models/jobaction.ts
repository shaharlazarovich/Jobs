//we are adding this interface to mimic what we do on the server for paging -
//which is to have an entity around our jobs array, in order to add a count
//of jobs - which then, we could device in the number of pages
export interface IJobActionsEnvelope {
    jobActions: IJobAction[];
    jobActionsCount: number
}

export interface IJobAction {
    id: string;
    jobId: string;
    jobName: string;
    userId: string;
    actionDate: Date;
    remoteIP: string;
    remoteResponse: string;
    requestProperties: string;
    source: string;
    action: string;
}

export class JobAction implements IJobAction {
    id: string = '';
    jobId: string = '';
    jobName: string = '';
    userId: string = '';
    actionDate: Date = new Date();
    remoteIP: string = '';
    remoteResponse: string = '';
    requestProperties: string = '';
    source: string = '';
    action: string = '';

    constructor(init?: IJobAction) {
        Object.assign(this, init); //this will initialize our object of class JobActions
                                  // with the above values
    }
}