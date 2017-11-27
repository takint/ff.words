const defaultState = {
    entries: []
};

export default (state=defaultState, action={}) => {
    switch(action.type){
        case 'FETCH_ENTRIES':
            return {
                ...state,
                entries: action.payload.data.data ||
                action.payload.data //action.payload
            };
        default: return state;
    }
}