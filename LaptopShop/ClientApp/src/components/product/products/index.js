import ProductsPage from './scenes/ProductsPage';
import {  getProducts } from './actions';

import { connect } from 'react-redux';
import PropTypes from 'prop-types';

const mapState = (state) => {
    return {
        loading: state.product.list.loading,
        list: state.product.list.data,
        errors: state.product.list.errors
    }
}

ProductsPage.propTypes = {
    loading: PropTypes.bool.isRequired,
    getProducts: PropTypes.func.isRequired,
    
};

const Products = ProductsPage;
export default connect(mapState, {getProducts})(Products);