import { shallow } from "enzyme";
import React from "react";
import { findByTestAttr } from '../../common/testUtils';
import ProfileDescription from "../../../src/features/profiles/ProfileDescription";

const setup = (props={}) => {
    const wrapper = shallow(<ProfileDescription {...props} />)
    return wrapper;
}

describe('Profile Description UniTest', () => {
    let wrapper:any;
    beforeEach(() => {
        wrapper = setup([]);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-profile-description');
        expect(component.length).toBe(1);
    });
})