import React, {Fragment, useEffect, useContext} from 'react';
import { Container } from 'semantic-ui-react'
import NavBar from '../../features/nav/NavBar';
import {observer} from 'mobx-react-lite'; 
import { Route , withRouter, RouteComponentProps, Switch } from 'react-router-dom';
import HomePage from '../../features/home/HomePage';
import ActivityDashboard from '../../features/jobs/dashboard/JobDashboard';
import ActivityForm from '../../features/activities/form/ActivityForm';
import ActivityDetails from '../../features/activities/details/ActivityDetails';
import NotFound from './NotFound';
import {ToastContainer} from 'react-toastify'
import { RootStoreContext } from '../stores/rootStore';
import LoadingComponent from './LoadingComponent';
import ModalContainer from '../common/modals/ModalContainer';
import ProfilePage from '../../features/profiles/ProfilePage';
import PrivateRoute from './PrivateRoute';

{
//import LoadingComponent from './LoadingComponent';
//import ActivityStore from '../stores/activityStore';
//import React, {Component} from 'react'; //we don't want to use class components anymore

//class App extends Component<{}, IState>{
//since we want to use functional components instead
//of class components - we'll comment out the class,
//as well as comment out imprting component - we'll
//bring useState instead - as we'll use react hooks -
//one for the state, and one for sideEffects - sideEffects
//are the react events like "component did mount" "component did update" etc.

//interface IState {
// activities: IActivity[]
//
  /* no need for all those state objects after the mobx refactoring
  //state management - each of the below consts represent
  //a state object we manage
  const [activities, setActivities] = useState<IActivity[]>([])
  const [selectedActivity, setSelectedActivity] = useState<IActivity | null>(null);
  const [editMode,setEditMode] = useState(false);
  const [loading,setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [target, setTarget] = useState('');

  //no need for these handlers after the mobx refactoring

  //the below is a list of handlers to deal with various
  //scenarios in the UI

  //the event: SyntheticEvent<HTMLButtonElement> is used to identify
  //the click on the delete button so to solve the problem when
  //all buttons (like edit,submit etc) showing loading together
  //with the delete - by identfying which button was clicked, we
  //can isolate it and show loader only on this button
  const handleDeleteActivity = (event: SyntheticEvent<HTMLButtonElement>, id: string) => {
    setSubmitting(true);
    setTarget(event.currentTarget.name);
    agent.Activities.delete(id).then(()=> {
      setActivities([...activities.filter(a=>a.id !==id)])
    }).then(() => setSubmitting(false))
  }
  const handleSelectActivity = (id: string) => {
    setSelectedActivity(activities.filter(a => a.id === id)[0])
    setEditMode(false)
  }
  const handleOpenCreateForm = () => {
    setSelectedActivity(null);
    setEditMode(true);
  }
    const handleCreateActivity = (activity: IActivity) => {
    setSubmitting(true);
    agent.Activities.create(activity).then(() => {
      setActivities([...activities, activity])//... is the spread operator - The spread operator can be used 
      //to take an existing array and add another element to it while still preserving the original array
      setSelectedActivity(activity);
      setEditMode(false);
    }).then(() => setSubmitting(false))
  }
    const handleEditActivity = (activity: IActivity) => {
    setSubmitting(true);
    agent.Activities.update(activity).then(() => {
      setActivities([...activities.filter(a=> a.id !== activity.id), activity]) //the filter operator that is
      //applied on the activities will make sure we edit only our current activity
      setSelectedActivity(activity);
      setEditMode(false);
    }).then(() => setSubmitting(false))

  }
  */
  //i'm keeping this old version of useEffects to show the process
  //of refactoring before we used mobx store
  //useEffect(() => {
  //  agent.Activities.list()
  //    .then(response => {
  //      let activities: IActivity[] = [];
  //      response.forEach(activity => {
  //        activity.date = activity.date.split('.')[0];
  //        activities.push(activity);
  //      })
  //       setActivities(activities)
  //    }).then(() => setLoading(false));
  //}, []); //this empty array is neccessary to avoid this effect
  // from running any time the component renders - this useEffect
  //holds several events within - didmount, didupdate, didunmount etc.
  //each one will cause a re-run if we don't provide this additional
  //parameter of empty array}
  //if (loading) return <LoadingComponent content='Loading Activities...' />
  //i'm keeping the above line as indication to show before the refactoring with mobx

  //readonly state: IState = {
  // activities: [], 
  //};

  // componentDidMount(){
  //   axios.get<IActivity[]>('http://localhost:5000/api/activities')
  //     .then((response) => {
  //       this.setState({
  //         activities: response.data
  //       })
  //     })
    
  // }

  //render(){

  /*return (
      <Fragment>
        <NavBar 
          //openCreateForm={handleOpenCreateForm} //no need for the property after mobx
        />
        <Container style={{marginTop: '7em'}}>
          <ActivityDashboard 
            //activities={activities} - i keep this as example before the mobx refactoring
            //activities={activityStore.activities} 
            //selectActivity={handleSelectActivity} //keeping this to show how it was before mobx
            //selectedActivity={selectedActivity!} 
            //the ! allow it to be eaither activity or null - as defined above
            //this is in case we don't define activity | null in the dashboard
            //selectedActivity={selectedActivity} //i'm keeping selectedActivity&EditMode to show
            //editMode={editMode}                 //how it was before the refactoring to mobx
            //setEditMode={setEditMode} //i'm keeping this to show how it was before mobx
            //setSelectedActivity={setSelectedActivity}//im keeping this to show the status before mobx refactoring
            //createActivity={handleCreateActivity}//keeping this to show how it was before mobx
            //editActivity={handleEditActivity}//i'm keeping this to show how it was before mobx
            //deleteActivity={handleDeleteActivity}//i'm keeping this to show how it was before mobx
            //submitting={submitting}//i'm keeping this to show how it was before mobx
            //target={target}//i'm keeping this to show how it was before mobx 
          />
        </Container>
      </Fragment>
    );*/

  /*i'm keeping this to show the state before routes refactoring
  return (
    <Fragment>
      <NavBar />
      <Container style={{marginTop: '7em'}}>
        <ActivityDashboard />
      </Container>
    </Fragment>
  );*/
}
const App: React.FC<RouteComponentProps> = ({location}) => {
  //we'd like to be logged in automatically if we already have a token.
  const rootStore = useContext(RootStoreContext);
  const { setAppLoaded, token, appLoaded } = rootStore.commonStore;
  const {getUser} = rootStore.userStore;

  useEffect(() => {
    if (token) {
      getUser().finally(() => setAppLoaded());
    } else {
      setAppLoaded();
    }
  }, [getUser, setAppLoaded, token])

  if (!appLoaded) return <LoadingComponent content='Loading app...' />
  
  return (
    <Fragment>
      <ModalContainer />
      <ToastContainer position='bottom-right' />
      <Route exact path='/' component={HomePage} />
      <Route path={`/(.+)`} render={() => (
        <Fragment>
          <NavBar />
          <Container style={{marginTop: '7em'}}>
            <Switch
            //switch makes sure only one of the routes is executed
            //in a given time - otherwise the last route of NotFound
            //will always appear
            >
            <PrivateRoute exact path='/activities' component={ActivityDashboard} />
            <PrivateRoute path='/activities/:id' component={ActivityDetails} />
            <PrivateRoute 
              key={location.key} //we use the key param to reload a new component each refresh to allow switching between edit and create
              path={['/createActivity', '/manage/:id']} 
              component={ActivityForm} 
            />
            <PrivateRoute path='/profile/:username' component={ProfilePage} />
            <Route component={NotFound} />
            </Switch>

          </Container>
        </Fragment>
      )}/>
    </Fragment>
  );

}
//}


//export default App; 
//i'm keeping the above line to show how it looked before 
//the mobx refactroing that turned the app.tsx into an observer
export default withRouter(observer(App));//the withRouter is a higher level component that allows access here to all the route objects (like location) 
