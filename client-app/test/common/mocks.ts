import { IJob } from "../../src/app/models/job";
import { IProfile } from "../../src/app/models/profile";
import { routerTestProps } from "./routeUtil";

export const jobMock: IJob = { 
    id: 1, 
    replication:"VMware SRM",
    jobName:"test1",
    company: 'Shomera',  
    lastRun: new Date('2020-01-06 23:00:05'), 
    date: new Date('2020-01-06 23:00:05'),  
    time: new Date('2020-01-06 23:00:05'),  
    servers: '10',
    results: 'OK',
    key: 'AAAA-BBBB-CCCC-DDDD',
    rtoNeeded: "10",
    jobIP: "localhost",
    rta: "10"
}

export const profileMock : IProfile = {
    displayName: "fakeName",
    userName: "fakeUserName",
    bio: "fakeBIO",
    image: "",
    following: true,
    followersCount: 1,
    followingCount: 1,
    photos: null
}

export const { history, location, match } = routerTestProps('/route/:id', { id: 'cd3da3fb-4f8b-4b40-a814-1b5839f8069f' });