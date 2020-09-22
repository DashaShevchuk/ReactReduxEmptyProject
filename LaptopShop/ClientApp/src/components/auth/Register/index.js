import RegisterPage from './scenes/RegisterPage';
import { registerUser } from './actions';

import { connect } from 'react-redux';
import PropTypes from 'prop-types';

const mapState = (state) => {
    return {
        loading: state.register.loading,
        errors: state.register.errors,
        captchaKey: state.captcha.key.data,
    }
}

RegisterPage.propTypes = {
    loading: PropTypes.bool.isRequired,
    captchaKey: PropTypes.string.isRequired,
    registerUser: PropTypes.func.isRequired,
    
};
const Register = RegisterPage;
export default connect(mapState, {registerUser})(Register);
