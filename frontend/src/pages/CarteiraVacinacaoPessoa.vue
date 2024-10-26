<template>
  <v-container fluid>
    <div v-if="load">
      <v-row class="px-10">
        <v-col cols="12" class="justify-space-between d-flex">
          <span class="text-title">Carteira de Vacinação</span>
          <span class="text-body"><b>{{ model.Nome }}</b> | {{ model.IdadeFormatada }}</span>
        </v-col>
        <v-divider></v-divider>
      </v-row>
      <div class="pt-5" v-if="model.Vacinas.length > 0">
        <v-timeline side="end" align="start">
          <v-timeline-item
            v-for="vacina in model.Vacinas"
            :key="vacina.Id"
            fill-dot
            dot-color="secondary"
            size="small"
          >
          <v-card style="min-width: 80vw;" :value="true">
            <v-card-title class="pt-4">
              <div class="d-flex justify-space-between" style="width: 100%;">
                <span class="text-primary">{{ vacina.Nome }}</span>
                <v-chip variant="outlined" color="secondary">
                  <span class="pr-1">
                    {{ vacina.TipoDose }}
                  </span>
                  <v-icon><PhSyringe :size="32" /></v-icon>
                </v-chip>
              </div>
            </v-card-title>
            <v-card-subtitle>
              {{ vacina.Descricao }}
            </v-card-subtitle>
            <div class="d-flex justify-space-between align-center py-2">
              <v-card-text class="text-primary">
                <p><strong>Idade Recomendada:</strong> {{ formatIdade(vacina.IdadeRecomendada) }}</p>
                <p><strong>Data da aplicação:</strong> {{ vacina.DtUpdate ? formatDate(vacina.DtUpdate) : 'Não Aplicada' }}</p>
              </v-card-text>
            
              <v-btn @click="moreInfo(vacina.Id)" flat class="text-primary mr-4" text>
                <span class="pr-2">Saiba mais</span> 
                <v-icon><PhInfo :size="32" /></v-icon>
              </v-btn>
            </div>
          </v-card>
          </v-timeline-item>
        </v-timeline>
      </div>
      <div v-else>
        <div class="justify-center d-flex pt-5">
          <span class="text-primary">
            Não há registros para serem apresentados
          </span>
        </div>
      </div>
    </div>
  </v-container>
</template>

<script>
import { getPessoaById } from '@/api/controllers/pessoa';
export default {
  data() {
    return {
      model: {},
      load: false,
      items: [
        {
          id: 1,
          color: 'info',
          icon: 'mdi-information',
        },
        {
          id: 2,
          color: 'error',
          icon: 'mdi-alert-circle',
        },
      ],
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
  async beforeCreate() {
    this.model = await getPessoaById(this.$route.query.id);
    this.load = true;
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
