import { IActivity, IAttendee } from "../../models/activity";
import { IJob } from "../../models/job";
import { IUser } from "../../models/users";

export const combineDateAndTime = (date: Date, time: Date) => {

    //we'll comment out the below standard way to concatenate date & time
    //since it only works on chrome - not on safari, so we'll use a more
    //generic way with IsoString

    // const timeString = time.getHours() + ':' + time.getMinutes() + ':00';
    // const year = date.getFullYear();
    // const month = date.getMonth() + 1;
    // const day = date.getDate();
    // const dateString = `${year}-${month}-${day}`;

    const dateString = date.toISOString().split('T')[0];
    const timeString = time.toISOString().split('T')[1];

    return new Date(dateString + 'T' + timeString);
}

export const setActivityProps = (activity: IActivity, user: IUser) => {
    activity.date = new Date(activity.date);
    activity.isGoing = activity.attendees.some( //the some method returns true if our user appears within the attendees list
        a => a.userName === user.username 
    );
    activity.isHost = activity.attendees.some( //the some method returns true if our user appears within the attendees list
      a => a.userName === user.username && a.isHost
    );
    return activity;
}

export const setJobProps = (job: IJob, user: IUser) => {
    job.lastRun = new Date(job.lastRun);   
    return job;
}

export const createAttendee = (user: IUser) : IAttendee => {
    return {
        displayName: user.displayName,
        isHost: false,
        userName: user.username,
        image: user.image!
    }
}