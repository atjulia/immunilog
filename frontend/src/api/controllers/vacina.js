import { supabase } from '../supabaseClient.js';

export const getVacinas = async (req, res) => {
  try {
    const { data, error } = await supabase
      .from('vacina')
      .select('*')
    if (error) {
      return res.status(400).json({ error: error.message })
    }

    res.status(200).json(data)
  } catch (err) {
    res.status(500).json({ error: 'Erro ao buscar dados' })
  }
}

import apiClient from '../index';

export const GetVacinaByIdadePessoa = async (pessoaId, param) => {
  try {
    const response = await apiClient.get(`/Vacina/GetVacinaByIdadePessoa/${pessoaId}?param=${param}`);
    return response.data;
  } catch (error) {
    console.error('Erro ao buscar vacinas:', error);
    throw error;
  }
};