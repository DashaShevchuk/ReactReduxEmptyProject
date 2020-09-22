import axios from "axios";

export default class RefreshService {
    static RefreshToken = () => {
        const refreshToken = localStorage.getItem('refreshToken');
        const authToken = localStorage.getItem('authToken');
        return axios.post(`/api/account/refreshToken`,{
            token:authToken,
            refreshToken
        });
    }
}