import { shallow } from "enzyme";
import React from "react";
import { findByTestAttr } from '../../common/testUtils';
import ProfileContent from "../../../src/features/profiles/ProfileContent";

interface IProps {
    setActiveTab: (activeIndex: any) => void;
}

const setup = (setActiveTab:any) => {
    const wrapper = shallow<IProps>(<ProfileContent {...setActiveTab} />)
    return wrapper;
}

describe('Profile Content UniTest', () => {
    let wrapper:any;
    beforeEach(() => {
        wrapper = setup([]);
    });
    test('renders without error', () => {
        const component = findByTestAttr(wrapper, 'component-profile-content');
        expect(component.length).toBe(1);
    });
})