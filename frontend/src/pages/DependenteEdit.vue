<template>
  <div class="text-center pa-4">
    <v-dialog
      v-model="show"
      width="auto"
			persistent
    >
      <v-card
        prepend-icon="mdi mdi-family-tree"
        title="Adicionar Dependente"
      >
			<v-row class="px-4">
				<v-col cols="12">
					<v-text-field label="CPF" v-model="model.Cpf" variant="outlined" />
				</v-col>
				<v-col cols="12">
					<v-text-field label="Nome" v-model="model.Nome" variant="outlined" />
				</v-col>
				<v-col cols="12">
					<v-text-field label="Data de nascimento" v-model="model.DtNascimento" variant="outlined" />
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
import { CreatePessoa } from '@/api/controllers/pessoa';

export default {
	data () {
		return {
			show: false,
			model: {},
      credentials: JSON.parse(localStorage.getItem('credentials')),
		}
	},
	methods: {
		openModal () {
			this.show = true
		},
		async submit () {
			try {
				var dto = {
					...this.model,
					TipoPessoa: 2,
					UsuarioId: this.credentials.UsuarioId
				}
				const response = await CreatePessoa(dto);
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