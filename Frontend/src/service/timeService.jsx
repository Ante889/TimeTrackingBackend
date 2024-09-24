import apiClient from '../api/api';

export const getTimeById = async (timeId, navigate) => {
  try {
    const api = apiClient(navigate);
    const response = await api.get(`/time/${timeId}`);
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const updateTime = async (timeId, updatedData, navigate) => {
  try {
    const api = apiClient(navigate);
    const response = await api.put(`/time/${timeId}`, updatedData);
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const deleteTime = async (timeId, navigate) => {
  try {
    const api = apiClient(navigate);
    const response = await api.delete(`/time/${timeId}`);
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const getTimesByCategoryId = async (categoryId, navigate) => {
  try {
    const api = apiClient(navigate);
    const response = await api.get(`/times/${categoryId}`);
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const createTime = async (timeData, navigate) => {
  try {
    const api = apiClient(navigate);
    const response = await api.post('/time', timeData);
    return response.data;
  } catch (error) {
    throw error;
  }
};
