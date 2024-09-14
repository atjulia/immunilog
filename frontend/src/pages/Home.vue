<template>
  <div>
    <v-responsive
      color="white"
      class="align-center fill-height mx-auto"
    >
      <v-row class="px-5 pt-8">
        <v-col cols="8">
          <v-row>
            <v-col cols="12" class="px-5">
              <span class="text-title">Minha saúde</span>
            </v-col>
          </v-row>
          <v-row>
            <v-col
              v-for="card in cards"
              :key="card.id"
              cols="1"
              class="d-flex flex-column align-items-center"
              style="min-width: 125px;"
            >
              <div class="d-flex align-items-center justify-center flex-grow-1">
                <card class="card d-block">
                  {{ card.id }}
                </card>
              </div>
              <span class="text-card">{{ card.text }}</span>
            </v-col>
          </v-row>
        </v-col>
        <v-col cols="4">
          <v-col cols="12" class="px-5">
            <span class="text-title text-subtitle-1">Gerenciar Dependentes</span>
          </v-col>
        </v-col>
      </v-row>
      <v-row class="px-5 pt-8">
        <v-col cols="12" class="px-5">
          <span class="text-title">Links Úteis</span>
        </v-col>
      </v-row>
    </v-responsive>
  </div>
</template>


<script>
export default {
  data () {
    return {
      model: {},
      credentials: JSON.parse(localStorage.getItem('credentials'))
    }
  },
  computed: {
    cards () {
      var list = [
        { id: 0, text: 'Adicionar Vacina', image: '' },
        { id: 1, text: 'Carteira de Vacinação', image: '' },
        { id: 2, text: 'Programa de Imunização', image: '' },
        { id: 3, text: 'Gerenciar Meu Perfil', image: '' },
      ]
      return list
    }
  }
}
</script>

<style scoped>
.text-card {
  font-weight: bold;  /* Texto em negrito */
  font-size: 14px;    /* Tamanho da fonte */
  color: #384593;     /* Cor do texto */
  text-align: center; /* Alinhamento central do texto */
}
.text-title {
  font-weight: 500; /* Semibold */
  font-size: 24px;  /* Tamanho da fonte */
  color: #384593;   /* Cor especificada */
}
.card {
  width: 100px;
  height: 100px;
  border: 2px solid #384593;
  border-radius: 16px;
}
</style>
