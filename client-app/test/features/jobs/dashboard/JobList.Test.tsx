import { ReactWrapper } from "enzyme";
import React from "react";
import { findByTestAttr } from '../../../common/testUtils';
import { setupWrapper } from '../../../common/wrapper';
import JobList from "../../../../src/features/jobs/dashboard/JobList";


describe('Job List UniTest', () => {
    let wrapper: ReactWrapper;
    beforeEach(async () => {
        wrapper = await setupWrapper(<JobList />);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-job-list');
        expect(component.length).toBe(1);
    });
    test('should match the snapshot', () => {
        const component = findByTestAttr(wrapper, 'component-job-list');
        expect(component).toMatchSnapshot();
    });
})