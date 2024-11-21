import apiClient from '../index';
import { toast } from "vue3-toastify";
import "vue3-toastify/dist/index.css";

export const authUsuario = async (data) => {
  await apiClient.post('/Auth', data).then((response) => {
    if (response.data.success) {
      return response.data.data;
    } else {
      toast(response.data.msg, {
        theme: "colored",
        type: "error",
        dangerouslyHTMLString: true
      })
    }
  });
};
