<template>
  <div class="text-center pa-4" v-if="show">
    <v-dialog
      v-model="show"
      width="400"
			persistent
    >
      <v-card
        prepend-icon="mdi mdi-family-tree"
        title="Adicionar Dependente"
      >
				<v-form ref="form" v-model="formValid" @submit.prevent="submit">
					<v-row class="px-4">
						<v-col cols="12">
							<v-text-field label="CPF" v-model="model.Cpf" variant="outlined" v-mask="'###.###.###-##'" :rules="[requiredRule, cpfRule]"/>
						</v-col>
						<v-col cols="12">
							<v-text-field label="Nome" v-model="model.Nome" variant="outlined" :rules="[requiredRule]" />
						</v-col>
						<v-col cols="12">
								<input-date
									:label="'Data de Nascimento'"
									v-model="model.dtNascimento"
									variant="outlined"
									:rules="[requiredRule]"
								/>
						</v-col>
						<v-row align="center" class="pa-3">
							<v-col cols="11" class="d-flex justify-center">
								<v-text-field
									label="A partir de que idade deseja logar?"
									v-model="model.IdadeLog"
									variant="outlined"
									v-mask="'##'"
									:rules="[idadeLogValidation]"
								/>
							</v-col>
							<v-col cols="1" class="pt-0 d-flex justify-center align-center">
								<v-tooltip location="top">
									<template v-slot:activator="{ props }">
										<v-icon v-bind="props">
											<PhInfo :size="32" />
										</v-icon>
									</template>
									<span>
										Idade 
										Conforme a idade que for informada nesse campo, o Immunilog vai ignorar vacinas anteriores que não foram registradas.<br>
										Caso não seja informado nenhum valor, o sistema irá considerar todas as vacinas desde o nascimento.
									</span>
								</v-tooltip>
							</v-col>
						</v-row>
					</v-row>
          <v-card-actions class="justify-space-between">
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
								:disabled="!formValid"
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

export default {
	data () {
		return {
			show: false,
			model: {},
			formValid: false,
      credentials: JSON.parse(localStorage.getItem('credentials')),
		}
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
    currentAge() {
			console.log(this.model.DtNascimento)
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
						DtNascimento: this.convertDateTime(this.model.dtNascimento),
						TipoPessoa: 2,
						UsuarioId: this.credentials.usuarioId
					}
					const response = await CreatePessoa(dto);
					if (response) {
						this.$emit('refresh')
						this.close()
					}
				}	
			} catch (error) {
				console.error('Erro ao buscar pessoas:', error);
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