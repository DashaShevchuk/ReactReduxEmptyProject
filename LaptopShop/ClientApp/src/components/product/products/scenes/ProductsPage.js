import React, { Component } from 'react';
import ProductItem from './ProductItem'

import EclipseWidget from '../../../common/eclipse';



class ProductsPage extends Component {

  state = {
    list: this.props.list,
    loading: this.props.loading,
    errors: {}
  };

  componentDidMount() {
    const page = parseInt(this.props.match.params.page, 10) || 1;
    this.props.getProducts({ page: 1 });
  }

  UNSAFE_componentWillReceiveProps = (nextProps) => {
    console.log('Change props ', nextProps);
    this.setState({
      loading: nextProps.loading,
      errors: nextProps.errors,
      list: nextProps.list
    });
  }


  render() {
    const {
      list,
      loading,
      errors
    } = this.state;
    console.log('----count----', list.length);
    

    const products = list.map((prod) => 
      <ProductItem prod={prod} key={prod.id}/>
    );
    return (
      <>
        <h1>Hello products</h1>
        <div class="row">
          {products}
        </div>



        {loading && <EclipseWidget />}
      </>
    );
  }
}




export default ProductsPage;