import React, { Fragment, useContext } from 'react';
import { Menu, Header } from 'semantic-ui-react';
import { Calendar } from 'react-widgets';
import { RootStoreContext } from '../../../app/stores/rootStore';
import { observer } from 'mobx-react-lite';

const JobFilters = () => {
  const rootStore = useContext(RootStoreContext);
  const { predicate, setPredicate } = rootStore.jobStore;
  return (
    <Fragment data-test="component-job-filters">
      <Menu vertical size={'large'} style={{ width: '100%', marginTop: 50 }}>
        <Header icon={'filter'} attached color={'teal'} content={'Filters'} />
        <Menu.Item
          active={predicate.size === 0}
          onClick={() => setPredicate('all', 'true')}
          color={'blue'}
          name={'all'}
          content={'All Jobs'}
        />
      </Menu>
      <Header
        icon={'calendar'}
        attached
        color={'teal'}
        content={'Show jobs after selected date'}
      />
      <Calendar
        onChange={date => setPredicate('lastRun', date!)}
        value={predicate.get('lastRun')}
      />
    </Fragment>
  );
};

export default observer(JobFilters);