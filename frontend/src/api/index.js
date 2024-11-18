import axios from 'axios';

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

const apiClient = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

apiClient.interceptors.request.use(
  (config) => {
    document.body.classList.add('loading');
    return config;
  },
  (error) => {
    document.body.classList.remove('loading');
    return Promise.reject(error);
  }
);

apiClient.interceptors.response.use(
  (response) => {
    document.body.classList.remove('loading');
    return response;
  },
  (error) => {
    document.body.classList.remove('loading');
    return Promise.reject(error);
  }
);

export default apiClient;
