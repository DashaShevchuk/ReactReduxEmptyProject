import React from 'react';

const Home = React.lazy(() => import("../components/Home"));
const Login = React.lazy(() => import("../components/auth/Login/scenes"));
const Register = React.lazy(() => import("../components/auth/Register"));
const Products = React.lazy(() => import("../components/product/products"));

const defaultRoutes = [
    { path: '/home', exact: true, name: 'Головна', component: Home  },
    { path: '/login', exact: true, name: 'Вхід', component: Login  },
    { path: '/register', exact: true, name: 'Реєстрація', component: Register  },
    { path: '/products/:category/:page?', exact: true, name: 'Список продуктів', component: Products  }
];
export default defaultRoutes;