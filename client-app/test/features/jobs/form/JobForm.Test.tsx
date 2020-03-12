import { shallow, ReactWrapper } from "enzyme";
import React from "react";
import { RouteComponentProps } from 'react-router'
import { findByTestAttr } from '../../../common/testUtils';
import JobForm from "../../../../src/features/jobs/form/JobForm";
import { setupWrapper } from '../../../common/wrapper';
import { Route } from "react-router-dom";
import dateFnsLocalizer from 'react-widgets-date-fns';
import { act } from 'react-dom/test-utils';
import flushPromises from 'flush-promises';

dateFnsLocalizer();

describe('Job Form UniTest', () => {
    let wrapper: ReactWrapper;

    beforeEach(async () => {
        wrapper = await setupWrapper(<Route component={JobForm} />);
    });

    test('renders without error', async () => {
        const component = findByTestAttr(wrapper, 'component-job-form');
        expect(component.length).toBe(1);
    });
    test('should match the snapshot', () => {
        const component = findByTestAttr(wrapper, 'component-job-form');
        expect(component).toMatchSnapshot();
    });
    test('renders error message on empty input', async () => {
        const component = findByTestAttr(wrapper, 'component-job-form');
        const inputJobName = component.find('input[name="jobName"]');

        act(() => {
            inputJobName.simulate('change', { target: { name: 'jobName', value: '' } });
            inputJobName.simulate('blur', );
        });
        await flushPromises();

        const alertDiv = inputJobName.closest('div');
        expect(alertDiv.text()).toEqual('the job name is required');
    });
})