import ActivityStore from "./activityStore";
import UserStore from "./userStore";
import CommonStore from "./commonStore";
import { createContext } from "react";
import { configure } from "mobx";
import ModalStore from "./modalStore";
import ProfileStore from "./profileStore";
import JobStore from "./jobStore";

//this enables "strict mode" on every mobx object, and enforces it to run
//within an action - so we should use the "runInAction" method for that
configure({enforceActions: 'always'});

export class RootStore {
    activityStore: ActivityStore;
    jobStore: JobStore;
    userStore: UserStore;
    commonStore: CommonStore;
    modalStore: ModalStore;
    profileStore: ProfileStore;

    constructor() {
        this.activityStore = new ActivityStore(this);
        this.userStore = new UserStore(this);
        this.commonStore = new CommonStore(this);
        this.modalStore = new ModalStore(this);
        this.profileStore = new ProfileStore(this);
        this.jobStore = new JobStore(this);
    }
}

export const RootStoreContext = createContext(new RootStore());