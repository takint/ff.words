import { combineReducers } from 'redux';
import EntryReducer from './entry-reducer';

const reducers = {
    entryStore: EntryReducer
};

const rootReducer = combineReducers(reducers);

export default rootReducer;