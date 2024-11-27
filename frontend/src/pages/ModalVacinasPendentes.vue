<template>
	<div class="text-center pa-4" v-if="dialog">
		<v-dialog v-model="dialog" max-width="900px">
      <v-card title="Vacinas pendentes">
        <v-card-text class="py-0">
          <v-divider></v-divider>
          <v-row class="pt-4">
            <v-col cols="12" class="pb-0" v-for="(vacina, i) in paginatedVacinas" :key="i">
              <v-card class="mb-2" variant="outlined" color="primary">
                <v-card-title class="d-flex justify-space-between">
                  {{ vacina.nome }}
                  <v-chip class="ml-2" color="secondary" text>
                    {{ vacina.tipoDose }}
                  </v-chip>
                </v-card-title>
                <v-card-text>
                  <div>
                    <strong>Descrição:</strong> {{ vacina.descricao }}
                  </div>
                  <div>
                    <strong>Idade Recomendada:</strong> {{ vacina.idadeRecomendada }}
                  </div>
                </v-card-text>
              </v-card>
            </v-col>
          </v-row>
					<v-pagination
						v-model="currentPage"
						:length="totalPages"
						:total-visible="5"
						class="mx-auto mt-4"
						@input="updatePage"
					/>
        </v-card-text>
        
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn text color="primary" @click="dialog = false">
            Fechar
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
	</div>
</template>

<script>
export default {
	data () {
		return {
			dialog: false,
			vacinas: [],
      currentPage: 1,
      itemsPerPage: 5,
		}
	},
	computed: {
    paginatedVacinas() {
      const start = (this.currentPage - 1) * this.itemsPerPage;
      const end = start + this.itemsPerPage;
      return this.vacinas.slice(start, end);
    },
    totalPages() {
      return Math.ceil(this.vacinas.length / this.itemsPerPage);
    },
	},
	methods: {
		openModal (sender) {
			this.vacinas = sender
			this.dialog = true
		}
	}
}
</script>
