<template>
  <div class="text-center pa-4" v-if="show">
    <v-dialog
      v-model="show"
      persistent
      width="500"
    >
      <v-card title="Cadastrar usuário">
        <v-form ref="form" v-model="formValid" @submit.prevent="submit">
          <v-row class="px-4">
            <v-col cols="12">
              <v-text-field
                label="Nome"
                v-model="model.nome"
                variant="outlined"
                :rules="[requiredRule]"
              />
            </v-col>
            <v-col cols="12">
              <v-text-field
                label="CPF"
                v-model="model.cpf"
                variant="outlined"
                v-mask="'###.###.###-##'"
                :rules="[requiredRule, cpfRule]"
              />
            </v-col>
            <v-col cols="12">
              <input-date
                :label="'Data de Nascimento'"
                v-model="model.dtNascimento"
                variant="outlined"
                :rules="[requiredRule]"
              />
            </v-col>
						<v-col cols="12" class="pa-0">
							<v-radio-group @change="model.IdadeLog = null" inline label="Deseja registrar todas suas vacinas?" v-model="model.handleIdadeLog" hide-details>
								<v-radio label="Sim" :value="1" />
								<v-radio label="Não" :value="2" />
							</v-radio-group>
						</v-col>
						<v-col cols="12">
							<v-text-field
								label="A partir de que idade deseja registrar?"
								v-model="model.IdadeLog"
								variant="outlined"
								v-mask="'##'"
								:disabled="model.handleIdadeLog !== 2"
								hint="Se uma idade for informada, o Immunilog ignorará vacinas anteriores não registradas. Caso contrário, considerará todas as vacinas desde o nascimento."
								:rules="[idadeLogValidation]"
							/>
						</v-col>
            <v-divider class="mx-2"/>
            <v-col cols="12">
              <v-text-field
                label="E-mail"
                v-model="model.email"
                variant="outlined"
                :rules="[requiredRule]"
              />
            </v-col>
            <v-col cols="12">
              <v-text-field
                label="Senha"
                v-model="model.senha"
                type="password"
                variant="outlined"
                :rules="[requiredRule, passwordRule]"
              />
            </v-col>
            <v-col cols="12">
              <v-text-field
                label="Confirme sua senha"
                v-model="model.senhaConfirmacao"
                type="password"
                variant="outlined"
                :rules="[requiredRule, passwordConfirmRule]"
              />
            </v-col>
          </v-row>
          <v-card-actions class="justify-space-between">
            <v-btn
              text
              @click="close"
            >
              Fechar
            </v-btn>
            <v-btn
              color="primary"
              :disabled="!formValid"
              type="submit"
            >
              Confirmar
            </v-btn>
          </v-card-actions>
        </v-form>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import { CreateUsuario } from '@/api/controllers/usuario';
import { authUsuario } from '@/api/controllers/auth';
import { th } from 'vuetify/locale';

export default {
  data() {
    return {
      show: false,
      model: {
        dtNascimento: ''
      },
      formValid: false
    };
  },
  computed: {
    requiredRule() {
      return v => !!v || 'Este campo é obrigatório';
    },
    cpfRule() {
      return v => {
        if (v.length !== 14) return 'O CPF não está no formato correto';
        return true;
      };
    },
    passwordRule() {
      return v => {
        if (!v) return 'A senha é obrigatória';
        return true;
      };
    },
    passwordConfirmRule() {
      return v => {
        if (!v) return 'A confirmação de senha é obrigatória';
        if (v !== this.model.senha) return 'As senhas não coincidem';
        return true;
      };
    },
    currentAge() {
      if (!this.model.dtNascimento) {
        return 0;
      } else {
        const birthDate = new Date(this.model.dtNascimento);
        const today = new Date();
        const age = today.getFullYear() - birthDate.getFullYear();
        const m = today.getMonth() - birthDate.getMonth();
        if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
          return age - 1;
        }
        return age;
      }
    },
  },
  methods: {
    openModal() {
      this.show = true;
    },
    close() {
      this.show = false;
      this.model = {};
    },
    idadeLogValidation(v) {
      if (this.model.idadeLog) {
        const idadeLog = parseInt(v, 10);
        if (isNaN(idadeLog)) return 'A idade deve ser um número';
        if (idadeLog > this.currentAge) return `A idade não pode ser maior que ${this.currentAge}`;
        return true;
      }
    },
    convertDateTime (data) {
			const [dia, mes, ano] = data.split('/');
			return new Date(ano, mes - 1, dia);
		},
    async submit () {
      if (this.$refs.form.validate()) {
        await CreateUsuario(th.model).then((resp) => {
          if (resp) {
            authUsuario({ Email: this.model.email, Senha: this.model.senha })
          }
        })
      }
    },
  },
};
</script>

<style scoped>
.card {
  width: 100px;
  height: 100px;
  border: 2px solid #384593;
  border-radius: 16px;
}
</style>
