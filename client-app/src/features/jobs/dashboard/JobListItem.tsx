import React, { useContext } from 'react'
import { Item, Button, Segment, Icon } from 'semantic-ui-react'
import { Link } from 'react-router-dom'
import { IJob } from '../../../app/models/job'
import { TrackEvent } from '../../../app/models/trackevent'
import {format} from 'date-fns'
import { RootStoreContext } from '../../../app/stores/rootStore'
import moment from 'moment'
import {v4 as uuid} from 'uuid'

const JobListItem: React.FC<{ job: IJob }> = ({ job }) => {
  const rootStore = useContext(RootStoreContext);
  const { user} = rootStore.userStore;
  const { runRemoteJob } = rootStore.jobStore;

  const track = () => {
    let trackEvent = new TrackEvent();
    trackEvent = {
      id: uuid(),
      jobId: job.id,
      jobName: job.jobName,
      userId: user!.username,
      actionDate: moment().toDate(),
      remoteIP: job.jobIP,
      remoteResponse: "",
      requestProperties: "",
      source: "Jobs_List",
      event: "runjob",
    }
    runRemoteJob(trackEvent);
  }

  return (
    <Segment.Group data-test="component-job-list-item">
      <Segment>
          <Item.Group>
          <Item>
          <Item.Image size='tiny' circular src={'/assets/user.png'}
          style={{marginBottom: 3}} />
          <Item.Content>
            <Item.Header as={Link} to={`/jobs/${job.id}`}>{job.jobName}</Item.Header>
            <Item.Description>
              Created by <Link to={`/profile/${user!.username}`}> {user!.displayName }</Link>
            </Item.Description>
          </Item.Content>
        </Item>
        </Item.Group>
      </Segment>
      <Segment>
          <Icon name='clock' />{format(job.lastRun, 'h:mm a')}
          <Icon name='marker' />{job.rtoNeeded} , {job.rta}
      </Segment>
      <Segment clearing>
            <span>{job.results}</span>
            <Button
                as={Link}
                to={`/jobs/${job.id}`}
                floated="right"
                content="View"
                color="blue"
            />
            <Button
                onClick={ track }
                floated="right"
                content="Run"
                color="blue"
            />
      </Segment>
    </Segment.Group>
  );
};

export default JobListItem
