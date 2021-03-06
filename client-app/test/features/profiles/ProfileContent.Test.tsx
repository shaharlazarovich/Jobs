import { ReactWrapper } from "enzyme";
import React from "react";
import { findByTestAttr } from '../../common/testUtils';
import ProfileContent from "../../../src/features/profiles/ProfileContent";
import { setupWrapper } from "../../common/wrapper";

describe('Profile Content UniTest', () => {
    let wrapper: ReactWrapper;
    beforeEach(async () => {
        wrapper = await setupWrapper(<ProfileContent setActiveTab={()=>true} />);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-profile-content');
        expect(component.length).toBe(1);
    });
    test('should match the snapshot', () => {
        const component = findByTestAttr(wrapper, 'component-profile-content');
        expect(component).toMatchSnapshot();
    });
})