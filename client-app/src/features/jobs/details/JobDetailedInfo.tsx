import React from 'react'
import { Segment, Grid, Icon } from 'semantic-ui-react';
import { IJob } from '../../../app/models/job';
import {format} from 'date-fns'


const JobDetailedInfo: React.FC<{job: IJob}> = ({job}) => {
    return (
        <Segment.Group data-test="component-job-detailed-info">
              <Segment attached='top'>
                <Grid>
                  <Grid.Column width={1}>
                    <Icon size='large' color='teal' name='info' />
                  </Grid.Column>
                  <Grid.Column width={15}>
                    <p>{job.company}</p>
                  </Grid.Column>
                </Grid>
              </Segment>
              <Segment attached>
                <Grid verticalAlign='middle'>
                  <Grid.Column width={1}>
                    <Icon name='calendar' size='large' color='teal' />
                  </Grid.Column>
                  <Grid.Column width={15}>
                    <span>
                      {format(job.lastRun,'eeee do MM')} at {format(job.lastRun,'h:mm')}
                    </span>
                  </Grid.Column>
                </Grid>
              </Segment>
              <Segment attached>
                <Grid verticalAlign='middle'>
                  <Grid.Column width={1}>
                    <Icon name='marker' size='large' color='teal' />
                  </Grid.Column>
                  <Grid.Column width={11}>
                    <span>{job.rtoNeeded}, {job.rta}</span>
                  </Grid.Column>
                </Grid>
              </Segment>
            </Segment.Group>
    )
};

export default JobDetailedInfo
