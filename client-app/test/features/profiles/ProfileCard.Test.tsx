import { shallow } from "enzyme";
import React from "react";
import { IProfile } from '../../../src/app/models/profile'
import { findByTestAttr } from '../../common/testUtils';
import ProfileCard from "../../../src/features/profiles/ProfileCard";

interface IProps {
  profile: IProfile
}

const setup = (profile:any) => {
    const wrapper = shallow<IProps>(<ProfileCard {...profile} />)
    return wrapper;
}

describe('Profile Card UniTest', () => {
    let wrapper:any;
    beforeEach(() => {
        wrapper = setup([]);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-profile-card');
        expect(component.length).toBe(1);
    });
})