import { ReactWrapper } from "enzyme";
import React from "react";
import { IProfile } from '../../../src/app/models/profile'
import { findByTestAttr } from '../../common/testUtils';
import ProfileCard from "../../../src/features/profiles/ProfileCard";
import { profileMock } from "../../common/mocks";
import { setupWrapper } from "../../common/wrapper";

describe('Profile Card UniTest', () => {
    let wrapper: ReactWrapper;
    let testProfile: IProfile;
    beforeEach(async () => {
        testProfile = { ...profileMock };
        wrapper = await setupWrapper(<ProfileCard profile={testProfile} />);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-profile-card');
        expect(component.length).toBe(1);
    });
    test('should match the snapshot', () => {
        const component = findByTestAttr(wrapper, 'component-profile-card');
        expect(component).toMatchSnapshot();
    });
})