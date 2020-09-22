import CategoriesPage from './scenes/CategoriesPage';
import { getCategories } from './actions';

import { connect } from 'react-redux';
import PropTypes from 'prop-types';

const mapState = (state) => {
    return {
        loading: state.categories.loading,
        list: state.categories.data,
        errors: state.categories.errors
    }
}

CategoriesPage.propTypes = {
    loading: PropTypes.bool.isRequired,
    getCategories: PropTypes.func.isRequired,
    
};

const Categories = CategoriesPage;
export default connect(mapState, {getCategories})(Categories);