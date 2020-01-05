import { shallow } from "enzyme";
import React from "react";
import { RouteComponentProps } from 'react-router'
import { findByTestAttr } from '../../../common/testUtils';
import JobForm from "../../../../src/features/jobs/form/JobForm";

interface DetailParams {
  id: string
}

const setup = (props:any) => {
    const wrapper = shallow<RouteComponentProps<DetailParams>>(<JobForm {...props} />)
    return wrapper;
}

describe('Job Form UniTest', () => {
    let wrapper:any;
    beforeEach(() => {
        wrapper = setup([]);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-job-form');
        expect(component.length).toBe(1);
    });
})