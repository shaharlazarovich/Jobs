import { shallow } from "enzyme";
import React from "react";
import { IProfile } from '../../../src/app/models/profile'
import { findByTestAttr } from '../../common/testUtils';
import ProfileHeader from "../../../src/features/profiles/ProfileHeader";

interface IProps {
  profile: IProfile;
}

const setup = (profile:any) => {
    const wrapper = shallow<IProps>(<ProfileHeader {...profile} />)
    return wrapper;
}

describe('Profile Header UniTest', () => {
    let wrapper:any;
    beforeEach(() => {
        wrapper = setup([]);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-profile-header');
        expect(component.length).toBe(1);
    });
})