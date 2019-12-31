export interface IAttendee {
    userName: string;
    displayName: string;
    image: string;
    isHost: boolean;
    following?: boolean,
}

//we are adding this interface to mimic what we do on the server for paging -
//which is to have an entity around our activity array, in order to add a count
//of activities - which then, we could device in the number of pages
export interface IActivitiesEnvelope {
    activities: IActivity[];
    activityCount: number
}

export interface IActivity {
    id: string;
    title: string;
    description: string;
    category: string;
    date: Date;
    city: string;
    venue: string;
    isGoing: boolean;
    isHost: boolean;
    attendees: IAttendee[];
    comments: IComment[];
}

export interface IComment {
    id: string;
    createdAt: Date;
    body: string;
    username: string;
    displayName: string;
    image: string;
}

export interface IActivityFormValues extends Partial<IActivity> {
    time?: Date    
}

export class ActivityFormValues implements IActivityFormValues {
    id?: string = undefined;
    title: string = '';
    category: string = '';
    description: string = '';
    date?:Date = undefined;
    time?: Date = undefined;
    city: string = '';
    venue: string = '';

    constructor(init?: IActivityFormValues) {
        if (init && init.date) {
            init.time = init.date;
        }
        Object.assign(this, init); //this will initialize our object of class ActivityFormValues
                                  // with the above values
    }
}