import { ReactWrapper } from "enzyme";
import React from "react";
import { findByTestAttr } from '../../common/testUtils';
import NavBar from "../../../src/features/nav/NavBar";
import { setupWrapper } from "../../common/wrapper";

describe('NavBar UniTest', () => {
    let wrapper: ReactWrapper;
    beforeEach(async () => {
        wrapper = await setupWrapper(<NavBar  />);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-navbar');
        expect(component.length).toBe(1);
    });
    test('should match the snapshot', () => {
        const component = findByTestAttr(wrapper, 'component-navbar');
        expect(component).toMatchSnapshot();
    });
})