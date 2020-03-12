import { ReactWrapper } from "enzyme";
import React from "react";
import { findByTestAttr } from '../../common/testUtils';
import LoginForm from "../../../src/features/user/LoginForm";
import { setupWrapper } from "../../common/wrapper";

describe('Login Form UniTest', () => {
    let wrapper: ReactWrapper;
    beforeEach(async () => {
        wrapper = await setupWrapper(<LoginForm  />);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-login-form');
        expect(component.length).toBe(1);
    });
    test('should match the snapshot', () => {
        const component = findByTestAttr(wrapper, 'component-login-form');
        expect(component).toMatchSnapshot();
    });
})