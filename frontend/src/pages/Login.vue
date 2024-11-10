<template>
  <v-app id="inspire">
    <v-main>
      <v-container class="fill-height pa-0"
             fluid
             :style="{ backgroundImage: 'url(/assets/background-login.svg)', backgroundPosition: 'center center', backgroundSize: 'cover', backgroundRepeat: 'no-repeat' }">
      <v-col offset-sm="1" sm="10">
        <v-row class="flex-row-reverse">
          <v-col sm="6" md="6" lg="4" xl="3" class="fill-height" align="center" style="background-color: #FFFFFF; border-radius: 12px;">
            <v-row align="center" class="fill-height">
              <v-col>
                <v-avatar size="250" max-height="48px" tile>
                  <v-img src="@/assets/logo.svg" height="10%" width="100%" contain />
                </v-avatar>
                <div>
                  <v-col class="pb-5">
                    <v-text-field label="E-mail" v-model="model.email" prepend-icon="mdi mdi-email-outline" variant="outlined" />
                  </v-col>
                  <v-col class="pb-1 pt-2">
                    <v-text-field label="Senha" v-model="model.senha" type="password" prepend-icon="mdi mdi-lock-outline" variant="outlined" />
                  </v-col>
                  <v-row class="flex-row-reverse pt-6 ma-3">
                    <v-btn @click="login"
                            class="float-left"
                            block
                            color="primary">
                      Entrar
                    </v-btn>
                  </v-row>
                  <v-divider />
                  <span>Não tem uma conta ainda? <span class="secondary--text" style="cursor: pointer" @click="cadastrar">Cadastre-se</span></span>
                </div>
              </v-col>
            </v-row>
          </v-col>
          <v-col>
            <span>Controle de vacinação </span>
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
