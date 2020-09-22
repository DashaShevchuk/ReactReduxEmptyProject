import RefreshService from './service';
import * as loginActions from '../Login/actions';
import * as types from './types';

export const refreshActions = {
    started: () => {
        return {
            type: types.REFRESH_STARTED
        }
    },

    success: () => {
        return {
            type: types.REFRESH_SUCCESS
        }
    },

    failed: (response) => {
        return {
            type: types.REFRESH_FAILED,
        }
    },
}

export const refreshToken = (dispatch) => {
    dispatch(refreshActions.started())
    return RefreshService.RefreshToken()
        .then((response) => {
            dispatch(refreshActions.success());
            loginActions.loginByJWT(response.data, dispatch);
            return Promise.resolve(response);
        }, err=> { throw err; })
        .catch((err) => {
            dispatch(refreshActions.failed());
            loginActions.logoutByJWT(dispatch);
            return Promise.reject(err);
        })
}