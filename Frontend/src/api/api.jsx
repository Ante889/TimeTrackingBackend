import axios from 'axios';
import { BACKEND_URL } from '../Constants';
import { notification } from 'antd';

const apiClient = (navigate) => {
  const instance = axios.create({
    baseURL: BACKEND_URL,
    headers: {
      'Content-Type': 'application/json',
    },
  });

  instance.interceptors.request.use(
    (config) => {
      const token = localStorage.getItem('token');
      if (token) {
        config.headers['Authorization'] = `Bearer ${token}`;
      }
      return config;
    },
    (error) => Promise.reject(error)
  );

  instance.interceptors.response.use(
    (response) => response,
    (error) => {
      if (error.response && error.response.status === 401) {
        notification.error({
          message: 'Unauthorized',
          description: 'Please login again',
        });
        localStorage.removeItem('token');
        navigate('/login');
      }
      return Promise.reject(error);
    }
  );

  return instance;
};

export default apiClient;
