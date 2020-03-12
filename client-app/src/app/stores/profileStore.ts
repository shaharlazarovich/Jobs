import {RootStore} from './rootStore'
import { observable, runInAction, action, computed, reaction } from 'mobx';
import agent from '../api/agentBase';
import { IProfile } from '../models/profile';
import { toast } from 'react-toastify';

export default class ProfileStore {
    rootStore: RootStore;

    constructor(rootStore: RootStore) {
        this.rootStore = rootStore;
        //with reaction mobx can listen and react to the tab changing in the profile
        reaction(
            () => this.activeTab,
            activeTab => {}
        )
    }

    @observable profile: IProfile | null = null;
    @observable loadingProfile = true;
    @observable loading = false;
    @observable activeTab: number = 0;
    @observable loadingActivities = false;
    
    @computed get isCurrentUser() {
        if (this.rootStore.userStore.user && this.profile) {
            return this.rootStore.userStore.user.username === this.profile.userName;
        } else {
            return false;
        }           
    }

    @action setActiveTab = (activeIndex: number) => {
        this.activeTab = activeIndex;
    }

    @action loadProfile = async (username: string) => {
        this.loadingProfile = true;
        try {
            const profile = await agent.Profiles.get(username);
            runInAction(() => {
                this.profile = profile;
                this.loadingProfile = false;
            })
        } catch (error) {
            runInAction(() => {
                this.loadingProfile = false;
            })
            console.log(error);
        }
    }

    
    
    @action updateProfile = async (profile: Partial<IProfile>) => {
        try{
            await agent.Profiles.updateProfile(profile);
            runInAction(() => {
                if (profile.displayName !== this.rootStore.userStore.user!.displayName) {
                    this.rootStore.userStore.user!.displayName = profile.displayName!;
                }
                this.profile = { ...this.profile!, ...profile}
            })
        } catch (error) {
            toast.error('Problem updating profile');
        }
    }

}