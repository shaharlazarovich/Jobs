import { ReactWrapper } from "enzyme";
import React from "react";
import { findByTestAttr } from '../../common/testUtils';
import ProfilePage from "../../../src/features/profiles/ProfilePage";
import { setupWrapper } from "../../common/wrapper";
import { Route } from "react-router-dom";
import { match } from '../../common/mocks';

describe('Profile Page UniTest', () => {
    let wrapper: ReactWrapper;
    beforeEach(async () => {
        wrapper = await setupWrapper(<Route component={ProfilePage} />);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-profile-page');
        expect(component.length).toBe(1);
    });
    test('should match the snapshot', () => {
        const component = findByTestAttr(wrapper, 'component-profile-page');
        expect(component).toMatchSnapshot();
    });
})