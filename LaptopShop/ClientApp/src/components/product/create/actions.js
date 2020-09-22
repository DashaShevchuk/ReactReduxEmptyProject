import ProductsService from '../service';
import * as types from '../types';

import { push } from 'connected-react-router';

export const createProductActions = {
    started: () => {
        return {
            type: types.CREATE_PRODUCT_STARTED
        }
    },

    success: (data) => {
        return {
            type: types.CREATE_PRODUCT_SUCCESS,
            payload: data
        }
    },

    failed: (response) => {
        return {
            type: types.CREATE_PRODUCT_FAILED,
            errors: response.data
        }
    }
}

export const createProduct = (model) => {
    return (dispatch) => {
        dispatch(createProductActions.started());
        ProductsService.create(model)
            .then((response)=>
            {
                dispatch(createProductActions.success(response));
                //dispatch(push('/'));
            }, err => {
                dispatch(createProductActions.failed(err.response));
            })
            .catch(err=> {
                console.log('Global Server problen in controler message', err);
            });
    };
}