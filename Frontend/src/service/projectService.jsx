  import apiClient from '../api/api';

  export const getProjectById = async (projectId, navigate) => {
    try {
      const api = apiClient(navigate);
      const response = await api.get(`/api/v1/project/${projectId}`);
      return response.data;
    } catch (error) {
      throw error;
    }
  };

  export const updateProject = async (projectId, updatedData, navigate) => {
    try {
      const api = apiClient(navigate);
      const response = await api.put(`/project/${projectId}`, updatedData);
      return response.data;
    } catch (error) {
      throw error;
    }
  };

  export const deleteProject = async (projectId, navigate) => {
    try {
      const api = apiClient(navigate);
      const response = await api.delete(`/project/${projectId}`);
      return response.data;
    } catch (error) {
      throw error;
    }
  };

  export const getProjectsByUserId = async (userId, navigate) => {
    try {
      const api = apiClient(navigate);
      const response = await api.get(`/projects/${userId}`);
      return response.data;
    } catch (error) {
      throw error;
    }
  };

  export const createProject = async (projectData, navigate) => {
    try {
      const api = apiClient(navigate);
      const response = await api.post('/project', projectData);
      return response.data;
    } catch (error) {
      throw error;
    }
  };
