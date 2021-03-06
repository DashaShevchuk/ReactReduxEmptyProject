import jwt from 'jsonwebtoken';
import { refreshToken } from '../../components/auth/RefreshToken/actions';

export default function refreshTokenMiddleware(){
    return ({ dispatch, getState }) => next => (action) => {
        
        if (typeof action === 'function') {
            const token = jwt.decode(localStorage.getItem('authToken'))
            const refreshThreshold = (Math.round(Date.now() / 1000));
            if (token && refreshThreshold > token.exp) {
                refreshToken(dispatch).then(() => {
                    return next(action)
                }, err=> { throw err; })
                .catch((err) => {
                });
            }
            else{
                return next(action);
            }
        }
        else{
            return next(action);
        }
        
    }
}