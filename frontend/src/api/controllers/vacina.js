import apiClient from '../index';

export const GetVacinaByIdadePessoa = async (pessoaId) => {
  try {
    console.log(pessoaId)
    const response = await apiClient.get(`/Vacina/GetVacinaByIdadePessoa/${pessoaId}`);
    return response.data;
  } catch (error) {
    console.error('Erro ao buscar vacinas:', error);
    throw error;
  }
};
