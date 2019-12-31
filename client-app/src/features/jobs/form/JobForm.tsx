import React, { useState, useContext, useEffect } from 'react'
import { Segment, Form, Button, Grid } from 'semantic-ui-react'
import { JobFormValues } from '../../../app/models/job'
import {v4 as uuid} from 'uuid'
import { observer } from 'mobx-react-lite'
import { RouteComponentProps } from 'react-router-dom'
import { Form as FinalForm, Field} from 'react-final-form'
import TextInput from '../../../app/common/form/TextInput'
import TextAreaInput from '../../../app/common/form/TextAreaInput'
import SelectInput from '../../../app/common/form/SelectInput'
import { replication } from '../../../app/common/options/replicationOptions'
import DateInput from '../../../app/common/form/DateInput'
import { combineDateAndTime } from '../../../app/common/util/util'
import { combineValidators, isRequired, composeValidators, hasLengthGreaterThan } from 'revalidate'
import { RootStoreContext } from '../../../app/stores/rootStore'

interface DetailParams {
    id: string;
}

const validate = combineValidators({
    jobName: isRequired({message: 'the job name is required'}),
    company: isRequired('Company'),
    repliction: composeValidators(
        isRequired('Replication'),
        hasLengthGreaterThan(4)({message: 'replication needs to be at least 5 characters'})
    )(),
    servers: isRequired('Servers'),
    rtoNeeded: isRequired('rtoNeeded'),
})

const JobForm: React.FC<RouteComponentProps<DetailParams>> = ({
    match,
    history    
}) => {
    const rootStore = useContext(RootStoreContext);
    const {
        submitting, 
        createJob,
        editJob,
        loadJob
    } = rootStore.jobStore;
    
//state management - each of the below consts represent
//a state object we manage
    const [job, setJob] = useState(new JobFormValues());
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        if (match.params.id) { //we add those checks to make sure we don't call this method redundently
            setLoading(true);   
            loadJob(match.params.id).then( //we can access the then method since our loadJob is async
                (job) => setJob(new JobFormValues(job))
            ).finally(() => setLoading(false));
        }        
    }, [loadJob, match.params.id]) //adding the dependency array to make sure the useEffect only runs once

const handleFinalFormSubmit = (values: any) => {
    const dateAndTime = combineDateAndTime(values.date, values.time);
    const {date, time, ...job} = values;
    job.lastRun = dateAndTime;
    if (!job.id){
        let newJob = {
            ...job,
            id: uuid()
        }
        createJob(newJob);
    } else {
        editJob(job);
    }
}
    return (
        <Grid>
            <Grid.Column width={10}>
            <Segment clearing>
                <FinalForm 
                    validate={validate}
                    initialValues={job}
                    onSubmit={handleFinalFormSubmit}
                    render={({handleSubmit, invalid, pristine}) => (
                        <Form onSubmit={handleSubmit} loading={loading} >
                        <Field 
                            component={TextInput}
                            name='jobname'
                            placeholder='Job Name' 
                            value={job.jobName} 
                        />
                        <Field 
                            component={TextAreaInput} 
                            name='company'
                            placeholder='Company'
                            rows={3} 
                            value={job.company} />
                        <Field 
                            component={SelectInput}
                            options={replication}
                            name='replication'
                            placeholder='Replication' 
                            value={job.replication} />
                        <Form.Group width='equal'>
                            <Field 
                                component={DateInput}
                                name='date'
                                date={true}
                                type='datetime-local' 
                                placeholder='Date' 
                                value={job.date} />
                            <Field 
                                component={DateInput}
                                name='time'
                                time={true}
                                type='datetime-local' 
                                placeholder='Time' 
                                value={job.time} />
                        </Form.Group>
                        <Field 
                            component={TextInput}
                            name='servers'
                            placeholder='Servers' 
                            value={job.servers} />
                        <Field 
                            component={TextInput}
                            name='rtoNeeded'
                            placeholder='RtoNeeded' 
                            value={job.rtoNeeded.toString()} />
                        <Button 
                            loading={submitting} 
                            disabled={loading || invalid || pristine}
                            floated='right' 
                            positive 
                            type='submit' 
                            content='Submit' 
                        />
                        <Button 
                            onClick={
                                job.id
                                ? () => history.push(`/jobs/${job.id}`)
                                : () => history.push('/jobs')
                            }
                            disabled={loading}
                            floated='right' 
                            type='button' 
                            content='Cancel' />
                    </Form>
                    )}
                />
                </Segment>
            </Grid.Column>
        </Grid>
    )
}

export default observer(JobForm)
