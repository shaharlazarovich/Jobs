import { ReactWrapper } from "enzyme";
import React from "react";
import { IJob } from '../../../../src/app/models/job'
import { findByTestAttr } from '../../../common/testUtils';
import JobDetailedHeader from "../../../../src/features/jobs/details/JobDetailedHeader";
import { jobMock } from "../../../common/mocks";
import { setupWrapper } from "../../../common/wrapper";

describe('Job Detailed Header UniTest', () => {
    let wrapper: ReactWrapper;
    let testJob: IJob;
    beforeEach(async () => {
        testJob = { ...jobMock };
        wrapper = await setupWrapper(<JobDetailedHeader job={testJob} />);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-job-detailed-header');
        expect(component.length).toBe(1);
    });
    test('should match the snapshot', () => {
        const component = findByTestAttr(wrapper, 'component-job-detailed-header');
        expect(component).toMatchSnapshot();
    });
})