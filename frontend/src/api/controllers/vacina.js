import { supabase } from '../supabaseClient.js';

export const getVacinas = async (req, res) => {
  const today = new Date();
  const birthDate = new Date(req.dtNascimento);
  let ageInYears = today.getFullYear() - birthDate.getFullYear();

  if (
    today.getMonth() < birthDate.getMonth() ||
    (today.getMonth() === birthDate.getMonth() && today.getDate() < birthDate.getDate())
  ) {
    ageInYears -= 1;
  }

  const ageInDecimal = ageInYears + (today.getMonth() - birthDate.getMonth()) / 12;
  const idadeLog = req.idadeLog;

  let query = supabase
    .from('vacinas')
    .select('*')
    .gte('idadeRecomendada', idadeLog || 0)
    .lte('idadeRecomendada', ageInDecimal);

  const { data, error } = await query;

  if (error) {
    return res.status(400).json({ error: error.message });
  }

  let vacinasRegistradas = req.vacinas.map(vacina => vacina.id);

  let vacinas = data.sort((a, b) => a.idadeRecomendada - b.idadeRecomendada)
  if (vacinasRegistradas.length > 0) {
    return vacinas.filter(vacina => !vacinasRegistradas.includes(vacina.id));
  } else {
    return vacinas;
  }
};


