import React, { Component } from 'react';
import { Card } from 'primereact/card';
import { Button } from 'primereact/button';

class ProductItem extends Component {
    constructor(props) {
        super(props);
        this.state = {
        }
    }
    render() {

        const header = <img alt="Card" src='https://www.mrfix.ua/media/product/134747326/apple-macbook-air-13-space-gray-mvfj2-2019.webp' />;

        const footer = (
            <span>
                <Button label="Купити" icon="fa fa-shopping-cart" style={{ marginRight: '.25em' }} />
            </span>);
        return (
            <div className="col-12 col-sm-6 col-md-4">
                <Card header={header} footer={footer}>
                    <div className="text-truncate">
                        {this.props.prod.name}
                    </div>
                </Card>
            </div>
        );
    }
}

export default ProductItem;