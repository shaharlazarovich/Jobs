import { shallow } from "enzyme";
import React from "react";
import { findByTestAttr } from '../../../common/testUtils';
import { IJob } from '../../../../src/app/models/job'
import JobListItem from "../../../../src/features/jobs/dashboard/JobListItem";

const setup = (job:any) => {
    const wrapper = shallow<IJob>(<JobListItem {...job} />)
    return wrapper;
}

describe('Job List Item UniTest', () => {
    let wrapper:any;
    beforeEach(() => {
        wrapper = setup([]);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-job-list-item');
        expect(component.length).toBe(1);
    });
})