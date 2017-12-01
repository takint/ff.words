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
        case 'FETCH_ENTRIES_FULFILLED':
            return {
                ...state,
                entries: action.payload.data.data || action.payload.data//action.payload
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
            const {"title": title, "content": content} = data.errors;
            const errors = { global: data.message, title, content };

            return {
                ...state,
                errors: errors,
                loading: false
            };
        default: return state;
    }
}