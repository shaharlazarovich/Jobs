import { ReactWrapper } from "enzyme";
import React from "react";
import { findByTestAttr } from '../../common/testUtils';
import RegisterForm from "../../../src/features/user/RegisterForm";
import { setupWrapper } from "../../common/wrapper";

describe('Register Form UniTest', () => {
    let wrapper: ReactWrapper;
    beforeEach(async () => {
        wrapper = await setupWrapper(<RegisterForm  />);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-register-form');
        expect(component.length).toBe(1);
    });
    test('should match the snapshot', () => {
        const component = findByTestAttr(wrapper, 'component-register-form');
        expect(component).toMatchSnapshot();
    });
})