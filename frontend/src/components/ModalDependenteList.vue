<template>
  <div v-if="show">
    <v-dialog
      v-model="show"
      persistent
      max-width="500"
    >
      <v-card :title="tipo === 1 ? 'Adicionar Vacina' : 'Carteira de Vacinação'">
				<template v-slot:prepend>
					<PhPlusCircle v-if="tipo === 1" :size="32" />
					<PhCardholder v-else :size="32" />
				</template>

        <v-card-text class="pt-4 pb-0">
          <v-row v-for="item in paginatedPessoas" :key="item.Id" class="align-center mb-3">
            <v-col cols="12" class="d-flex justify-space-between align-center">
              <div class="d-flex align-center">
                <span class="font-weight-medium">{{ item.nome }}</span>
              </div>
							<v-chip
								:color="item.tipoPessoa === 1 ? 'primary' : 'secondary'"
								class="ml-3"
								small
							>
								{{ item.tipoPessoa === 1 ? 'Principal' : 'Dependente' }}
							</v-chip>
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

        <v-card-actions class="justify-end">
          <v-btn
            outlined
            @click="close"
          >
            Fechar
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
export default {
  data() {
    return {
      show: false,
      dependentes: [],
      tipo: null,
			currentPage: 1,
			itemsPerPage: 5
    };
  },
	computed: {
    paginatedPessoas() {
      const start = (this.currentPage - 1) * this.itemsPerPage;
      const end = start + this.itemsPerPage;
      return this.dependentes.slice(start, end);
    },
    totalPages() {
      return Math.ceil(this.dependentes.length / this.itemsPerPage);
    },
	},
  methods: {
    openModal(dependentes, tipo) {
      this.show = true;
      this.tipo = tipo;
      this.dependentes = dependentes;
    },
    selectDependente(dependente) {
      if (this.tipo === 1) {
        this.$emit("dependente", dependente);
        this.close();
      } else {
        this.$router.push({ path: "/carteira", query: { id: dependente.id } });
        this.close();
      }
    },
    close() {
      this.show = false;
    }
  }
};
</script>

<style scoped>
/* Estilização aprimorada para as linhas */
.v-row {
  padding: 8px 16px;
  border: 1px solid #e0e0e0;
  border-radius: 8px;
  transition: background-color 0.2s ease-in-out;
  cursor: pointer;
}

.v-row:hover {
  background-color: #f5f5f5;
}

/* Chip para tipos */
.v-chip {
  font-weight: 500;
}

/* Botão de ação */
.v-btn {
  transition: transform 0.2s ease-in-out;
}

.v-btn:hover {
  transform: scale(1.1);
}
</style>
