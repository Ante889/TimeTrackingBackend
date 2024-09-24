import apiClient from '../api/api';

export const getPhaseById = async (phaseId, navigate) => {
  try {
    const api = apiClient(navigate);
    const response = await api.get(`/phase/${phaseId}`);
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const updatePhase = async (phaseId, updatedData, navigate) => {
  try {
    const api = apiClient(navigate);
    const response = await api.put(`/phase/${phaseId}`, updatedData);
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const deletePhase = async (phaseId, navigate) => {
  try {
    const api = apiClient(navigate);
    const response = await api.delete(`/phase/${phaseId}`);
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const getPhasesByProjectId = async (projectId, navigate) => {
  try {
    const api = apiClient(navigate);
    const response = await api.get(`/phases/${projectId}`);
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const createPhase = async (phaseData, navigate) => {
  try {
    const api = apiClient(navigate);
    const response = await api.post('/phase', phaseData);
    return response.data;
  } catch (error) {
    throw error;
  }
};
