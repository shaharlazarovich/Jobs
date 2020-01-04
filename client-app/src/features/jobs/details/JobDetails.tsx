import React, { useContext, useEffect } from 'react'
import { Grid } from 'semantic-ui-react'
import { observer } from 'mobx-react-lite'
import { RouteComponentProps } from 'react-router'
import LoadingComponent from '../../../app/layout/LoadingComponent'
import JobDetailedHeader from './JobDetailedHeader'
import JobDetailedInfo from './JobDetailedInfo'
import { RootStoreContext } from '../../../app/stores/rootStore'

interface DetailParams {
  id: string
}

const JobDetails: React.FC<RouteComponentProps<DetailParams>> = ({
  match,
  history
}) => {
  const rootStore = useContext(RootStoreContext);
  const {
          job,
          loadJob,
          loadingInitial
        } = rootStore.jobStore; //destructure of parameters
        
  useEffect(() => {
    loadJob(match.params.id);
  }, [loadJob, match.params.id,history])

  if (loadingInitial) return <LoadingComponent content='Loading job...' />
  if (!job) return <h2>Job Not Found</h2>
  return (
    <Grid data-test="component-job-details">
      <Grid.Column width={16}>
        <JobDetailedHeader job={job} />
        <JobDetailedInfo job={job} />
      </Grid.Column>
    </Grid>
  );
}

export default observer(JobDetails)
