import React, { useContext, useEffect } from 'react'
import { Grid } from 'semantic-ui-react'
import { observer } from 'mobx-react-lite'
import { RouteComponentProps } from 'react-router'
import LoadingComponent from '../../../app/layout/LoadingComponent'
import ActivityDetailedHeader from './ActivityDetailedHeader'
import ActivityDetailedInfo from './ActivityDetailedInfo'
import ActivityDetailedChat from './ActivityDetailedChat'
import ActivityDetailedSidebar from './ActivityDetailedSidebar'
import { RootStoreContext } from '../../../app/stores/rootStore'

/* 
//im keeping this to show the state before mobx
//properties are passed to ui components via an interface,
//so we define the interface here, and we can use this
//property in other components or in the containing app.tsx
interface IProps {
    //activity: IActivity;//im keeping this to show the status before mobx refactoring
    setEditMode: (editMode: boolean) => void;
    setSelectedActivity: (activity: IActivity | null) => void;
}

//we are using a React functional component. the syntax
//below include the component definition as an arrow function,
//but also type declaration of React.FC which gets IProps 
//interface (so we could pass on properties) and a list of
//properties we could use inside the component - it is called
//properties deconstruction

  const ActivityDetails: React.FC<IProps> = ({//im keeping this to show the status before mobx refactoring
    activity, //im keeping this to show the status before mobx refactoring
    setEditMode,//im keeping this to show the status before mobx refactoring
    setSelectedActivity//im keeping this to show the status before mobx refactoring
*/

interface DetailParams {
  id: string
}

const ActivityDetails: React.FC<RouteComponentProps<DetailParams>> = ({
  match,
  history
}) => {
  const rootStore = useContext(RootStoreContext);
  const {
          activity,
          loadActivity,
          loadingInitial
        } = rootStore.activityStore; //destructure of parameters
        
  useEffect(() => {
    loadActivity(match.params.id);
  }, [loadActivity, match.params.id,history])

  if (loadingInitial) return <LoadingComponent content='Loading activity...' />
  if (!activity) return <h2>Activity Not Found</h2>
  return (
    <Grid>
      <Grid.Column width={10}>
        <ActivityDetailedHeader activity={activity} />
        <ActivityDetailedInfo activity={activity} />
        <ActivityDetailedChat />
      </Grid.Column>
      <Grid.Column width={6}>
        <ActivityDetailedSidebar attendees={activity.attendees} />
      </Grid.Column>
    </Grid>
  );
    /*return (//im keeping this to show state before style refactoring
        <Card fluid>
        <Image src={`/assets/categoryImages/${activity!.category}.jpg`} wrapped ui={false} />
        <Card.Content>
          <Card.Header>{activity!.title}</Card.Header>
          <Card.Meta>
            <span>{activity!.date}</span>
          </Card.Meta>
          <Card.Description>
            {activity!.description}
          </Card.Description>
        </Card.Content>
        <Card.Content extra>
          <ButtonGroup widths={2}>
              <Button 
                //onClick={() => openEditForm(activity!.id)} //im keeping this to show state before router refactor
                as={Link} to={`/manage/${activity.id}`}
                basic 
                color='blue' 
                content='Edit'
              />
              <Button 
                //onClick={cancelSelectedActivity} //im keeping this to show state before router refactor
                onClick={() => history.push('/activities')}
                basic 
                color='grey' 
                content='Cancel' 
              />
          </ButtonGroup>
        </Card.Content>
      </Card>
    )*/
}

//export default ActivityDetails
export default observer(ActivityDetails)
