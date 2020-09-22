import React, { Component } from 'react';
import TextFieldGroup from "../../../common/TextFieldGroup";
import EclipseWidget from '../../../common/eclipse';



class ProductCreatePage extends Component {

  state = {
    name: "",
    price: "",
    loading: this.props.loading,
    errors: {}
  };

  componentDidMount() {

    //this.props.searchProducts({ page: 1 });
  }

  UNSAFE_componentWillReceiveProps = (nextProps) => {
    console.log('Change props ', nextProps);
    this.setState({
      loading: nextProps.loading,
      errors: nextProps.errors
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

  handleSubmit = e => {
    e.preventDefault();
  }

  render() {

    const {
      name,
      price,
      loading,
      errors
    } = this.state;
    //console.log('----count----', list.length);



    return (
      <>
        <div className="container">
          <h1 className="d-flex justify-content-center">Створити продукт</h1>
          <form name="form" onSubmit={this.handleSubmit}>
            <TextFieldGroup
              field="name"
              label="Назва товару"
              value={name}
              error={errors.name}
              onChange={this.handleChange}
            />
             <TextFieldGroup
              field="price"
              label="Ціна"
              value={price}
              type="number"
              error={errors.price}
              onChange={this.handleChange}
            />
          </form>
        </div>

        {loading && <EclipseWidget />}
      </>
    );
  }
}




export default ProductCreatePage;