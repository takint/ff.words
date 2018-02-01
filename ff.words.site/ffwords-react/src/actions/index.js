import axios from 'axios';

const apiEndpoint = 'http://localhost:52707/api';

export const client = axios.create({
    baseURL: apiEndpoint,
    headers: {
        'Content-Type': 'application/json'
    }
});