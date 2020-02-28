import React, { useEffect, useContext, useState }  from 'react';
import { Grid, Loader } from 'semantic-ui-react';
import JobList from './JobList';
import { observer } from 'mobx-react-lite';
import { RootStoreContext } from '../../../app/stores/rootStore';
import InfiniteScroll from 'react-infinite-scroller';
import JobFilters from './JobFilters';
import JobListItemPlaceholder from './JobListItemPlaceHolder';
import { TrackEvent } from '../../../app/models/trackevent'
import uuid from 'uuid';
import moment from 'moment';

const JobDashboard: React.FC = () => {
    const rootStore = useContext(RootStoreContext); //this is called global state from our state store of mobx
    const { loadJobs, loadingInitial, setPage, page, totalPages} = rootStore.jobStore;
    const { trackService } = rootStore.trackStore;
    const [loadingNext, setLoadingNext] = useState(false); //this is called local state which we take from useContext
    
    const handleGetNext = () => {
        setLoadingNext(true);
        setPage(page + 1);
        loadJobs().then(() => setLoadingNext(false));
    }

    let trackEvent = new TrackEvent();
    trackEvent = {
        id: uuid(),
        actionDate: moment().toDate(),
        source: "JobDashboard",
        event: "loadcomponent",
    }


    //useEffect is a React hook that build our jobs list
    useEffect(() => {
      trackService(trackEvent);
      loadJobs();
    }, [loadJobs]); //now, instead of leaving this empty, we need to declare here every dependency useEffect has

    return (
    <Grid data-test="component-job-dashboard">
        <Grid.Column width={10}>
            {loadingInitial && page === 0 ? (<JobListItemPlaceholder />) :
            (<InfiniteScroll
                pageStart = {0}
                loadMore = {handleGetNext}
                hasMore = {!loadingNext && page + 1 < totalPages}//we need to check we don't have loading next to avoid duplicates, 
                //and we must add 1 to page - since it starts with zero
                initialLoad = {false}
            >
                <JobList />
            </InfiniteScroll>
            )}
        </Grid.Column>
        <Grid.Column width={6}>
            <JobFilters />
        </Grid.Column>
        <Grid.Column width={10}>
            <Loader active={loadingNext} />
        </Grid.Column>
    </Grid>
);
}

//export default ActivityDashboard
export default observer(JobDashboard)
