import apiClient from '../api/api';

export const loginUser = async (email, password, navigate) => {
  try {
    const api = apiClient(navigate);
    const response = await api.post('/login', {
      email,
      password,
    });
    
    if (response.data.token) {
      localStorage.setItem('token', response.data.token);
      localStorage.setItem('userId', response.data.id);
      localStorage.setItem('userFullName', response.data.firstName + ' ' + response.data.lastName);
      navigate('/dashboard');
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
      localStorage.setItem('token', response.data.token);
      navigate('/dashboard');
    }
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const logoutUser = (navigate) => {
   localStorage.removeItem('token');
   localStorage.removeItem('userFullName');
   localStorage.removeItem('userId');
   navigate('/login');
};