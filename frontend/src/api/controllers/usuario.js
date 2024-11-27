import apiClient from '../index';
import { toast } from "vue3-toastify";
import "vue3-toastify/dist/index.css";
import { authUsuario } from '@/api/controllers/auth';

export const CreateUsuario = async (data, credenciais) => {
  await apiClient.post('/Usuario/CreateUsuario', data).then((response) => {
    if (response.data.success) {
      authUsuario(credenciais)
      return response.data;
    } else {
      toast(response.data.msg, {
        theme: "colored",
        type: "error",
        dangerouslyHTMLString: true
      })
    }
  })
};
