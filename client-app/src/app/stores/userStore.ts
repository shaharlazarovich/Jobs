import { observable, computed, action, runInAction } from "mobx";
import { IUser, IUserFormValues } from "../models/users";
import agent from "../api/agentBase";
import { RootStore } from './rootStore';
import { history } from '../common/util/history';
//import { history } from '../../'


export default class UserStore {
    rootStore: RootStore;
    constructor(rootStore:RootStore) {
        this.rootStore = rootStore;
    }

    @observable user: IUser | null = null;

    @computed get isLoggedIn() {
        return !!this.user
    }

    @action login = async (values: IUserFormValues) => {
        try {
            const user = await agent.User.login(values);
            runInAction(() => { //runInaction is needed, since lines of code after the await are considered "out of action"
                this.user = user;    
            })
            this.rootStore.commonStore.setToken(user.token);
            this.rootStore.modalStore.closeModal();
            history.push('/jobs');
        } catch (error) {
            throw(error);
        }
    }

    @action register = async (values: IUserFormValues) => {
        try {
            const user = await agent.User.register(values);
            this.rootStore.commonStore.setToken(user.token);
            this.rootStore.modalStore.closeModal();
            history.push('/jobs');
        } catch (error) {
            throw(error);
        }
    }

    @action getUser = async () => {
        try {
            const user = await agent.User.current();
            runInAction(() => {
                this.user = user;
            })
        } catch (error) {
            console.log(error);    
        }
    }

    @action logout = () => {
        this.rootStore.commonStore.setToken(null);
        this.user = null;
        history.push('/');
    }
}