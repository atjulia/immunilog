import apiClient from '../index';

export const CreateSolicitacaoVacina = async (data) => {
  try {
    console.log(data);
    const response = await apiClient.post('/VacinaPessoa/CreateSolicitacaoVacina', data);
    return response.data;
  } catch (error) {
    console.error('Erro ao criar solicitação:', error);
    throw error;
  }
};

export const GetVacinasByPessoaId = async (data) => {
  try {
    const response = await apiClient.post('/VacinaPessoa/GetVacinasByPessoaId', data);
    return response.data;
  } catch (error) {
    console.error('Erro ao criar pessoa:', error);
    throw error;
  }
};