import { shallow } from "enzyme";
import React from "react";
import { findByTestAttr } from '../../common/testUtils';
import RegisterForm from "../../../src/features/user/RegisterForm";

const setup = (props={}) => {
    const wrapper = shallow(<RegisterForm {...props} />)
    return wrapper;
}

describe('Register Form UniTest', () => {
    let wrapper:any;
    beforeEach(() => {
        wrapper = setup([]);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-register-form');
        expect(component.length).toBe(1);
    });
})