import { ReactWrapper } from "enzyme";
import React from "react";
import { IProfile } from '../../../src/app/models/profile'
import { findByTestAttr } from '../../common/testUtils';
import ProfileHeader from "../../../src/features/profiles/ProfileHeader";
import { profileMock } from "../../common/mocks";
import { setupWrapper } from "../../common/wrapper";

describe('Profile Header UniTest', () => {
    let wrapper: ReactWrapper;
    let testProfile: IProfile;
    beforeEach(async () => {
        testProfile = { ...profileMock };
        wrapper = await setupWrapper(<ProfileHeader profile={testProfile} isCurrentUser={true} />);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-profile-header');
        expect(component.length).toBe(1);
    });
    test('should match the snapshot', () => {
        const component = findByTestAttr(wrapper, 'component-profile-header');
        expect(component).toMatchSnapshot();
    });
})