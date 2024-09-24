<template>
	<div class="text-center pa-4">
    <v-dialog
      v-model="show"
			persistent
			width="800"
    >
      <v-card title="Adicionar Vacina">
				<v-row class="px-4">
					<v-col cols="12">
						<v-text-field label="sdasdasdasd" v-model="model.Cpf" variant="outlined" v-mask="'###.###.###-##'"/>
					</v-col>
					<v-col cols="12">
						<v-text-field label="Nome" v-model="model.Nome" variant="outlined" />
					</v-col>
					<v-col cols="12">
						<v-text-field label="Data de nascimento" v-model="model.DtNascimento" variant="outlined" v-mask="'##/##/####'"/>
					</v-col>
			</v-row>
			<template v-slot:actions>
					<v-row class="pa-2">
						<v-btn
							class="ml-2"
							text="Fechar"
							@click="show = false"
						></v-btn>
						<v-btn
							class="ms-auto"
							text="Confirmar"
							type="submit"
							@click="submit"
						></v-btn>
					</v-row>
        </template>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import { CreateSolicitacaoVacina } from '@/api/controllers/pessoaVacina';

export default {
	data () {
		return {
			show: false,
			model: {},
			tipo: null,
      credentials: JSON.parse(localStorage.getItem('credentials')),
		}
	},
	methods: {
		openModal (dependente) {
			this.show = true
			console.log('Adicionar vacina', dependente)
			this.model.DependenteId = dependente.Id
		},
		async submit () {
			try {
				var dto = {
					...this.model,
					DtNascimento: this.convertDateTime(this.model.DtNascimento),
					TipoPessoa: 2,
					UsuarioId: this.credentials.UsuarioId
				}
				const response = await CreateSolicitacaoVacina(dto);
				console.log(response);
				this.$emit('refresh')
				this.close()
			} catch (error) {
				console.error('Erro ao buscar pessoas:', error);
			}
		},
		close () {
			this.show = false
		}
	}
}
</script>

<style scoped>
.card {
  width: 100px;
  height: 100px;
  border: 2px solid #384593;
  border-radius: 16px;
}
</style>