import { ReactWrapper } from "enzyme";
import React from "react";
import { findByTestAttr } from '../../../common/testUtils';
import JobDetails from "../../../../src/features/jobs/details/JobDetails";
import { setupWrapper } from "../../../common/wrapper";
import { Route } from "react-router-dom";
import { match , history } from '../../../common/mocks';

describe('Job Details UniTest', () => {
    let wrapper: ReactWrapper;
    beforeEach(async () => {
        wrapper = await setupWrapper(<Route component={JobDetails} match={match} history={history} />);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-job-details');
        expect(component.length).toBe(1);
    });
    test('should match the snapshot', () => {
        const component = findByTestAttr(wrapper, 'component-job-details');
        expect(component).toMatchSnapshot();
    });
})