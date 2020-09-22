import * as types from './types';
import update from '../../helpers/update';

const initialState = {
    list: {
        data: [],
        currentPage: 1,
        totalPage: 1,
        failed: false,
        loading: false,
        success: false
    },
    create: {
        loading: false,
        success: false,
        failed: false,
        errors: {}
    },
    loading: false,
    success: false,
    failed: false,
    data: [],
    errors: {
        
    },
}

export const productReducer = (state = initialState, action) => {
    let newState = state;
    switch (action.type) {
        
        //--------------------------GET PRODUCTS--------------------------
        case types.GET_PRODUCTS_STARTED: {
            newState = update.set(state, 'list.loading', true);
            newState = update.set(newState, 'list.success', false);
            newState = update.set(newState, 'list.failed', false);
            //newState = update.set(newState, 'list.currentPage', action.payload);
            // newState = {
            //     ...state, 
            //     loading: true,
            //     errors: {}
            // };
            break;
        }
        case types.GET_PRODUCTS_SUCCESS: {
            
            newState = update.set(state, 'list.loading', false);
            newState = update.set(newState, 'list.data', action.payload.data);
            newState = update.set(newState, 'list.failed', false);
            //newState = {...state, loading: false, errors: {}, data: action.payload.data };
            break;
        }
        case types.GET_PRODUCTS_FAILED: {
            newState = update.set(state, 'list.loading', false);
            newState = update.set(newState, 'list.data', []);
            newState = update.set(newState, 'list.failed', true);
            break;
        }

        //--------------------------CREATE PRODUCT--------------------------
        default: {
            return state;
        }
    }
    return newState;
}

