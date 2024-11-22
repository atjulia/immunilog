import apiClient from '../index';
import { supabase } from '../supabaseClient.js';

export const CreateSolicitacaoVacina = async (data) => {
  try {
    const response = await apiClient.post('/VacinaPessoa/CreateSolicitacaoVacina', data);
    return response.data;
  } catch (error) {
    console.error('Erro ao criar solicitação:', error);
    throw error;
  }
};

export const GetVacinasByPessoaId = async (data, param) => {
  try {
    const response = await apiClient.post('/VacinaPessoa/GetVacinasByPessoaId', data);
    if (param) {
      const vacinaIds = response.data.map(vacina => vacina.vacinaId);

      if (vacinaIds.length > 0) {
        const { data, error } = await supabase
          .from('vacinas')
          .select('*')
          .in('id', vacinaIds);

          const mergedData = response.data.map(vacina => {
            const supabaseRecord = data.find(supabaseItem => supabaseItem.id === vacina.vacinaId);
            
            return {
              ...vacina,
              ...supabaseRecord
            };
          });
        return mergedData
      } else {
        return []
      }
    } else {
      return response.data;
    }
  } catch (error) {
    console.error('Erro ao criar pessoa:', error);
    throw error;
  }
};