import { shallow } from "enzyme";
import React from "react";
import { findByTestAttr } from '../../common/testUtils';
import NavBar from "../../../src/features/nav/NavBar";

const setup = (props={}) => {
    const wrapper = shallow(<NavBar {...props} />)
    return wrapper;
}

describe('NavBar UniTest', () => {
    let wrapper:any;
    beforeEach(() => {
        wrapper = setup([]);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-navbar');
        expect(component.length).toBe(1);
    });
})