import { IJob } from "../../models/job";
import { IUser } from "../../models/users";

export const combineDateAndTime = (date: Date, time: Date) => {
    var dateString,timeString;
    var dateToReturn = new Date();
    if (typeof(date)!= 'undefined' && typeof(time)!='undefined'){
        dateString = date.toISOString().split('T')[0];
        timeString = time.toISOString().split('T')[1];
        dateToReturn = new Date(dateString + 'T' + timeString);
    }
    
    return dateToReturn;
}

export const setJobProps = (job: IJob, user: IUser) => {
    job.lastRun = new Date(job.lastRun);   
    return job;
}