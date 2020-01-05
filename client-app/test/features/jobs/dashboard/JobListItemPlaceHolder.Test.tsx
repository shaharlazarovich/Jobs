import { shallow } from "enzyme";
import React from "react";
import { findByTestAttr } from '../../../common/testUtils';
import JobListItemPlaceHolder from "../../../../src/features/jobs/dashboard/JobListItemPlaceHolder";

const setup = (props={}) => {
    const wrapper = shallow(<JobListItemPlaceHolder {...props} />)
    return wrapper;
}

describe('Job List Item Place Holder UniTest', () => {
    let wrapper:any;
    beforeEach(() => {
        wrapper = setup([]);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-job-list-item-place-holder');
        expect(component.length).toBe(1);
    });
})