<template>
	<div class="text-center pa-4">
    <v-dialog
      v-model="show"
			persistent
			width="800"
    >
      <v-card title="Adicionar Vacina">
				<v-row class="px-4">
					<v-col cols="6">
						<v-select :items="optionVacinas" label="Vacina" v-model="model.VacinaId" variant="outlined" item-title="text" item-value="value" />
					</v-col>
					<v-col cols="6">
						<v-select
							v-model="model.Reacao"
							:items="reacoes"
							label="Reação"
							chips
							multiple
							variant="outlined" />
					</v-col>
					<v-col cols="12">
						<v-file-input
							label="Anexar Comprovante de Vacina"
							v-model="model.file"
							accept=".pdf,.doc,.docx,.jpg,.png" 
							@change="handleFileChange"
							variant="outlined"
							prepend-icon="mdi-upload"
						/>
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
import { CreateVacinaPessoa } from '@/api/controllers/pessoaVacina';

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
			this.model.DependenteId = dependente.Id
			var response = await GetVacinaByIdadePessoa(dependente.Id);
			this.optionVacinas = response.map(p => {
				return { text: `${p.Nome} - ${p.TipoDose}`, value: p.Id }
			})
			console.log(this.optionVacinas)
			this.show = true
		},
		handleFileChange(event) {
      const selectedFile = event.target.files[0];
      if (selectedFile) {
        this.file = selectedFile;
        this.fileUrl = URL.createObjectURL(selectedFile);
				console.log(this.fileUrl)
      }
    },
		async submit () {
			try {
				const response = await CreateVacinaPessoa(this.model);
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