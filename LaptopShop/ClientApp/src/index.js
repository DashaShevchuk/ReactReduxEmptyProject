import 'bootstrap/dist/css/bootstrap.css';
import 'font-awesome/css/font-awesome.min.css';
import 'primereact/resources/themes/nova-light/theme.css';
import 'primereact/resources/primereact.min.css';
import 'primeicons/primeicons.css';
import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { ConnectedRouter } from 'connected-react-router';

import configureStore, {history} from './store/configureStore';
import App from './App';
import registerServiceWorker from './registerServiceWorker';

import * as loginActions from './components/auth/Login/actions';


// Get the application-wide store instance, prepopulating with state from the server where available.
const initialState = window.initialReduxState;
const store = configureStore(history, initialState);

if(localStorage.authToken && localStorage.refreshToken) {
  let data = {
    token: localStorage.authToken, 
    refreshToken: localStorage.refreshToken
  };
  loginActions.loginByJWT(data, store.dispatch);
}

const rootElement = document.getElementById('root');

ReactDOM.render(
  <Provider store={store}>
    <ConnectedRouter history={history}>
      <App />
    </ConnectedRouter>
  </Provider>,
  rootElement);

registerServiceWorker();
