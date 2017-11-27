//import { entries } from '../entry-data';
import { client } from './index';

const api = 'http://localhost:52707/api/entry/GetEntries';

export function fetchEntries(){
    return dispatch => {
        dispatch({
            type: 'FETCH_ENTRIES',
            payload: client.get(api) //entries
        });
    };
};