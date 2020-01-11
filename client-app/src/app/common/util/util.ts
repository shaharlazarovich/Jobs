import { IJob } from "../../models/job";
import { IUser } from "../../models/users";

export const combineDateAndTime = (date: Date, time: Date) => {

    const dateString = date.toISOString().split('T')[0];
    const timeString = time.toISOString().split('T')[1];

    return new Date(dateString + 'T' + timeString);
}

export const setJobProps = (job: IJob, user: IUser) => {
    job.lastRun = new Date(job.lastRun);   
    return job;
}