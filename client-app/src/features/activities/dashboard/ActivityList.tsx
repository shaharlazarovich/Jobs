import React, { useContext, Fragment } from 'react'
import { Item, Label } from 'semantic-ui-react'
import { observer } from 'mobx-react-lite'
import ActivityListItem from './ActivityListItem'
import { RootStoreContext } from '../../../app/stores/rootStore'
import {format} from 'date-fns'

/*
//properties are passed to ui components via an interface,
//so we define the interface here, and we can use this
//property in other components or in the containing app.tsx
interface IProps {
    //activities: IActivity[];              //i'm keeping this to show the state  
    //selectActivity: (id: string) => void; //before mobx refactoring
    //deleteActivity: (event: SyntheticEvent<HTMLButtonElement>, id: string) => void;//before mobx refactoring
    //submitting: boolean;//before mobx refactoring
    //target: string;//before mobx refactoring
}

//we are using a React functional component. the syntax
//below include the component definition as an arrow function,
//but also type declaration of React.FC which gets IProps 
//interface (so we could pass on properties) and a list of
//properties we could use inside the component - it is called
//properties deconstruction

//const ActivityList: React.FC<IProps> = ({
    //activities,     //i'm keeping this to show the state 
    //selectActivity, //before mobx refactoring
    //deleteActivity, //before mobx refactoring
    //submitting, //before mobx refactoring
    //target //before mobx refactoring
*/

const ActivityList: React.FC = () => {
    const rootStore = useContext(RootStoreContext);
    const {activitiesByDate} = rootStore.activityStore; //destructure of parameters
    return ( //we want to group our activities by date, using the method we created in activityStore
    <Fragment>
        {activitiesByDate.map(([group, activities])=> (
            <Fragment key={group}>
                <Label size='large' color='blue'>
                    {format(new Date(group), 'eeee do MMMM')}
                </Label>
                <Item.Group divided>
                {activities.map(activity => (
                    <ActivityListItem key={activity.id} activity={activity} />
                ))}
                </Item.Group>
            </Fragment>
        ))}
    </Fragment>
    )
}

//export default ActivityList
export default observer(ActivityList)
