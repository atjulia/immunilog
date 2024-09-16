import axios from 'axios';

const API_BASE_URL = 'https://localhost:7015/api';

export const getPessoasByUsuarioId = async (usuarioId) => {
  const response = await axios.get(`${API_BASE_URL}/Pessoa/GetPessoasByUsuarioId/${usuarioId}`);
  return response.data;
};