import axios from 'axios';

export default class CategoryService {
    static getAll() {
        return axios.get('/api/categories/search');
    }
    static getSideMenu() {
        return axios.get('/api/categories/sidemenu');
    }
}