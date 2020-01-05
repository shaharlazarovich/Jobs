import { shallow } from "enzyme";
import React from "react";
import { findByTestAttr } from '../../../common/testUtils';
import JobDashboard from "../../../../src/features/jobs/dashboard/JobDashboard";

const setup = (props={}) => {
    const wrapper = shallow(<JobDashboard {...props} />)
    return wrapper;
}

describe('Job Dashboard UniTest', () => {
    let wrapper:any;
    beforeEach(() => {
        wrapper = setup([]);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-job-dashboard');
        expect(component.length).toBe(1);
    });
})