import ProductCreatePage from './scenes/ProductCreatePage';
import { createProduct } from './actions';

import { connect } from 'react-redux';
import PropTypes from 'prop-types';

const mapState = (state) => {
    return {
        loading: state.products.create.loading,
        errors: state.products.create.errors
    }
}

ProductCreatePage.propTypes = {
    loading: PropTypes.bool.isRequired,
    errors: PropTypes.bool.isRequired,
    createProduct: PropTypes.func.isRequired,
    
};

const ProductCreate = ProductCreatePage;
export default connect(mapState, {createProduct})(ProductCreate);