import React, { useContext } from 'react'
import { Segment, Item, Button, Header, Image } from 'semantic-ui-react'
import { IJob } from '../../../app/models/job';
import { observer } from 'mobx-react-lite';
import { Link } from 'react-router-dom';
import {format} from 'date-fns'
import { RootStoreContext } from '../../../app/stores/rootStore';

const jobImageStyle = {
  filter: 'brightness(30%)'
};

const jobImageTextStyle = {
  position: 'absolute',
  bottom: '5%',
  left: '5%',
  width: '100%',
  height: 'auto',
  color: 'white'
};

const JobDetailsHeader: React.FC<{job: IJob}> = ({job}) => {
  const rootStore = useContext(RootStoreContext);
  const user = rootStore.userStore.user;
  return (
            <Segment.Group data-test="component-job-detailed-header">
              <Segment basic attached='top' style={{ padding: '0' }}>
                <Image src={`/assets/replicationImages/${job.replication}.jpg`} fluid style={jobImageStyle} />
                <Segment basic style={jobImageTextStyle}>
                  <Item.Group>
                    <Item>
                      <Item.Content>
                        <Header
                          size='huge'
                          content={job.jobName}
                          style={{ color: 'white' }}
                        />
                        <p>{format(job.lastRun, 'eeee do MM')}</p>
                        <p>
                          Created by <Link to={`/profile/${user!.username}`}><strong>{user!.displayName }</strong></Link>
                        </p>
                      </Item.Content>
                    </Item>
                  </Item.Group>
                </Segment>
              </Segment>
              <Segment clearing attached='bottom'>
                  <Button as={Link} to={`/manage/${job.id}`} color='orange' floated='right'>
                    Edit Job
                  </Button>
              </Segment>
            </Segment.Group>
    );
};

export default observer(JobDetailsHeader);