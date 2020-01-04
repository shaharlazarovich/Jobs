import { shallow } from "enzyme";
import React from "react";
import { RouteComponentProps } from 'react-router'
import { findByTestAttr } from '../../../common/testUtils';
import JobDetails from "../../../../src/features/jobs/details/JobDetails";

interface DetailParams {
  id: string
}

const setup = (props:any) => {
    const wrapper = shallow<RouteComponentProps<DetailParams>>(<JobDetails {...props} />)
    return wrapper;
}

describe('Job Details UniTest', () => {
    let wrapper:any;
    beforeEach(() => {
        wrapper = setup([]);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-job-details');
        expect(component.length).toBe(1);
    });
})