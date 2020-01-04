import { shallow } from "enzyme";
import React from "react";
import { findByTestAttr } from '../common/testUtils';
import App from "../../src/app/layout/App";

const setup = (props={}) => {
    const wrapper = shallow(<App {...props} />)
    return wrapper;
}

describe('App UniTest', () => {
    let wrapper:any;
    beforeEach(() => {
        wrapper = setup([]);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-app');
        expect(component.length).toBe(1);
    });

    test('app snapshots test', () => {
      const wrapper = shallow(<App />);
      expect(wrapper).toMatchSnapshot()
    })
})