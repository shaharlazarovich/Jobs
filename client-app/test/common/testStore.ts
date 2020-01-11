import { createContext } from "react";
import { configure } from "mobx";

//this enables "strict mode" on every mobx object, and enforces it to run
//within an action - so we should use the "runInAction" method for that
configure({enforceActions: 'observed'});

export class TestStore {

    constructor() {
    }

    dispatch(jobName:any){
        return jobName; 
    }

    getState(){}
}

export const TestStoreContext = createContext(new TestStore());