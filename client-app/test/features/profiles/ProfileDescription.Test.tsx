import { ReactWrapper } from "enzyme";
import React from "react";
import { findByTestAttr } from '../../common/testUtils';
import ProfileDescription from "../../../src/features/profiles/ProfileDescription";
import { setupWrapper } from "../../common/wrapper";

describe('Profile Description UniTest', () => {
    let wrapper: ReactWrapper;
    beforeEach(async () => {
        wrapper = await setupWrapper(<ProfileDescription  />);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-profile-description');
        expect(component.length).toBe(1);
    });
    test('should match the snapshot', () => {
        const component = findByTestAttr(wrapper, 'component-profile-description');
        expect(component).toMatchSnapshot();
    });
})