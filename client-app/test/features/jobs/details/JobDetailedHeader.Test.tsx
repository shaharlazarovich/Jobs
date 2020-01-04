import { shallow } from "enzyme";
import React from "react";
import { IJob } from '../../../../src/app/models/job'
import { findByTestAttr } from '../../../common/testUtils';
import JobDetailedHeader from "../../../../src/features/jobs/details/JobDetailedHeader";

const setup = (job:any) => {
    const wrapper = shallow<IJob>(<JobDetailedHeader {...job} />)
    return wrapper;
}

describe('Job Detailed Header UniTest', () => {
    let wrapper:any;
    beforeEach(() => {
        wrapper = setup([]);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-job-detailed-header');
        expect(component.length).toBe(1);
    });
})