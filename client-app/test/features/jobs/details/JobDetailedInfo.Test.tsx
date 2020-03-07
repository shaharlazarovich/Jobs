import { ReactWrapper } from "enzyme";
import React from "react";
import { IJob } from '../../../../src/app/models/job'
import { findByTestAttr } from '../../../common/testUtils';
import JobDetailedInfo from "../../../../src/features/jobs/details/JobDetailedInfo";
import { jobMock } from "../../../common/mocks";
import { setupWrapper } from "../../../common/wrapper";

describe('Job Detailed Info UniTest', () => {
    let wrapper: ReactWrapper;
    let testJob: IJob;
    beforeEach(async () => {
        testJob = { ...jobMock };
        wrapper = await setupWrapper(<JobDetailedInfo job={testJob} />);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-job-detailed-info');
        expect(component.length).toBe(1);
    });
    test('should match the snapshot', () => {
        const component = findByTestAttr(wrapper, 'component-job-detailed-info');
        expect(component).toMatchSnapshot();
    });
})