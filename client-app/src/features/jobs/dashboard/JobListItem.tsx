import React, { useContext } from 'react'
import { Item, Button, Segment, Icon } from 'semantic-ui-react'
import { Link } from 'react-router-dom'
import { IJob } from '../../../app/models/job'
import { TrackEvent } from '../../../app/models/trackevent'
import {format} from 'date-fns'
import { RootStoreContext } from '../../../app/stores/rootStore'
import moment from 'moment'
import {v4 as uuid} from 'uuid'
import { useMatomo } from '@datapunt/matomo-tracker-react'

const JobListItem: React.FC<{ job: IJob }> = ({ job }) => {
  const rootStore = useContext(RootStoreContext);
  const { user} = rootStore.userStore;
  const { runRemoteJob } = rootStore.jobStore;
  const { trackPageView, trackEvent } = useMatomo();

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
      eventTracked: "runjob",
    }
    runRemoteJob(trackEvent);
  }

  const handleOnClick = () => {
    trackEvent({
      category: 'Button clicks',
      action: 'joblistitem-event',
      name: 'trackJob', 
      value: 234, 
      documentTitle: 'JobListItem', 
      href: 'http://localhost:3000', 
      customDimensions: [
        {
          id: 1,
          value: 'loggedIn',
        },
      ],  
    });
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
            <Button
                onClick={ handleOnClick }
                floated="right"
                content="Track"
                color="blue"
            />
      </Segment>
    </Segment.Group>
  );
};

export default JobListItem
