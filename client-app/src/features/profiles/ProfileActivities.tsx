import React, { useEffect, useContext } from 'react';
import { observer } from 'mobx-react-lite';
import { Tab, Grid, Header, Card, Image, TabProps } from 'semantic-ui-react';
import { Link } from 'react-router-dom';
import { IUserActivity } from '../../app/models/profile';
import { format } from 'date-fns';
import { RootStoreContext } from '../../app/stores/rootStore';

const panes = [
  { menuItem: 'Future Events', pane: { key: 'futureEvents' } },
  { menuItem: 'Past Events', pane: { key: 'pastEvents' } },
  { menuItem: 'Hosting', pane: { key: 'hosted' } }
];

const ProfileEvents = () => {
  const rootStore = useContext(RootStoreContext);
  const {
    profile,
    loadingActivities,
  } = rootStore.profileStore!;

  const handleTabChange = (
    e: React.MouseEvent<HTMLDivElement, MouseEvent>,
    data: TabProps
  ) => {
    let predicate;
    switch (data.activeIndex) {
      case 1:
        predicate = 'past';
        break;
      case 2:
        predicate = 'hosting';
        break;
      default:
        predicate = 'future';
        break;
    }
  };

  return (
    <Tab.Pane loading={loadingActivities}>
      <Grid>
        <Grid.Column width={16}>
          <Header floated='left' icon='calendar' content={'Activities'} />
        </Grid.Column>
        <Grid.Column width={16}>
          <Tab
            panes={panes}
            menu={{ secondary: true, pointing: true }}
            onTabChange={(e, data) => handleTabChange(e, data)}
          />
          <br />
          <Card.Group itemsPerRow={4}>
          </Card.Group>
        </Grid.Column>
      </Grid>
    </Tab.Pane>
  );
};

export default observer(ProfileEvents);
