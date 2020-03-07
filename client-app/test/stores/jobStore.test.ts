import JobStore from '../../src/app/stores/jobStore';
import {RootStore} from '../../src/app/stores/rootStore';
import { jobMock } from '../common/mocks';
import moxios from 'moxios';
import agentBase from '../../src/app/api/agentBase';

describe('JobStore tests', () => {
    let store: JobStore;

    beforeEach(() => {
        store = new JobStore(new RootStore());
    })

    test('create job', async () => {
        moxios.install(agentBase.axios);
        moxios.wait(() => {
            const req = moxios.requests.mostRecent();
            req.respondWith({
                status: 200,
            })
        });
        await store.createJob({ ...jobMock });

        moxios.uninstall(agentBase.axios);

        expect(store.jobRegistry.get(jobMock.id)).toEqual(jobMock);
    })


})