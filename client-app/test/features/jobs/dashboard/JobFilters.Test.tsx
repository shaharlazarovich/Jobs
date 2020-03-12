import { ReactWrapper } from "enzyme";
import React from "react";
import { findByTestAttr } from '../../../common/testUtils';
import JobFilters from "../../../../src/features/jobs/dashboard/JobFilters";
import { setupWrapper } from "../../../common/wrapper";
import dateFnsLocalizer from 'react-widgets-date-fns';

dateFnsLocalizer();

describe('Job Filters UniTest', () => {
    let wrapper: ReactWrapper;
    beforeEach(async () => {
        wrapper = await setupWrapper(<JobFilters  />);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-job-filters');
        expect(component.length).toBe(1);
    });
    test('should match the snapshot', () => {
        const component = findByTestAttr(wrapper, 'component-job-filters');
        expect(component).toMatchSnapshot();
    });
})