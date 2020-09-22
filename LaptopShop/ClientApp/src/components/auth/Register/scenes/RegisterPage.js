import React, { Component } from "react";
import TextFieldGroup from "../../../common/TextFieldGroup";
import ImageFieldGroupCropper from "../../../common/ImageFieldGroupCropper";
import PhoneFieldGroup from "../../../common/PhoneFieldGroup";
import CaptchaFieldGroup from "../../../common/CaptchaFieldGroup";
import EclipseWidget from '../../../common/eclipse';
import { validateFields } from "./validate";


export class RegisterPage extends Component {
  state = {
    lastName: "",
    firstName: "",
    email: "",
    phone: "",
    photo: "",
    password: "",
    confirmPassword: "",
    captchaText: "",
    errors: this.props.errors,
    loading: this.props.loading,
    captchaKey: this.props.captchaKey

  };

  UNSAFE_componentWillReceiveProps = (nextProps) => {
    console.log('Change props ');
    this.setState({
      loading: nextProps.loading,
      errors: nextProps.errors,
      captchaKey: nextProps.captchaKey
    });
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
  }

  handleChange = e => {
    this.setStateByErrors(e.target.name, e.target.value);
  }

  getCroppedImage = img => {
    if (!!this.state.errors["photo"]) {
      let errors = Object.assign({}, this.state.errors);
      delete errors["photo"];
      this.setState({
        photo: img,
        errors
      });
    } else {
      this.setState({ photo: img });
    }
  };

  handleSubmit = e => {
    e.preventDefault();
    // console.log("--register submit--");
    //const { email, photo } = this.state;
    let errors = validateFields(this.state);
    // let errors = {};

    // if (email === "") errors.email = "Поле не може бути пустим!";
    // if (photo === "") errors.photo = "Закинь фотку!";

    const isValid = Object.keys(errors).length === 0;

    if (isValid) {
      const model = {
        FirstName: this.state.firstName,
        LastName: this.state.lastName,
        Email: this.state.email,
        Phone: this.state.phone,
        Password: this.state.password,
        ConfirmPassword: this.state.confirmPassword,
        Photo: this.state.photo,
        CaptchaText: this.state.captchaText,
        CaptchaKey: this.state.captchaKey,
      };
      this.props.registerUser(model);
      // console.log("Model is Valid");
      // console.log("Model is Valid");
      //ajax axios post
    } else {
      this.setState({ errors });
    }
  };

  render() {
    const {
      lastName,
      firstName,
      email,
      phone,
      photo,
      password,
      confirmPassword,
      captchaText,
      loading,
      errors
    } = this.state;

    // const { captcha } = this.props;
    console.log("Regiter page state", this.state);

    //console.log(image);
    return (
      <>
        <div className="container">
          <h1 className="d-flex justify-content-center">Реєстрація</h1>

          <form name="form" onSubmit={this.handleSubmit}>
            <TextFieldGroup
              field="lastName"
              label="Прізвище"
              value={lastName}
              error={errors.lastName}
              onChange={this.handleChange}
            />

            <TextFieldGroup
              field="firstName"
              label="Ім'я"
              value={firstName}
              error={errors.firstName}
              onChange={this.handleChange}
            />

            <TextFieldGroup
              field="email"
              label="Електронна пошта"
              value={email}
              error={errors.email}
              onChange={this.handleChange}
            />

            <PhoneFieldGroup
              field="phone"
              label="Телефон"
              value={phone}
              error={errors.phone}
              onChange={this.handleChange}
            />

            <ImageFieldGroupCropper
              getCroppedImage={this.getCroppedImage}
              error={errors.photo}
              photo={photo}
            />

            <TextFieldGroup
              field="password"
              label="Пароль"
              value={password}
              error={errors.password}
              onChange={this.handleChange}
              type="password"
            />

            <TextFieldGroup
              field="confirmPassword"
              label="Підтверження пароля"
              value={confirmPassword}
              error={errors.confirmPassword}
              onChange={this.handleChange}
              type="password"
            />

            <CaptchaFieldGroup
              field="captchaText"
              label="Введіть текст на зображенні"
              value={captchaText}
              error={errors.captchaText}
              onChange={this.handleChange}
            />

            <div className="form-group">
              <button className="btn btn-primary">Зареєструватися</button>
            </div>
          </form>
        </div>
        {loading && <EclipseWidget/>}
      </>
    );
  }
}

export default RegisterPage;
