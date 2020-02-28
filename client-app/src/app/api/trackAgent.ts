import agent from './agent';
import { ITrackEvent } from '../models/trackevent';

const requests = agent.requests;

const TrackEvent = {
    post: (trackevent: ITrackEvent) => requests.post('/trackevents', trackevent),
}

export default {
    TrackEvent
}