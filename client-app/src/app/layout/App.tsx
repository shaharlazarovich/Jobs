import React, {Fragment, useEffect, useContext} from 'react';
import { Container } from 'semantic-ui-react'
import NavBar from '../../features/nav/NavBar';
import {observer} from 'mobx-react-lite'; 
import { Route , withRouter, RouteComponentProps, Switch } from 'react-router-dom';
import HomePage from '../../features/home/HomePage';
import JobDashboard from '../../features/jobs/dashboard/JobDashboard';
import JobForm from '../../features/jobs/form/JobForm';
import JobDetails from '../../features/jobs/details/JobDetails';
import NotFound from './NotFound';
import {ToastContainer} from 'react-toastify'
import { RootStoreContext } from '../stores/rootStore';
import LoadingComponent from './LoadingComponent';
import ModalContainer from '../common/modals/ModalContainer';
import ProfilePage from '../../features/profiles/ProfilePage';
import PrivateRoute from './PrivateRoute';

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
            <PrivateRoute exact path='/jobs' component={JobDashboard} />
            <PrivateRoute path='/jobs/:id' component={JobDetails} />
            <PrivateRoute 
              key={location.key} //we use the key param to reload a new component each refresh to allow switching between edit and create
              path={['/createJob', '/manage/:id']} 
              component={JobForm} 
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

export default withRouter(observer(App));//the withRouter is a higher level component that allows access here to all the route objects (like location) 
