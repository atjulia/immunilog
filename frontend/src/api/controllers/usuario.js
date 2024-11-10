import apiClient from '../index';

export const CreateUsuario = async (data) => {
  try {
    const response = await apiClient.post('/Usuario/CreateUsuario', data);
    return response.data;
  } catch (error) {
    console.error('Erro ao criar usuario:', error);
    throw error;
  }
};
