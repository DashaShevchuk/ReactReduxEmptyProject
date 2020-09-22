import React from "react";
import PropTypes from "prop-types";
import { Route, Redirect } from "react-router";
import { connect } from "react-redux";

const UserRoute = ({ iAuthentificated ,component: Component, ...rest }) => (
  <Route
    {...rest}
    render={props =>
      iAuthentificated ? (
          <Component {...props} />
      ) : (
        <Redirect to="/login" />
      )
    }
  />
);
UserRoute.propTypes = {
  component: PropTypes.any.isRequired,
  iAuthentificated: PropTypes.bool.isRequired
};
function mapStateToProps(state) {
  return {
    iAuthentificated: state.auth.isAuthenticated
  };
}

export default connect(mapStateToProps)(UserRoute);