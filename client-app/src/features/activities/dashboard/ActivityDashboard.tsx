import React, { useEffect, useContext, useState }  from 'react';
import { Grid, Button, Loader } from 'semantic-ui-react';
import ActivityList from './ActivityList';
import { observer } from 'mobx-react-lite';
//import LoadingComponent from '../../../app/layout/LoadingComponent';
import { RootStoreContext } from '../../../app/stores/rootStore';
import InfiniteScroll from 'react-infinite-scroller';
import ActivityFilters from './ActivityFilters';
import ActivityListItemPlaceholder from './ActivityListItemPlaceHolder';

{
/*
//properties are passed to ui components via an interface,
//so we define the interface here, and we can use this
//property in other components or in the containing app.tsx
interface IProps {
    //activities: IActivity[];//keeping this to show how it was before mobx
    //selectActivity: (id: string) => void; //keeping this to show how it was before mobx
    //selectedActivity: IActivity | null; //i'm keeping selectedActivity&EditMode to show
    //editMode: boolean; //how it was before the refactoring to mobx
    //setEditMode: (editMode: boolean) => void;//i'm keeping this to show how it was before mobx
    //setSelectedActivity: (activity: IActivity | null) => void;//im keeping this to show the status before mobx refactoring
    //createActivity: (activity: IActivity) => void;//keeping this to show how it was before mobx
    //editActivity: (activity: IActivity) => void;//i'm keeping this to show how it was before mobx
    //deleteActivity: (e: SyntheticEvent<HTMLButtonElement>, id: string) => void;//keeping this to show how it was before mobx
    //submitting: boolean;//keeping this to show how it was before mobx
    //target: string;//keeping this to show how it was before mobx
}

//we are using a React functional component. the syntax
//below include the component definition as an arrow function,
//but also type declaration of React.FC which gets IProps 
//interface (so we could pass on properties) and a list of
//properties we could use inside the component - it is called
//properties deconstruction

//const ActivityDashboard: React.FC<IProps> = ({
    //activities, //i'm keeping this to show how it was before mobx
    //selectActivity, //i'm keeping this to show how it was before mobx
    //selectedActivity, //i'm keeping selectedActivity&EditMode to show
    //editMode,         //how it was before the refactoring to mobx
    //setEditMode,//i'm keeping this to show how it was before mobx
    //setSelectedActivity,//im keeping this to show the status before mobx refactoring
    //createActivity,//keeping this to show how it was before mobx
    //editActivity,//i'm keeping this to show how it was before mobx
    //deleteActivity,//i'm keeping this to show how it was before mobx
    //submitting,//i'm keeping this to show how it was before mobx
    //target//i'm keeping this to show how it was before mobx

const ActivityDashboard: React.FC = () => {
    const activityStore = useContext(ActivityStore);
    const {editMode, activity} = activityStore;//destructure of parameters
*/

    /*im keeping this to show the state before routing refactoring
    return (
        <Grid>
            <Grid.Column width={10}>
                <ActivityList />
            </Grid.Column>
            <Grid.Column width={6}>
                {activity && !editMode && (//the && means display the activity only if editMode is true and selectedActivity is not null
                    <ActivityDetails />
                )}
                {editMode && (//the && means display the form only if editMode is true
                    <ActivityForm 
                        key={activity && activity.id || 0} //adding a key to the form reloads it every time the key changes
                        activity={activity!} 
                    />
                )}
            </Grid.Column>
        </Grid>
    )
    
    
    return (
        <Grid>
            <Grid.Column width={10}>
                <ActivityList  
                    //activities={activities}        //im keeping this to show the 
                    //selectActivity={selectActivity}//status before mobx refactoring 
                    //deleteActivity={deleteActivity}//status before mobx refactoring 
                    //submitting={submitting}//status before mobx refactoring 
                    //target={target}//status before mobx refactoring 
                 />
            </Grid.Column>
            <Grid.Column width={6}>
                {selectedActivity && !editMode && //the && means display the activity only if editMode is true and selectedActivity is not null
                    <ActivityDetails 
                        //activity={selectedActivity} //im keeping this to show the status before mobx refactoring
                        //setEditMode={setEditMode}//im keeping this to show the status before mobx refactoring
                        //setSelectedActivity={setSelectedActivity}//im keeping this to show the status before mobx refactoring
                    />
                }
                {editMode && //the && means display the form only if editMode is true
                    <ActivityForm 
                        key={selectedActivity && selectedActivity.id || 0} //adding a key to the form reloads it every time the key changes
                        activity={selectedActivity!} //adding the exclamation mark tells types script we have checked for null value
                        //so it will empty the state object when we want to create a new activity
                        //setEditMode={setEditMode} //i'm keeping this to show how it was before mobx                        
                        //createActivity={createActivity}//keeping this to show how it was before mobx
                        //editActivity={editActivity}//i'm keeping this to show how it was before mobx
                        //submitting={submitting}//i'm keeping this to show how it was before mobx
                    />
                }
            </Grid.Column>
        </Grid>
    )*/
}
const ActivityDashboard: React.FC = () => {
    const rootStore = useContext(RootStoreContext); //this is called global state from our state store of mobx
    const { loadActivities, loadingInitial, setPage, page, totalPages} = rootStore.activityStore;
    const [loadingNext, setLoadingNext] = useState(false); //this is called local state which we take from useContext
    
    const handleGetNext = () => {
        setLoadingNext(true);
        setPage(page + 1);
        loadActivities().then(() => setLoadingNext(false));
    }

    //useEffect is a React hook that build our activities list
    useEffect(() => {
      loadActivities();
    }, [loadActivities]); //now, instead of leaving this empty, we need to declare here every dependency useEffect has

    //we want the loading indication to appear on initial page load - but not whenever we are paging
    //if (loadingInitial && page === 0) return <LoadingComponent content='Loading Activities...' />

    return (
    <Grid>
        <Grid.Column width={10}>
            {loadingInitial && page === 0 ? (<ActivityListItemPlaceholder />) :
            (<InfiniteScroll
                pageStart = {0}
                loadMore = {handleGetNext}
                hasMore = {!loadingNext && page + 1 < totalPages}//we need to check we don't have loading next to avoid duplicates, 
                //and we must add 1 to page - since it starts with zero
                initialLoad = {false}
            >
                <ActivityList />
            </InfiniteScroll>
            )}
        </Grid.Column>
        <Grid.Column width={6}>
            <ActivityFilters />
        </Grid.Column>
        <Grid.Column width={10}>
            <Loader active={loadingNext} />
        </Grid.Column>
    </Grid>
);
}

//export default ActivityDashboard
export default observer(ActivityDashboard)
