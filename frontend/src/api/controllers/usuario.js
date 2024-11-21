import apiClient from '../index';
import { toast } from "vue3-toastify";
import "vue3-toastify/dist/index.css";

export const CreateUsuario = async (data) => {
  await apiClient.post('/Usuario/CreateUsuario', data).then((response) => {
    console.log(response)
    if (response.data.success) {
      return response.data.data;
    } else {
      toast(response.data.msg, {
        theme: "colored",
        type: "error",
        dangerouslyHTMLString: true
      })
    }
  })
};
