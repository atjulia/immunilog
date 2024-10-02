import apiClient from '../index';

export const GetVacinaByIdadePessoa = async (pessoaId, param) => {
  try {
    console.log(param)
    const response = await apiClient.get(`/Vacina/GetVacinaByIdadePessoa/${pessoaId}?param=${param}`);
    return response.data;
  } catch (error) {
    console.error('Erro ao buscar vacinas:', error);
    throw error;
  }
};
