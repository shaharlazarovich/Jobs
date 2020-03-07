import { ReactWrapper } from "enzyme";
import React from "react";
import { findByTestAttr } from '../../../common/testUtils';
import { setupWrapper } from '../../../common/wrapper';
import dateFnsLocalizer from 'react-widgets-date-fns';
import JobDashboard from "../../../../src/features/jobs/dashboard/JobDashboard";

dateFnsLocalizer();

describe('Job Dashboard UniTest', () => {
    let wrapper: ReactWrapper;
    beforeEach(async () => {
        wrapper = await setupWrapper(<JobDashboard  />);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-job-dashboard');
        expect(component.length).toBe(1);
    });
    test('should match the snapshot', () => {
        const component = findByTestAttr(wrapper, 'component-job-dashboard');
        expect(component).toMatchSnapshot();
    });
})