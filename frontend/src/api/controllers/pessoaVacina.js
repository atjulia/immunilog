import apiClient from '../index';

export const CreateSolicitacaoVacina = async (data) => {
  try {
    const response = await apiClient.post('/PessoaVacina/CreateSolicitacaoVacina', data);
    return response.data;
  } catch (error) {
    console.error('Erro ao criar solicitação:', error);
    throw error;
  }
};
