<template>
  <v-container class="fill-height">
    <v-responsive
      color="white"
      class="align-center fill-height mx-auto"
      max-width="900"
    >
      <v-card color="#F8F7F5" class="pa-3" flat>
        <v-row>
          <v-col cols="12">
            <v-img src="@/assets/logo.svg" height="25" />
          </v-col>
          <v-col cols="12">
            <v-text-field label="E-mail" v-model="model.email" prepend-icon="mdi mdi-email-outline" variant="outlined" />
          </v-col>
          <v-col cols="12">
            <v-text-field label="Senha" v-model="model.senha" prepend-icon="mdi mdi-lock-outline" variant="outlined" />
          </v-col>
          <v-col cols="3">
            <v-btn @click="login">Entrar</v-btn>
          </v-col>
        </v-row>
      </v-card>
    </v-responsive>
  </v-container>
</template>

<script>
import { jwtDecode } from 'jwt-decode';

export default {
  data () {
    return {
      model: {}
    }
  },
  methods: {
    async login () {
      try {
        const response = await fetch("https://localhost:7015/login", {
          method: "POST",
          headers: {
            "Content-Type": "application/json"
          },
          body: JSON.stringify({ Email: this.model.email, Senha: this.model.senha })
        });

        const data = await response.json();
        if (data.Token) {
          const decodedToken = jwtDecode(data.Token);
          localStorage.setItem("credentials", JSON.stringify({
            ...decodedToken
          }));
          location.reload();
        } else {
          console.log("Falha na autenticação.");
        }
      } catch (error) {
        console.error("Erro:", error);
      }
    }
  }
}
</script>
