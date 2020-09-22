import React, { Component, Suspense } from 'react';
import { Route, Switch } from 'react-router-dom';
import Layout from './Layout';
// routes config
import adminRoutes from '../../../routes/adminRoutes';

class AdminLayout extends Component {

  render() {
    console.log('-------this-----', this.props);
    return (
      <Layout>
        <Suspense fallback={<div>Загрузка...</div>}>
          <Switch>
            {adminRoutes.map((route, idx) => {
              return  (
                <Route
                  key={idx}
                  path={route.path}
                  exact={route.exact}
                  name={route.name}
                  render={props => (
                    <route.component {...props} />
                  )} />
              );
            })}
          </Switch>
        </Suspense>
      </Layout>
    );
  }
}
export default AdminLayout;