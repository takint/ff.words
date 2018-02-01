import { combineReducers } from 'redux';
import { reducer as formReducer } from 'redux-form';
import EntryReducer from './entry-reducer';

const reducers = {
    entryStore: EntryReducer,
    form: formReducer
};

const rootReducer = combineReducers(reducers);

export default rootReducer;