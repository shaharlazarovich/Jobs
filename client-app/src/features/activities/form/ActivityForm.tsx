import React, { useState, useContext, useEffect } from 'react'
import { Segment, Form, Button, Grid } from 'semantic-ui-react'
import { ActivityFormValues } from '../../../app/models/activity'
import {v4 as uuid} from 'uuid'
import { observer } from 'mobx-react-lite'
import { RouteComponentProps } from 'react-router-dom'
import { Form as FinalForm, Field} from 'react-final-form'
import TextInput from '../../../app/common/form/TextInput'
import TextAreaInput from '../../../app/common/form/TextAreaInput'
import SelectInput from '../../../app/common/form/SelectInput'
import { category } from '../../../app/common/options/categoryOptions'
import DateInput from '../../../app/common/form/DateInput'
import { combineDateAndTime } from '../../../app/common/util/util'
import { combineValidators, isRequired, composeValidators, hasLengthGreaterThan } from 'revalidate'
import { RootStoreContext } from '../../../app/stores/rootStore'


/*
//properties are passed to ui components via an interface,
//so we define the interface here, and we can use this
//property in other components or in the containing app.tsx
interface IProps { im keeping this to show the state before router refactoring
    activity: IActivity;
}

interface IProps { //i'm keeping the full iprops to show how it was befor mobx
    activity: IActivity;
    //setEditMode: (editMode: boolean) => void;//i'm keeping this to show how it was before mobx
    //createActivity: (activity: IActivity) => void;//i'm keeping this to show how it was before mobx
    //editActivity: (activity: IActivity) => void;//i'm keeping this to show how it was before mobx
    //submitting: boolean;//i'm keeping this to show how it was before mobx
}
//we are using a React functional component. the syntax
//below include the component definition as an arrow function,
//but also type declaration of React.FC which gets IProps 
//interface (so we could pass on properties) and a list of
//properties we could use inside the component - it is called
//properties deconstruction

const ActivityForm: React.FC<IProps> = ({
    //setEditMode,//i'm keeping this to show how it was before mobx
    activity: initialFormState,
    //createActivity,//i'm keeping this to show how it was before mobx
    //editActivity,  //i'm keeping this to show how it was before mobx
    //submitting //i'm keeping this to show how it was before mobx

//const ActivityForm: React.FC<IProps> = ({activity: initialFormState}) => {
//im keeping this to show state before router refactoring (since now there's
//no parent element anymore to inherit the initialFormState from - as we're routing)
*/
interface DetailParams {
    id: string;
}

const validate = combineValidators({
    title: isRequired({message: 'the event title is required'}),
    category: isRequired('Category'),
    description: composeValidators(
        isRequired('Description'),
        hasLengthGreaterThan(4)({message: 'Description needs to be at least 5 characters'})
    )(),
    city: isRequired('City'),
    venue: isRequired('Venue'),
    date: isRequired('Date'),
    time: isRequired('Time')
})

const ActivityForm: React.FC<RouteComponentProps<DetailParams>> = ({
    match,
    history    
}) => {
    const rootStore = useContext(RootStoreContext);
    const {
        submitting, 
        createActivity,
        editActivity,
        loadActivity
    } = rootStore.activityStore;
    
  {/* im keeping this to show state before router refactoring
    const initializeForm = () => {
        if (initialFormState){
            return initialFormState
        } else {
            return {
                id:'',
                title:'',
                category:'',
                description:'',
                date:'',
                city:'',
                venue:''
            };
        }
    };
*/}

//state management - each of the below consts represent
//a state object we manage
    const [activity, setActivity] = useState(new ActivityFormValues());
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        if (match.params.id) { //we add those checks to make sure we don't call this method redundently
            setLoading(true);   
            loadActivity(match.params.id).then( //we can access the then method since our loadActivity is async
                (activity) => setActivity(new ActivityFormValues(activity))
            ).finally(() => setLoading(false));
        }        
    }, [loadActivity, match.params.id]) //adding the dependency array to make sure the useEffect only runs once

/*
//the below is a list of handlers to deal with various
//scenarios in the UI

const handleSubmit = () => {
    if (activity.id.length === 0){
        let newActivity = {
            ...activity,
            id: uuid()
        }
        createActivity(newActivity).then(() => history.push(`/activities/${newActivity.id}`))
    } else {
        editActivity(activity).then(() => history.push(`/activities/${activity.id}`))
    }
} im keeping the handleSubmit to show the state before the final-form refactoring 
const handleInputChange = (event: FormEvent<HTMLInputElement | HTMLTextAreaElement>) => { // this handler is needed because we must report input changes to react
//or else we couldn't type input into the fields, due to the fact we already populated them with values from our activities.
//react is working with a virtual dom, and the data you put in react is written to it, and then react is syncing this
//into the real dom - so we also must report to react the other way - when someone is typing in the real dom
//and we want to sync it inot the virtual dom    
    const {name, value} = event.currentTarget;
    setActivity({...activity, [name]: value});
}; im keeping the handleInputChange to show the state before the final-form refactoring 
*/

const handleFinalFormSubmit = (values: any) => {
    const dateAndTime = combineDateAndTime(values.date, values.time);
    const {date, time, ...activity} = values;
    activity.date = dateAndTime;
    if (!activity.id){
        let newActivity = {
            ...activity,
            id: uuid()
        }
        createActivity(newActivity);
    } else {
        editActivity(activity);
    }
}
    return (
        <Grid>
            <Grid.Column width={10}>
            <Segment clearing>
                <FinalForm 
                    validate={validate}
                    initialValues={activity}
                    onSubmit={handleFinalFormSubmit}
                    render={({handleSubmit, invalid, pristine}) => (
                        <Form onSubmit={handleSubmit} loading={loading} >
                        <Field 
                            component={TextInput}
                            name='title'
                            placeholder='Title' 
                            value={activity.title} 
                        />
                        <Field 
                            component={TextAreaInput} 
                            name='description'
                            placeholder='Description'
                            rows={3} 
                            value={activity.description} />
                        <Field 
                            component={SelectInput}
                            options={category}
                            name='category'
                            placeholder='Category' 
                            value={activity.category} />
                        <Form.Group width='equal'>
                            <Field 
                                component={DateInput}
                                name='date'
                                date={true}
                                type='datetime-local' 
                                placeholder='Date' 
                                value={activity.date} />
                            <Field 
                                component={DateInput}
                                name='time'
                                time={true}
                                type='datetime-local' 
                                placeholder='Time' 
                                value={activity.time} />
                        </Form.Group>
                        <Field 
                            component={TextInput}
                            name='city'
                            placeholder='City' 
                            value={activity.city} />
                        <Field 
                            component={TextInput}
                            name='venue'
                            placeholder='Venue' 
                            value={activity.venue} />
                        <Button 
                            loading={submitting} 
                            disabled={loading || invalid || pristine}
                            floated='right' 
                            positive 
                            type='submit' 
                            content='Submit' 
                        />
                        <Button 
                            //onClick={cancelFormOpen} //keeping this to show state before router refactoring
                            onClick={
                                activity.id
                                ? () => history.push(`/activities/${activity.id}`)
                                : () => history.push('/activities')
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

//export default ActivityForm
export default observer(ActivityForm)
