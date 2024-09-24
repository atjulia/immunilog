<template>
	<div class="text-center pa-4">
    <v-dialog
      v-model="show"
			persistent
			width="800"
    >
      <v-card
        :title="tipo === 1 ? 'Adicionar Vacina' : 'Carteira de Vacinação'"
      >
			<template v-slot:prepend>
				<PhPlusCircle v-if="tipo === 1" :size="32" />
				<PhCardholder v-else :size="32" />
			</template>
			<v-row class="py-4" v-for="item in dependentes" :key="item.Id">
				<v-col cols="12" class="mx-8">
					<card
							block
              rounded="lg"
              @click="selectDependente(item)"
            >
						<div style="cursor: pointer">
							<v-row>
								<v-col cols="8" class="pa-0">
									{{ item.Nome }}
								</v-col>
								<v-col cols="4" class="pa-0">
									<v-chip color="secondary">
										{{ item.TipoPessoa === 1 ? 'Principal' : 'Dependente' }}
									</v-chip>
								</v-col>
							</v-row>
						</div>
						<span>
						</span>
					</card>
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
			dependentes: [],
			tipo: null,
      credentials: JSON.parse(localStorage.getItem('credentials')),
		}
	},
	methods: {
		openModal (dependentes, tipo) {
			this.show = true
			this.tipo = tipo
			this.dependentes = dependentes
		},
		selectDependente (dependente) {
			if (this.tipo === 1) {
				this.$emit('dependente', dependente)
				this.close()
			} else {
				console.log('carteira')
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