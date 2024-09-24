import apiClient from '../api/api';

export const getCategoryById = async (categoryId, navigate) => {
  try {
    const api = apiClient(navigate);
    const response = await api.get(`/category/${categoryId}`);
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const updateCategory = async (categoryId, updatedData, navigate) => {
  try {
    const api = apiClient(navigate);
    const response = await api.put(`/category/${categoryId}`, updatedData);
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const deleteCategory = async (categoryId, navigate) => {
  try {
    const api = apiClient(navigate);
    const response = await api.delete(`/category/${categoryId}`);
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const getCategoriesByPhaseId = async (phaseId, navigate) => {
  try {
    const api = apiClient(navigate);
    const response = await api.get(`/categories/${phaseId}`);
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const createCategory = async (categoryData, navigate) => {
  try {
    const api = apiClient(navigate);
    const response = await api.post('/category', categoryData);
    return response.data;
  } catch (error) {
    throw error;
  }
};
