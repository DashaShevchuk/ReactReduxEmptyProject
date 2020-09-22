import React, { Suspense, Component } from "react";
import {Redirect, Route, Switch } from "react-router";
// import Layout from "./components/Layout";
import './App.scss';

//routes
import GuestRoute from "./components/routes/GuestRoute";
import UserRoute from "./components/routes/UserRoute";

//components
const Home = React.lazy(() => import("./components/Home"));
const Counter = React.lazy(() => import("./components/Counter"));
const FetchData = React.lazy(() => import("./components/FetchData"));
const ProductsPage = React.lazy(() => import("./components/products"));
const Register = React.lazy(() => import("./components/auth/Register"));
const Login = React.lazy(() => import("./components/auth/Login/scenes"));

// Containers
const DefaultLayout = React.lazy(() => import('./components/containers/DefaultLayout'));
const AdminLayout = React.lazy(() => import('./components/containers/AdminLayout'));

class App extends Component {
    render() {
        return (
            // <Layout>
            <Suspense fallback={<div>Загрузка...</div>}>
                <Switch>
                    <Route path="/admin" name="Admin" render={props => <AdminLayout {...props} />} />

                    <Route path="/" name="Default"
                        render={props => <DefaultLayout {...props} />} />


                    {/* <Route exact path="/" component={Home} />
                        <UserRoute path="/counter" component={Counter} />
                        <UserRoute path="/fetch-data/:startDateIndex?" component={FetchData} />
                        <UserRoute path="/products/:page?" component={ProductsPage} />
                        <GuestRoute path="/register" component={Register} />
                        <GuestRoute path='/login' component={Login} /> */}
                </Switch>
            </Suspense>
            // </Layout>
        );
    }
};
export default App;