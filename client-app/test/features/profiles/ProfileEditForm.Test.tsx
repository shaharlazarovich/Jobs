import { ReactWrapper } from "enzyme";
import React from "react";
import { IProfile } from '../../../src/app/models/profile'
import { findByTestAttr } from '../../common/testUtils';
import ProfileEditForm from "../../../src/features/profiles/ProfileEditForm";
import { profileMock } from "../../common/mocks";
import { setupWrapper } from "../../common/wrapper";

describe('Profile Edit Form UniTest', () => {
    let wrapper: ReactWrapper;
    let testProfile: IProfile;
    beforeEach(async () => {
        testProfile = { ...profileMock };
        wrapper = await setupWrapper(<ProfileEditForm profile={testProfile} updateProfile={()=>true} />);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-profile-edit-form');
        expect(component.length).toBe(1);
    });
    test('should match the snapshot', () => {
        const component = findByTestAttr(wrapper, 'component-profile-edit-form');
        expect(component).toMatchSnapshot();
    });
})