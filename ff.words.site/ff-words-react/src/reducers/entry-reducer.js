// const defaultState = {
//     entries: []
// };

const entryDefaultState = {
    entries: [],
    entry: {title:"", content: ""},
    loading: false,
    errors: {}
};

export default (state=entryDefaultState, action={}) => {
    switch(action.type){
        case 'FETCH_ENTRY_PENDING':
            return {
                ...state,
                loading: true,
                entry: {title: {}}
            };
        case 'FETCH_ENTRY_FULFILLED':
            return {
                ...state,
                entry: action.payload.data,
                errors: {},
                loading: false
            };
        case 'FETCH_ENTRIES_FULFILLED':
            return {
                ...state,
                entries: action.payload.data.data || action.payload.data
            };
        case 'FETCH_ENTRIES':
            return {
                ...state,
                entries: action.payload
            };
        case 'NEW_ENTRY':
            return {
                ...state,
                entry: {title:"", content: ""}
            };
        case 'UPDATE_ENTRY_PENDING':
            return {
                ...state,
                loading: true
            };
        case 'UPDATE_ENTRY_FULLFILLED':
            const entry = action.payload.data;
            return {
                ...state,
                entries: state.entries.map(item => item.id === entry.id ? entry : item),
                errors: {},
                loading: false
            };
        case 'UPDATE_ENTRY_REJECTED':
            const updateData = action.payload.response.data;
            const updateErrors = { global: "Validation Failed", title: { message: updateData.errorMessage } };
            return {
                ...state,
                errors: updateErrors,
                loading: false
            };
        case 'SAVE_ENTRY_PENDING':
            return {
                ...state,
                loading: true
            };
        case 'SAVE_ENTRY_FULLFILLED':
            return {
                ...state,
                entries: [...state.entries, action.payload.data]
            };
        case 'SAVE_ENTRY_REJECTED':
            const data = action.payload.response.data;
            const errors = { global: "Validation Failed", title: { message: data.errorMessage } };

            return {
                ...state,
                errors: errors,
                loading: false
            };
        default: return state;
    }
}