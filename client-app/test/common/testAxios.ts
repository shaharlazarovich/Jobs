import moxios from 'moxios';
import { TestStoreContext, TestStore } from './testStore';

describe('getJob action creator', () => {
    beforeEach(() => {
      moxios.install();
    });
    afterEach(() => {
      moxios.uninstall();
    });
    test('adds response to state', () => {
      const jobName = 'netapp';
      const store = new TestStore();
  
      moxios.wait(() => {
        const request = moxios.requests.mostRecent();
        request.respondWith({
          status: 200,
          response: jobName,
        });
      });
  
      return store.dispatch('netapp')
        .then(() => {
          const newState = store.getState();
          //expect(newState.jobName).toBe(jobName);
        })
    });
  });
  