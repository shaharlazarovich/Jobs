import axios, {AxiosResponse} from 'axios';
//import { IActivity, IActivitiesEnvelope } from '../models/activity';
import { IJob, IJobsEnvelope } from '../models/job';
import { history } from '../common/util/history'; 
//import {createBrowserHistory} from 'history'
import { toast } from 'react-toastify';
import { IUser, IUserFormValues } from '../models/users';
import { IProfile, IPhoto } from '../models/profile';


//we don't need to explicitely name index - its the default

//axios.defaults.baseURL = 'http://localhost:5000/api';
axios.defaults.baseURL = process.env.REACT_APP_API_URL;

//axios interceptors can handle the request - as well as the response
//here, we check if we already have a stored token, and if we do - we
//send it over in our authorization header's bearer token
axios.interceptors.request.use((config) => {
    const token = window.localStorage.getItem('jwt');
    if (token) config.headers.Authorization = `Bearer ${token}`;
    return config;
}, error => {
    return Promise.reject(error);
})

//axios interceptors catch the server response before
//it hits the ui flow, so we could do something about it
axios.interceptors.response.use(undefined,error => {
    if (error.message === 'Network Error' && !error.response) {
        toast.error('Network error')
    }
    const {status, data, config, headers} = error.response; //this way we are deconstructing the 
    //error response parameters so we don't need to type it every time
    if (status === 404) {
        history.push('/notfound');
    }
    if (status === 401 && 
        String(headers['www-authenticate']).startsWith('Bearer error="invalid_token", error_description="The token expired')) 
        {
            window.localStorage.removeItem('jwt');
            history.push('/');
            toast.info('Your session has expired, please login again');
        }
    if (status === 400 && config.method === 'get' && data.errors.hasOwnProperty('id')){
        //the combination of these 3 conditions is most likely indication that we have
        //a request with the wrong guid of the activity
        history.push('/notfound');
    }
    if (status === 500) {
        toast.error('Server error');
    }
    throw error.response;
})

const responseBody = (response: AxiosResponse) => response.data;

//we're adding here the sleep methos in order to simulate server
//slow response/internet slow connection on production - this is
//a good practice for development phase to add delay for loading indicators
//we are using a technique called Currying which turns a function 
//with multiple arguments into a sequence of nesting functions
const sleep = (ms: number) => (response: AxiosResponse) => 
    new Promise<AxiosResponse>(resolve => setTimeout(() => resolve(response), ms));

const requests = {
    //the below get request is the one with the artificial delay
    //in order to see loading times
    get: 
        (url: string) => 
        axios.get(url)
        .then(sleep(1000))
        .then(responseBody),
    post: 
        (url: string, body: {}) => 
        axios.post(url, body)
        .then(sleep(1000))
        .then(responseBody),
    put: 
        (url: string, body: {}) => 
        axios.put(url, body)
        .then(sleep(1000))
        .then(responseBody),
    del: 
        (url: string) => 
        axios.delete(url)
        .then(sleep(1000))
        .then(responseBody),
    postForm: 
        (url: string, file: Blob) => {
        let formData = new FormData();
        formData.append('File', file);
        return axios.post(url, formData, {
            headers: {'Content-type': 'multipart/form-data'}
        }).then(responseBody)
    }
}

const Jobs = {
    list: 
        (params: URLSearchParams): Promise<IJobsEnvelope> => 
        axios.get('/jobs', {params: params})
        .then(sleep(1000))
        .then(responseBody),
    details: (id: string) => requests.get(`/jobs/${id}`),
    create: (job: IJob) => requests.post('/jobs', job),
    update: (job: IJob) => requests.put(`/jobs/${job.id}`, job),
    delete: (id: string) => requests.del(`/jobs/${id}`),
}


const User = {
    current: (): Promise<IUser> => requests.get('/user'),
    login: (user: IUserFormValues): Promise<IUser> => requests.post(`/user/login`, user),
    register: (user: IUserFormValues): Promise<IUser> => requests.post(`/user/register`, user),
}

const Profiles = {
     get: (username: string): Promise<IProfile> => requests.get(`/profiles/${username}`),
     uploadPhoto: (photo: Blob): Promise<IPhoto> => requests.postForm(`/photos`, photo) ,
     setMainPhoto: (id: string) => requests.post(`/photos/${id}/setMain`, {}),
     deletePhoto: (id: string) => requests.del(`/photos/${id}`),
     updateProfile: (profile: Partial<IProfile>) => requests.put(`/profiles`, profile),
     follow: (username: string) => requests.post(`/profiles/${username}/follow`, {}),
     unfollow: (username: string) => requests.del(`/profiles/${username}/follow`),
     listFollowings: (username: string, predicate: string) =>
        requests.get(`/profiles/${username}/follow?predicate=${predicate}`),
     listActivities: (username: string, predicate: string) => 
        requests.get(`/profiles/${username}/activities?predicate=${predicate}`),
}

export default {
    User,
    Profiles,
    Jobs
}