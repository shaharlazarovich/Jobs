import { ReactWrapper } from "enzyme";
import { setupWrapper } from '../../../common/wrapper';
import React from "react";
import { findByTestAttr } from '../../../common/testUtils';
import JobListItemPlaceHolder from "../../../../src/features/jobs/dashboard/JobListItemPlaceHolder";

describe('Job List Item Place Holder UniTest', () => {
    let wrapper: ReactWrapper;
    beforeEach(async () => {
        wrapper = await setupWrapper(<JobListItemPlaceHolder  />);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-job-list-item-place-holder');
        expect(component.length).toBe(1);
    });
    test('should match the snapshot', () => {
        const component = findByTestAttr(wrapper, 'component-job-list-item-place-holder');
        expect(component).toMatchSnapshot();
    });
})