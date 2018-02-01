//import { entries } from '../entry-data';
import { client } from './index';

const retrieveApi = '/entry/GetEntries';
const getApi = 'entry/GetEntry';
const addEntryApi = '/entry/AddEntry';
const updateEntryApi = 'entry/UpdateEntry';

export function fetchEntries(){
    return dispatch => {
        dispatch({
            type: 'FETCH_ENTRIES',
            payload: client.get(retrieveApi) //entries
        });
    };
};

export function newEntry(){
    return dispatch => {
        dispatch({
            type: 'NEW_ENTRY'
        });
    };
};

export function fetchEntry(id){
    return dispatch => {
        return dispatch({
            type: 'FETCH_ENTRY',
            payload: client.get(`${getApi}?id=${id}`)
        });
    };
};

export function updateEntry(entry) {
    return dispatch => {
        return dispatch({
            type: 'UPDATE_ENTRY',
            payload: client.post(updateEntryApi, entry)
        });
    };
};

export function saveEntry(entry){
    return dispatch => {
        return dispatch({
            type:'SAVE_ENTRY',
            payload: client.post(addEntryApi, entry)
        });
    };
};