//import { entries } from '../entry-data';
import { client } from './index';

const retrieveApi = '/entry/GetEntries';
const addEntryApi = '/entry/AddEntry'

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

export function saveEntry(entry){
    return dispatch => {
        return dispatch({
            type:'SAVE_ENTRY',
            payload: client.post(addEntryApi, entry)
        });
    };
};