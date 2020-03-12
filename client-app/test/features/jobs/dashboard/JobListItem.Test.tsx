import { ReactWrapper } from "enzyme";
import React from "react";
import { findByTestAttr } from '../../../common/testUtils';
import { IJob } from '../../../../src/app/models/job'
import JobListItem from "../../../../src/features/jobs/dashboard/JobListItem";
import { setupWrapper } from '../../../common/wrapper';
import { act } from 'react-dom/test-utils';
import flushPromises from 'flush-promises';
import { toast } from 'react-toastify';
import { jobMock } from '../../../common/mocks';

describe('Job List Item UniTest', () => {
    let wrapper: ReactWrapper;
    let testJob: IJob;
    beforeEach(async () => {
        testJob = { ...jobMock };
        wrapper = await setupWrapper(<JobListItem job={testJob} />);
    });

    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-job-list-item');
        expect(component.length).toBe(1);
    });

    test('should match the snapshot', () => {
        const component = findByTestAttr(wrapper, 'component-job-list-item');
        expect(component).toMatchSnapshot();
    });

    test('run button', async () => {
        const component = findByTestAttr(wrapper, 'component-job-list-item');
        const button = component.find('[data-test="run-button"]').at(0);
        const errorSpy = jest.spyOn(toast, 'error');
        const infoSpy = jest.spyOn(toast, 'info');

        act(() => {
            button.simulate('click');
        });
        await flushPromises();

        expect(infoSpy).toHaveBeenCalledWith('run job request sent');
        expect(errorSpy).not.toHaveBeenCalled();
    });
})