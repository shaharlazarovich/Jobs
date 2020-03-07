import agent from './agentBase';
import { ITrackEvent } from '../models/trackevent';

const requests = agent.requests;

const TrackEvent = {
    post: (trackevent: ITrackEvent) => requests.post('/trackevents', trackevent),
}

export default {
    TrackEvent
}