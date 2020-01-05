import { shallow } from "enzyme";
import React from "react";
import { IJob } from '../../../../src/app/models/job'
import { findByTestAttr } from '../../../common/testUtils';
import JobDetailedInfo from "../../../../src/features/jobs/details/JobDetailedInfo";

const setup = (job:any) => {
    const wrapper = shallow<IJob>(<JobDetailedInfo {...job} />)
    return wrapper;
}

describe('Job Detailed Info UniTest', () => {
    let wrapper:any;
    beforeEach(() => {
        wrapper = setup([]);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-job-detailed-info');
        expect(component.length).toBe(1);
    });
})