<template>
  <div class="text-center pa-4" v-if="dialog">
    <v-dialog
      v-model="dialog"
      width="auto"
			persistent
    >
      <v-card
        max-width="430"
        title="Confirmação de exclusão"
      >
			<v-row class="pa-4">
				<span class="mx-4">
					Você realmente deseja deletar o dependente <span class="text-primary">{{ pessoa.nome }}</span>
				</span>
			</v-row>
        <template v-slot:actions>
          <v-btn
            text="Não"
            @click="dialog = false"
          ></v-btn>
          <v-btn
            class="ms-auto"
            text="Sim"
            @click="handleExclusao"
          ></v-btn>
        </template>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import { DeletePessoa } from '@/api/controllers/pessoa';
export default {
	data () {
		return {
			dialog: false,
			pessoa: {}
		}
	},
	methods: {
		openModal (sender) {
			this.pessoa = sender
			this.dialog = true
		},
		async handleExclusao () {
			await DeletePessoa(this.pessoa.id)
			this.dialog = false
			this.$emit('refresh')
		}
	}
}
</script>