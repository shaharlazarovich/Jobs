import { shallow } from "enzyme";
import React from "react";
import { findByTestAttr } from '../../common/testUtils';
import LoginForm from "../../../src/features/user/LoginForm";

const setup = (props={}) => {
    const wrapper = shallow(<LoginForm {...props} />)
    return wrapper;
}

describe('Login Form UniTest', () => {
    let wrapper:any;
    beforeEach(() => {
        wrapper = setup([]);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-login-form');
        expect(component.length).toBe(1);
    });
})