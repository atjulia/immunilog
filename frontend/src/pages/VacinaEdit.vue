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
					</v-row>
        </template>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
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
			this.model = dependente
		},
		selectDependente (dependente) {
			console.log(dependente)
			console.log(dependente.Id)
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