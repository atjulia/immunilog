<template>
  <v-container fluid>
    <div v-if="load">
      <v-row class="px-10">
        <v-col cols="12" class="justify-space-between d-flex">
          <span class="text-title">Carteira de Vacinação</span>
          <span class="text-body"> <b>{{ model.Nome }}</b> | {{ model.IdadeFormatada }}</span>
          <!-- <v-alert type="info" border="left">
            Para ficar em dia com seu calendário vacinal, recomendamos as seguintes vacinas:
            <ul>
              <li v-for="vacina in model.Vacinas" :key="vacina.Id">
                {{ vacina.Nome }} ({{ vacina.TipoDose }})
              </li>
            </ul>
          </v-alert> -->
        </v-col>
				<v-divider></v-divider>
      </v-row>


      <v-timeline align="start" side="end" color="blue" class="custom-timeline">
        <v-timeline-item
          v-for="vacina in model.Vacinas"
          :key="vacina.Id"
          color="blue"
          small
        >
          <template opposite>
            <v-avatar size="40" color="blue darken-1">
              <v-icon color="white">mdi-syringe</v-icon>
            </v-avatar>
          </template>

          <v-card>
            <v-card-title>{{ vacina.Nome }}</v-card-title>
            <v-card-subtitle>{{ vacina.TipoDose }}</v-card-subtitle>
            <v-card-text>
              <p>{{ vacina.Descricao }}</p>
              <p><strong>Idade Recomendada:</strong> {{ formatIdade(vacina.IdadeRecomendada) }}</p>
              <p><strong>Data da aplicação:</strong> {{ vacina.DtUpdate ? formatDate(vacina.DtUpdate) : 'Não Aplicada' }}</p>
            </v-card-text>
            <v-card-actions>
              <v-btn @click="moreInfo(vacina.Id)" text>Saiba mais</v-btn>
            </v-card-actions>
          </v-card>
        </v-timeline-item>
      </v-timeline>
    </div>
    <div v-else>
      <v-row justify="center">
        <v-col cols="12" md="4">
          <v-skeleton-loader type="card" />
        </v-col>
      </v-row>
    </div>
  </v-container>
</template>

<script>
import { getPessoaById } from '@/api/controllers/pessoa';
export default {
  data() {
    return {
      model: {},
      load: false
    };
  },
  methods: {
    formatIdade(idadeRecomendada) {
      return idadeRecomendada > 1 ? idadeRecomendada : (idadeRecomendada.toString().split(".")[1] === '1' ? idadeRecomendada.toString().split(".")[1] + ' mês': + idadeRecomendada.toString().split(".")[1] + ' meses');
    },
    formatDate(date) {
      return new Date(date).toLocaleDateString('pt-BR');
    },
    moreInfo(vacinaId) {
      console.log('More info for vaccine ID:', vacinaId);
    }
  },
  async created() {
    this.model = await getPessoaById(this.$route.query.id);
    this.load = true;
    console.log(this.model);
  }
}
</script>

<style scoped>
.text-title {
  color: #384593;
  font-size: 28px;
  font-style: normal;
  font-weight: 600;
  line-height: 32px;
}

.text-body {
  color: #384593;
  font-size: 18px;
  font-style: normal;
  font-weight: 400;
  line-height: 22px;
}
</style>
