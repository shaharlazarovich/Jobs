import React, { useContext, Fragment } from 'react'
import { Item, Label } from 'semantic-ui-react'
import { observer } from 'mobx-react-lite'
import JobListItem from './JobListItem'
import { RootStoreContext } from '../../../app/stores/rootStore'
import {format} from 'date-fns'

const JobList: React.FC = () => {
    const rootStore = useContext(RootStoreContext);
    const {jobsByDate} = rootStore.jobStore; //destructure of parameters
    return ( //we want to group our jobs by date, using the method we created in jobStore
    <Fragment>
        {jobsByDate.map(([group, jobs])=> (
            <Fragment key={group}>
                <Label size='large' color='blue'>
                    {format(new Date(group), 'eeee do MMMM')}
                </Label>
                <Item.Group divided>
                {jobs.map(job => (
                    <JobListItem key={job.id} job={job} />
                ))}
                </Item.Group>
            </Fragment>
        ))}
    </Fragment>
    )
}

export default observer(JobList)
