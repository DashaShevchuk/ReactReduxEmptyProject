import CategoryService from '../service';
import * as types from '../types';

export const categoryActions = {
    started: () => {
        return {
            type: types.CATEGORY_STARTED
        }
    },

    success: (data) => {
        return {
            type: types.CATEGORY_SUCCESS,
            payload: data
        }
    },

    failed: (response) => {
        return {
            type: types.CATEGORY_FAILED,
            errors: response.data
        }
    }
}

export const setErrors = (errors) => {
    return (dispatch) => {
        dispatch(categoryActions.setErrors(errors));
    };
}

export const getCategories = () => {
    return (dispatch) => {
        dispatch(categoryActions.started());
        CategoryService.getAll()
            .then((response)=>
            {
                dispatch(categoryActions.success(response));
                //dispatch(push('/'));
            }, err => {
                dispatch(categoryActions.failed(err.response));
            })
            .catch(err=> {
                console.log('Global Server problen in controler message', err);
            });
    };
}