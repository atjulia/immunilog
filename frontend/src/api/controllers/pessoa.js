import apiClient from '../index';
import { toast } from "vue3-toastify";
import "vue3-toastify/dist/index.css";

export const getPessoasByUsuarioId = async (usuarioId) => {
  try {
    const response = await apiClient.get(`/Pessoa/GetPessoasByUsuarioId/${usuarioId}`);
    return response.data;
  } catch (error) {
    console.error('Erro ao buscar pessoas:', error);
    throw error;
  }
};
export const getPessoaById = async (pessoaId, param) => {
  try {
    const response = await apiClient.get(`/Pessoa/getPessoaById/${pessoaId}`);
    return response.data;
  } catch (error) {
    console.error('Erro ao buscar pessoas:', error);
    throw error;
  }
};

export const CreatePessoa = async (data) => {
  try {
    const response = await apiClient.post('/Pessoa/CreatePessoa', data);
    return response.data;
  } catch (error) {
    toast('Já existe uma pessoa cadastrada para o CPF informado', {
      theme: "colored",
      type: "error",
      dangerouslyHTMLString: true
    })
  }
};

export const DeletePessoa = async (pessoaId) => {
  try {
    const response = await apiClient.delete(`/Pessoa/DeletePessoa/${pessoaId}`);
    return response.data;
  } catch (error) {
    console.error('Erro ao deletar pessoa:', error);
    throw error;
  }
};