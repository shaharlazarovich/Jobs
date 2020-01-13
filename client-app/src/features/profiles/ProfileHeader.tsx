import React from 'react';
import { Segment, Item, Header, Grid, Divider, Reveal } from 'semantic-ui-react';
import { IProfile } from '../../app/models/profile';
import { observer } from 'mobx-react-lite';

interface IProps {
    profile: IProfile,
    isCurrentUser: boolean,
    loading: boolean
}
const ProfileHeader: React.FC<IProps> = ({
    profile,
    isCurrentUser,
    loading
  }) => {
  return (
    <Segment data-test="component-profile-header">
      <Grid>
        <Grid.Column width={12}>
          <Item.Group>
            <Item>
              <Item.Image
                avatar
                size='small'
                src={profile.image || '/assets/user.png'}
              />
              <Item.Content verticalAlign='middle'>
                <Header as='h1'>{profile.displayName}</Header>
              </Item.Content>
            </Item>
          </Item.Group>
        </Grid.Column>
        <Grid.Column width={4}>
          <Divider/>
          {!isCurrentUser && 
          <Reveal animated='move'>
            <Reveal.Content visible style={{ width: '100%' }}>
              
            </Reveal.Content>
            <Reveal.Content hidden>
              
            </Reveal.Content>
          </Reveal>
          }
        </Grid.Column>
      </Grid>
    </Segment>
  );
};

export default observer(ProfileHeader);
