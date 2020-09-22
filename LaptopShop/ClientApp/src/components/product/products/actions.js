import ProductsService from '../service';
import * as types from '../types';

import { push } from 'connected-react-router';

export const getProductsActions = {
    started: () => {
        return {
            type: types.GET_PRODUCTS_STARTED
        }
    },

    success: (data) => {
        return {
            type: types.GET_PRODUCTS_SUCCESS,
            payload: data
        }
    },

    failed: (response) => {
        return {
            type: types.GET_PRODUCTS_FAILED,
            errors: response.data
        }
    }
}

export const getProducts = (model) => {
    return (dispatch) => {
        dispatch(getProductsActions.started());
        ProductsService.get(model)
            .then((response)=>
            {
                dispatch(getProductsActions.success(response));
                //dispatch(push('/'));
            }, err => {
                dispatch(getProductsActions.failed(err.response));
            })
            .catch(err=> {
                console.log('Global Server problen in controler message', err);
            });
    };
}