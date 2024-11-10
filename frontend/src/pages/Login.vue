<template>
  <v-app id="inspire">
    <v-main>
      <v-container class="fill-height pa-0"
             fluid
             :style="{ backgroundImage: 'url(../assets/background-login.svg)', backgroundPosition: 'center center', backgroundSize: 'cover', backgroundRepeat: 'no-repeat' }">
      <v-col offset-sm="1" sm="10" class="pt-0">
        <v-row class="flex-row-reverse">
          <v-col sm="6" md="6" lg="5" xl="4" class="fill-height pt-0" align="center" >
            <v-row align="center" class="fill-height">
              <v-col>
                <span class="text-primary title">Entre na sua conta</span>
                <div class="py-4">
                  <v-col class="pb-5">
                    <v-text-field label="E-mail" v-model="model.email" prepend-icon="mdi mdi-email-outline" variant="outlined" />
                  </v-col>
                  <v-col class="pb-1 pt-2">
                    <v-text-field label="Senha" v-model="model.senha" type="password" prepend-icon="mdi mdi-lock-outline" variant="outlined" />
                  </v-col>
                  <v-row class="flex-row-reverse pt-6 ma-3 mb-5">
                    <v-btn @click="login"
                            class="float-left"
                            block
                            color="primary">
                      Entrar
                    </v-btn>
                  </v-row>
                  <v-divider class="py-2 mx-5" />
                  <span>
                    Não tem uma conta ainda? 
                    <span class="text-secondary" style="font-weight: bold; cursor: pointer;" @click="cadastrar">Cadastre-se</span>
                  </span>
                </div>
              </v-col>
            </v-row>
          </v-col>
          <v-col>
            <v-row>
              <span class="text-primary title">Controle de vacinação</span>
            </v-row>
            <v-row class="pt-3">
              <span class="text-primary body d-block">Mais praticidade e conforto para seus atendimentos <br/> Obtenha um histórico da vacinação seu e dos seus filhos</span>
            </v-row>
          </v-col>
        </v-row>
      </v-col>
      </v-container>
      <ModalCadastro ref="cadastro" @login="login"/>
    </v-main>
  </v-app>
</template>

<script>
import { jwtDecode } from 'jwt-decode';
import ModalCadastro from './ModalCadastro.vue';
import { authUsuario } from '@/api/controllers/auth';

export default {
  components: {
    ModalCadastro
  },
  data () {
    return {
      model: {},
      logoImage: '@/assets/logo.svg',
    }
  },
  methods: {
    async login () {
      try {
        const response = await authUsuario({ Email: this.model.email, Senha: this.model.senha })
        if (response.Token) {
          const decodedToken = jwtDecode(response.Token);
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
    },
    cadastrar () {
      this.$refs.cadastro.openModal()
    }
  }
}
</script>

<style scoped>
.title {
  font-weight: 800;
  line-height: auto;
  font-size: 36px;
}
.body {
  font-size: 20px;
  font-weight: normal;
}
</style>