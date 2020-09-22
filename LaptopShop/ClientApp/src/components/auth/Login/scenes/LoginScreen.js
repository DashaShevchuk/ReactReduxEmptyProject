import React, { Component } from "react";


import TextFieldGroup from "../../../common/TextFieldGroup";
import { validateFields } from "./validate";
import AlertGroup from "../../../common/AlertGroup";
import RecaptchaGroup from "../../../common/recaptcha/RecaptchaGroup";

import './LoginScreen.scss';

const recaptchaRef = React.createRef();

export class LoginScreen extends Component {
  state = {
    email: "",
    password: "",
    recaptchaToken: "",
    errors: {
      showCaptcha: false
    },
    loading: false,
    
  };

  static getDerivedStateFromProps(nextProps, prevState) {
    return { 
      loading: nextProps.loading, 
      errors: nextProps.errors,
    };
  }

  setStateByErrors = (name, value) => {
    if (!!this.state.errors[name]) {
      let errors = Object.assign({}, this.state.errors);
      delete errors[name];
      this.setState({
        [name]: value,
        errors
      });
    } else {
      this.setState({ [name]: value });
    }
  };

  handleChange = e => {
    this.setStateByErrors(e.target.name, e.target.value);
  };

  handleSubmit = e => {
    e.preventDefault();
    const fields = this.state;
    let errors = validateFields(fields);

    const isValid = Object.keys(errors).length === 0;

    if (isValid) {
      this.props.loginUser(fields);
    } else {
      this.props.setErrors(errors);
    }
    if(this.state.errors.showCaptcha)
    {
      recaptchaRef.current.reset();
      this.setState({
        recaptchaToken:""
      })
    }
  };

  handleChangeRecaptcha=(e)=> {
    this.setState({
      recaptchaToken:!!!e ? "" : e
    })
    this.props.setErrors({recaptchaToken:""});
  }
  render() {
    const { email, password, errors, loading } = this.state;

    return (
      <div>
        {
            <div className="p-0 loginform">
              <h1 className="d-flex justify-content-center">Логін</h1>

              {errors.invalid && (
                <AlertGroup title={errors.invalid} alertColor="alert-danger" />
              )}

              <form name="form" onSubmit={this.handleSubmit}>
                <TextFieldGroup
                  field="email"
                  label="Електронна пошта"
                  value={email}
                  error={errors.email}
                  onChange={this.handleChange}
                  type="email"
                  placeholder="Почта"
                  isShowLabel={false}
                />

                <TextFieldGroup
                  field="password"
                  label="Пароль"
                  value={password}
                  error={errors.password}
                  onChange={this.handleChange}
                  type="password"
                  placeholder="Пароль"
                  isShowLabel={false}
                />
                { errors.showCaptcha && 
                  <RecaptchaGroup 
                    ref={recaptchaRef}
                    onChange={this.handleChangeRecaptcha} 
                    error={errors.recaptchaToken} 
                  />
                }

                <div className="form-group">
                  <button
                    disabled={
                      loading ||
                      email.trim() === "" ||
                      password.trim() === ""
                    }
                    className="btn btn-primary btn-block"
                  >
                    {loading && <span className="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>}
                    Вхід
              </button>
                </div>
              </form>
            </div>
        }
      </div>
    );
  }
}

export default LoginScreen;
