import apiClient from '../index';

export const authUsuario = async (data) => {
  try {
    const response = await apiClient.post('/Auth', data);
    return response.data;
  } catch (error) {
    console.error('Erro ao criar usuario:', error);
    throw error;
  }
};
