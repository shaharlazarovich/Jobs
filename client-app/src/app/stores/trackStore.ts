import { action } from 'mobx'
import { toast } from 'react-toastify';
import { RootStore } from './rootStore';
import { ITrackEvent } from '../models/trackevent';
import trackAgent from '../api/trackAgent';

const LIMIT = 6; //this is the amount of records per page for the paging methods

//to avoid error about decorators (the @ sign before the observable)
//being expermintal - add this line to the tsconfig.json
//"experimentalDecorators": true
export default class TrackStore {
    rootStore: RootStore;
    constructor(rootStore:RootStore) {
        this.rootStore = rootStore;
    }
    
    @action trackService = async (trackevent: ITrackEvent) => {
        try {
            await trackAgent.TrackEvent.post(trackevent);
            toast.info('load component track sent')
        }
        catch(error) {
            toast.error('problem sending track')
            console.log(error.response); 
        }
    }
    
}
