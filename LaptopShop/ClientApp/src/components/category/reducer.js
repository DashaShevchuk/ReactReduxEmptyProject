import * as types from './types';

const initialState = {
    loading: false,
    success: false,
    failed: false,
    data: [],
    errors: {
        
    },
    sidemenu: {
        data: [],
        
    }
}

export const categoryReducer = (state = initialState, action) => {
    let newState = state;
    switch (action.type) {
        case types.CATEGORY_STARTED: {
            newState = {
                ...state, 
                loading: true,
                errors: {}
            };
            break;
        }
        case types.CATEGORY_SUCCESS: {
            newState = {...state, loading: false, errors: {}, data: action.payload.data };
            break;
        }
        case types.CATEGORY_FAILED: {
            newState = {
                ...state, 
                loading: false, 
                errors: action.errors,
                data: []
            };
            break;
        }
        default: {
            return state;
        }
    }
    return newState;
}

