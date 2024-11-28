<template>
  <div class="text-center pa-4" v-if="show">
    <v-dialog
      v-model="show"
      width="450"
			persistent
    >
      <v-card
        prepend-icon="mdi mdi-family-tree"
        title="Adicionar Dependente"
      >
				<v-form ref="form" v-model="formValid" @submit.prevent="submit">
					<v-row class="px-4">
						<v-col cols="12">
							<v-text-field label="CPF" v-model="model.Cpf" variant="outlined" v-mask="'###.###.###-##'" data-cy="CPF" :rules="[requiredRule, cpfRule]"/>
						</v-col>
						<v-col cols="12">
							<v-text-field label="Nome" v-model="model.Nome" variant="outlined" data-cy="Nome" :rules="[requiredRule]" />
						</v-col>
						<v-col cols="12">
								<input-date
									:label="'Data de Nascimento'"
									v-model="model.dtNascimento"
									data-cy="dtNascimento"
									variant="outlined"
									:rules="[requiredRule]"
								/>
						</v-col>
						<v-col cols="12" class="pa-0">
							<v-radio-group @change="model.IdadeLog = null" inline label="Deseja registrar todas suas vacinas?" v-model="model.handleIdadeLog" :rules="[requiredRule]" hide-details>
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
					</v-row>
          <v-card-actions class="justify-space-between pt-4">
						<v-row class="pa-2">
							<v-btn
								class="ml-2"
								text="Fechar"
								@click="show = false"
							></v-btn>
							<v-btn
								class="ms-auto"
								text="Confirmar"
              	color="primary"
								type="submit"
								:disabled="!disabledButton"
							></v-btn>
						</v-row>
					</v-card-actions>
				</v-form>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import { CreatePessoa } from '@/api/controllers/pessoa';
import { toast } from "vue3-toastify";
import "vue3-toastify/dist/index.css";

export default {
	data () {
		return {
			show: false,
			model: {},
			formValid: false,
      credentials: JSON.parse(localStorage.getItem('credentials')),
			handleIdadeLog: null
		}
	},
  computed: {
		disabledButton () {
			return !!(this.formValid && this.model.dtNascimento?.length > 0)
		},
    requiredRule() {
      return v => !!v || 'Este campo é obrigatório';
    },
    cpfRule() {
      return v => {
        if (v.length !== 14) return 'O CPF não está no formato correto';
        return true;
      };
    },
    currentAge() {
      if (!this.model.DtNascimento) {
        return 0;
      } else {
        const birthDate = new Date(this.model.DtNascimento);
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
		openModal () {
			this.model = {}
			this.show = true
		},
    idadeLogValidation(v) {
      if (this.model.IdadeLog) {
        const idadeLog = parseInt(v, 10);
        if (isNaN(idadeLog)) return 'A idade deve ser um número';
        if (idadeLog > this.currentAge) return `A idade não pode ser maior que ${this.currentAge}`;
        return true;
      }
    },
		async submit () {
			try {
        if (this.$refs.form.validate()) {
					const dto = {
						Nome: this.model.Nome,
						Cpf: this.model.Cpf,
						DtNascimento: this.model.dtNascimento,
						TipoPessoa: 2,
						UsuarioId: this.credentials.usuarioId
					}
					const response = await CreatePessoa(dto);
					if (response) {
						toast('Dependente cadastrado com sucesso!', {
							theme: "colored",
							type: "success",
							dangerouslyHTMLString: true
						})
						this.$emit('refresh')
						this.close()
					}
				}	
			} catch (error) {
				console.error('Erro ao cadastrar pessoa:', error);
			}
		},
		convertDateTime (data) {
			const [dia, mes, ano] = data.split('/');
			return new Date(ano, mes - 1, dia);
		},
		close () {
			this.model = {}
			this.show = false
		}
	}
}
</script>