import { mount } from "enzyme";
import React, { useContext, useEffect } from "react";
import {observer} from 'mobx-react-lite'; 
import { RootStoreContext } from "../../src/app/stores/rootStore";
import { IUserFormValues, IUser } from "../../src/app/models/users";
import moxios from 'moxios';
import agent from '../../src/app/api/agentBase';
import { MemoryRouter, RouteComponentProps } from "react-router-dom";
import { IProfile } from "../../src/app/models/profile";
import { routerTestProps } from "./routeUtil";

const userFormValuesMock: IUserFormValues = {
    email: 'fake@email.com',
    password: 'FakePassword',
    username: 'Fake Username', 
};

const userMock: IUser = {
    username: 'Fake Username', 
    displayName: 'Fake Username', 
    token: 'some-like-token', 
};

const profileMock: IProfile = {
    userName: 'Fake Username', 
    displayName: 'Fake Username', 
    bio: 'Fake Bio', 
    image: "",
    following: true,
    followersCount: 1,
    followingCount: 1,
    photos: null
};

const userName: string = "bob"

export const setupWrapper = async (testedElement: JSX.Element) => {
    const WrapperComponent: React.FunctionComponent = observer(() => {
        const rootStore = useContext(RootStoreContext);
        let props: RouteComponentProps = routerTestProps('/route/:id', { id: 'cd3da3fb-4f8b-4b40-a814-1b5839f8069f' });
        if (!rootStore.userStore.user) {
            moxios.install(agent.axios);
            moxios.wait(() => {
                const req = moxios.requests.mostRecent();
                req.respondWith({
                    status: 200,
                    response: userMock
                })
            });
            rootStore.userStore.login(userFormValuesMock);

            if (!rootStore.profileStore.profile) {
                moxios.install(agent.axios);
                moxios.wait(() => {
                    const req = moxios.requests.mostRecent();
                    req.respondWith({
                        status: 200,
                        response: profileMock
                    })
                });
                rootStore.profileStore.loadProfile(userName);
            }

            return null;
        }

        moxios.uninstall(agent.axios);
        
        return <MemoryRouter {...props}>{testedElement}</MemoryRouter>;
    });
    //since we are returning null fro the moxios call,
    //we are using flushPromises to await until we populate
    //our user store inside the context and re-render (otherwise,
    //our test would receive back null and fail)
    const wrapper = mount(<WrapperComponent/>);
    await new Promise((res, rej) => setTimeout(() => res(), 5000));
    wrapper.update();

    return wrapper;
}
