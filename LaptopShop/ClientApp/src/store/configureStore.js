import { applyMiddleware, combineReducers, compose, createStore } from 'redux';
import thunk from 'redux-thunk';
import { connectRouter, routerMiddleware } from 'connected-react-router';
import * as Counter from './Counter';
import * as WeatherForecasts from './WeatherForecasts';
import { createBrowserHistory } from 'history';
import { captchaReducer } from "../components/common/captcha/reducer";
import { authReducer } from '../components/auth/Login/reducer';
import { registerReducer } from '../components/auth/Register/reducer';
//import { productsReducer } from "../components/products/reducer";

import { categoryReducer } from '../components/category/reducer';

import { productReducer } from '../components/product/reducer';

import refreshTokenMiddleware from './middlewares/refreshTokenMiddleware';



// Create browser history to use in the Redux store
const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
export const history = createBrowserHistory({ basename: baseUrl });

export default function configureStore(history, initialState) {
  const reducers = {
    counter: Counter.reducer,
    weatherForecasts: WeatherForecasts.reducer,
    //products: productsReducer,
    auth:authReducer,
    register: registerReducer,
    captcha: captchaReducer,
    category: categoryReducer,
    product: productReducer
  };

  const middleware = [
    refreshTokenMiddleware(),
    thunk,
    routerMiddleware(history)
  ];

  // In development, use the browser's Redux dev tools extension if installed
  const enhancers = [];
  const isDevelopment = process.env.NODE_ENV === 'development';
  if (isDevelopment && typeof window !== 'undefined' && window.devToolsExtension) {
    window.devToolsExtension = window.__REDUX_DEVTOOLS_EXTENSION__;
    enhancers.push(window.devToolsExtension());
  }



  const rootReducer = combineReducers({
    ...reducers,
    router: connectRouter(history)
  });

  return createStore(
    rootReducer,
    initialState,
    compose(applyMiddleware(...middleware), ...enhancers)
  );
}
