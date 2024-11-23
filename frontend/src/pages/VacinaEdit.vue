<template>
	<div class="text-center pa-4" v-if="show">
    <v-dialog
      v-model="show"
			persistent
			width="500"
    >
      <v-card title="Adicionar Vacina">
				<v-row class="px-4">
					<v-col cols="12">
						<v-select :items="optionVacinas" label="Vacina" v-model="model.VacinaId" variant="outlined" no-data-text="Nenhuma vacina disponível" item-title="text" item-value="value" />
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
						<input-date
                :label="'Data de Aplicação'"
                v-model="model.dtAplicacao"
								:minDate="pessoa.dtNascimento"
                variant="outlined"
                :rules="[requiredRule]"
              />
					</v-col>
					<v-col cols="12">
						<v-text-field
							label="Fabricante"
							v-model="model.fabricante"
							variant="outlined"
						/>
					</v-col>
					<v-col cols="12">
						<v-text-field
							label="Lote da vacina"
							v-model="model.loteVacina"
							variant="outlined"
						/>
					</v-col>
					<v-col cols="12">
						<v-text-field
							label="Clínica ou UBS que o procedimento foi realizado"
							v-model="model.localAplicacao"
							variant="outlined"
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
import { getVacinas } from '@/api/controllers/vacina';
import { CreateSolicitacaoVacina, GetVacinasByPessoaId } from '@/api/controllers/pessoaVacina';
import { getPessoaById } from '@/api/controllers/pessoa';
import { toast } from "vue3-toastify";
import "vue3-toastify/dist/index.css";

export default {
	data () {
		return {
			show: false,
			model: {},
			tipo: null,
			reacoes: ['Dor ou sensibilidade no local da aplicação', 'Febre', 'Fadiga', 'Dor muscular ou nas articulações', 'Dor de cabeça', 'Calafrios', 'Náusea', 'Inchaço', 'Tontura', 'Outros'],
			optionVacinas: [],
      fileUrl: null,
			pessoa: {},
			dateError: ''
		}
	},
	methods: {
		async openModal (dependente) {
			this.model.PessoaId = dependente.id
			this.pessoa = await getPessoaById(dependente.id);
			this.pessoa.vacinas = await GetVacinasByPessoaId(dependente.id, 'filtroVacina')
			const response = await getVacinas(this.pessoa)

			this.optionVacinas = response.map(p => {
				return { text: `${p.nome} - ${p.tipoDose}`, value: p.id }
			})
			this.show = true
		},
		convertDateTime (data) {
			const [dia, mes, ano] = data.split('/');
			return new Date(ano, mes - 1, dia);
		},
		validateDate() {
      const date = this.model.dtAplicacao;
      if (this.isValidDate(date)) {
        this.dateError = "";
      } else {
        this.dateError = "Data inválida.";
      }
    },
    isValidDate(date) {
      const regex = /^(\d{2})\/(\d{2})\/(\d{4})$/;
      const match = date.match(regex);
      if (!match) return false;

      const day = parseInt(match[1], 10);
      const month = parseInt(match[2], 10) - 1;
      const year = parseInt(match[3], 10);

      if (year < 1910) return false;

      const dateObj = new Date(year, month, day);
      return dateObj.getDate() === day && dateObj.getMonth() === month && dateObj.getFullYear() === year;
    },
		async submit () {
			try {
				const response = await CreateSolicitacaoVacina({...this.model, DtAplicacao: this.convertDateTime(this.model.dtAplicacao), Reacao: JSON.stringify(this.model.Reacao)});
				if (response) {
					toast('Vacina Cadastrada com sucesso!', {
						theme: "colored",
						type: "success",
						dangerouslyHTMLString: true
					})
					this.$emit('refresh')
					this.close()
				}
			} catch (error) {
				console.error('Erro ao criar vacina:', error);
			}
		},
		close () {
			this.show = false
			this.model = {}
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