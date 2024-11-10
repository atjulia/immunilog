import { supabase } from '../supabaseClient.js';

export const getVacinas = async (req, res) => {
  const today = new Date();
  const birthDate = new Date(req.DtNascimento);
  let ageInYears = today.getFullYear() - birthDate.getFullYear();

  if (
    today.getMonth() < birthDate.getMonth() ||
    (today.getMonth() === birthDate.getMonth() && today.getDate() < birthDate.getDate())
  ) {
    ageInYears -= 1;
  }

  const ageInDecimal = ageInYears + (today.getMonth() - birthDate.getMonth()) / 12;
  const idadeLog = req.IdadeLog;

  let query = supabase
    .from('vacinas')
    .select('*')
    .gte('idadeRecomendada', idadeLog || 0)
    .lte('idadeRecomendada', ageInDecimal);

  if (idadeLog) {
    query = supabase
      .from('vacinas')
      .select('*')
      .gte('idadeRecomendada', idadeLog)
      .lte('idadeRecomendada', ageInDecimal);
  }
  const { data, error } = await query;

  if (error) {
    return res.status(400).json({ error: error.message });
  }

  let vacinasRegistradas = req.Vacinas.map(vacina => vacina.Id);

  let vacinas = data;
  if (vacinasRegistradas.length > 0) {
    vacinas = vacinas.filter(vacina => !vacinasRegistradas.includes(vacina.id));
  }
  return vacinas;
};


