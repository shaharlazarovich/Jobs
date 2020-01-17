import { shallow } from "enzyme";
import React from "react";
import { findByTestAttr } from '../../../common/testUtils';
import { IJob, JobFormValues } from '../../../../src/app/models/job'
import JobListItem from "../../../../src/features/jobs/dashboard/JobListItem";

const setup = (job:any) => {
    const wrapper = shallow<IJob>(<JobListItem job={job} />)
    return wrapper;
}

describe('Job List Item UniTest', () => {
    let wrapper:any;
    let testJob = new JobFormValues();
    beforeEach(() => {
        testJob = { 
            id:"4", 
            replication:"VMware SRM",
            jobName:"test1",
            company: 'Shomera',  
            lastRun: new Date('2020-01-06 23:00:05'),  
            servers: '10',
            results: 'OK',
            key: 'AAAA-BBBB-CCCC-DDDD',
            rtoNeeded: "10"
        }
        wrapper = setup({ testJob });
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-job-list-item');
        expect(component.length).toBe(1);
    });
})