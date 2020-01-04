import { shallow } from "enzyme";
import React from "react";
import NotFound from "../../src/app/layout/NotFound";

describe('Hello, Enzyme!', () => {
    test('renders', () => {
        const wrapper = shallow(<div><h1>Hello, Enzyme!</h1></div>);
        console.log(wrapper.debug());
        expect(wrapper.find('h1').html()).toMatch(/Hello, Enzyme/);
      })

      test('renders', () => {
        const wrapper = shallow(<div><h1>Hello, Enzyme!</h1></div>);
        console.log(wrapper.debug());
        expect(wrapper).toBeTruthy();//meaning - wrapper is not null/undefined etc/
      })
    
      test('renders snapshots, too', () => {
        const wrapper = shallow(<div><h1>Hello, Enzyme!</h1></div>);
        expect(wrapper).toMatchSnapshot()
      })

      test('notfound snapshots, too', () => {
        const wrapper = shallow(<NotFound />);
        expect(wrapper).toMatchSnapshot()
      })

      test('notfound', () => {
        const wrapper = shallow(<NotFound />);
        expect(wrapper.find('h1').html()).toMatch(/Test/)
      })

      
});
