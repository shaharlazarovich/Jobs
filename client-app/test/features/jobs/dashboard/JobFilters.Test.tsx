import { shallow } from "enzyme";
import React from "react";
import { findByTestAttr } from '../../../common/testUtils';
import JobFilters from "../../../../src/features/jobs/dashboard/JobFilters";

const setup = (props={}) => {
    const wrapper = shallow(<JobFilters {...props} />)
    return wrapper;
}

describe('Job Filters UniTest', () => {
    let wrapper:any;
    beforeEach(() => {
        wrapper = setup([]);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-job-filters');
        expect(component.length).toBe(1);
    });
})