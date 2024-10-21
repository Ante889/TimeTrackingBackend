import apiClient from '../api/api';
import Cookies from 'universal-cookie';
import { removeCookieToken, setCookieToken } from './cookieService';

export const loginUser = async (email, password, navigate) => {
  try {
    const api = apiClient(navigate);
    const response = await api.post('/login', {
      email,
      password,
    });
    
    if (response.data.token) {
      setCookieToken( response.data.token);
      localStorage.setItem('userId', response.data.id);
      localStorage.setItem('userFullName', response.data.firstName + ' ' + response.data.lastName);
      navigate('/base-image');
    }
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const registerUser = async (email, password, confirmPassword, firstName, lastName, navigate) => {
  try {
    const api = apiClient(navigate);
    const response = await api.post('/registration', {
      email,
      password,
      confirmPassword,
      firstName,
      lastName
    });
    
    if (response.data.token) {
      await loginUser(email, password, navigate);
    }
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const logoutUser = (navigate) => {
   removeCookieToken();
   localStorage.removeItem('userFullName');
   localStorage.removeItem('userId');
   navigate('/login');
};