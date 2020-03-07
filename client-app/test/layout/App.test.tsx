import { ReactWrapper } from "enzyme";
import React from "react";
import { setupWrapper } from '../common/wrapper';
import { findByTestAttr } from '../common/testUtils';
import App from "../../src/app/layout/App";
import { Route } from "react-router-dom";

describe('App UniTest', () => {
    let wrapper: ReactWrapper;
    beforeEach(async () => {
        wrapper = await setupWrapper(<Route component={App} />);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-app');
        expect(component.length).toBe(1);
    });

    test('should match the snapshot', () => {
        const component = findByTestAttr(wrapper, 'component-job-list-item');
        expect(component).toMatchSnapshot();
    });
})