import { shallow } from "enzyme";
import React from "react";
import { IProfile } from '../../../src/app/models/profile'
import { findByTestAttr } from '../../common/testUtils';
import ProfileEditForm from "../../../src/features/profiles/ProfileEditForm";

interface IProps {
  profile: IProfile;
}

const setup = (profile:any) => {
    const wrapper = shallow<IProps>(<ProfileEditForm {...profile} />)
    return wrapper;
}

describe('Profile Edit Form UniTest', () => {
    let wrapper:any;
    beforeEach(() => {
        wrapper = setup([]);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-profile-edit-form');
        expect(component.length).toBe(1);
    });
})