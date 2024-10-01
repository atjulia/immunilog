<template>
	<div class="text-center pa-4">
    <v-dialog
      v-model="show"
			persistent
			width="500"
    >
      <v-card title="Adicionar Vacina">
				<v-row class="px-4">
					<v-col cols="12">
						<v-select :items="optionVacinas" label="Vacina" v-model="model.VacinaId" variant="outlined" item-title="text" item-value="value" />
					</v-col>
					<v-col cols="12">
						<v-select
							v-model="model.Reacao"
							:items="reacoes"
							label="Reação"
							chips
							multiple
							variant="outlined" />
					</v-col>
					<v-col cols="12">
						<v-text-field label="Data de aplicação" v-model="model.DtAplicacao" variant="outlined" v-mask="'##/##/####'"/>
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
import { GetVacinaByIdadePessoa } from '@/api/controllers/vacina';
import { CreateSolicitacaoVacina } from '@/api/controllers/pessoaVacina';

export default {
	data () {
		return {
			show: false,
			model: {},
			tipo: null,
			reacoes: ['Enjoo', 'Dor de Cabeça', 'Coceira', 'Dor no corpo'],
			optionVacinas: [],
      fileUrl: null,
		}
	},
	methods: {
		async openModal (dependente) {
			console.log('Adicionar vacina', dependente)
			this.model.PessoaId = dependente.Id
			var response = await GetVacinaByIdadePessoa(dependente.Id);
			this.optionVacinas = response.map(p => {
				return { text: `${p.Nome} - ${p.TipoDose}`, value: p.Id }
			})
			console.log(this.optionVacinas)
			this.show = true
		},
		convertDateTime (data) {
			const [dia, mes, ano] = data.split('/');
			return new Date(ano, mes - 1, dia);
		},
		async submit () {
			try {
				const response = await CreateSolicitacaoVacina({...this.model, DtAplicacao: this.convertDateTime(this.model.DtAplicacao), Reacao: JSON.stringify(this.model.Reacao)});
				console.log(response);
				this.$emit('refresh')
				this.close()
			} catch (error) {
				console.error('Erro ao criar vacina:', error);
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