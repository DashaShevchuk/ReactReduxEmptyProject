import axios from 'axios';

export default class ProductsService {
    static get(model) {
        return axios.post('/api/products/search', model);
    }

    static create(model) {
        return axios.post('/api/products/create', model);
    }
}