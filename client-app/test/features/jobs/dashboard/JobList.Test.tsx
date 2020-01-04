import { shallow } from "enzyme";
import React from "react";
import { findByTestAttr } from '../../../common/testUtils';
import JobList from "../../../../src/features/jobs/dashboard/JobList";

const setup = (props={}) => {
    const wrapper = shallow(<JobList {...props} />)
    return wrapper;
}

describe('Job List UniTest', () => {
    let wrapper:any;
    beforeEach(() => {
        wrapper = setup([]);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-job-list');
        expect(component.length).toBe(1);
    });
})