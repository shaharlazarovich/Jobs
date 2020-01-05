import { shallow } from "enzyme";
import React from "react";
import { RouteComponentProps } from 'react-router'
import { findByTestAttr } from '../../common/testUtils';
import ProfilePage from "../../../src/features/profiles/ProfilePage";

interface RouteParams {
    username: string
}

interface IProps extends RouteComponentProps<RouteParams>{}

const setup = (props:any) => {
    const wrapper = shallow<IProps>(<ProfilePage {...props} />)
    return wrapper;
}

describe('Profile Page UniTest', () => {
    let wrapper:any;
    beforeEach(() => {
        wrapper = setup([]);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-profile-page');
        expect(component.length).toBe(1);
    });
})