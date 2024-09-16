<template>
  <div>
    <v-row class="px-5 pt-8">
      <v-col cols="8">
        <v-row>
          <v-col cols="12">
            <span class="text-title">Minha saúde</span>
          </v-col>
        </v-row>
        <v-row>
          <v-col
            v-for="card in cards"
            :key="card.id"
            cols="1"
            class="d-flex flex-column align-items-center pr-4"
            style="min-width: 125px;"
          >
            <div class="d-flex align-items-center justify-center flex-grow-1">
              <card class="card d-block">
                {{ card.id }}
              </card>
            </div>
            <span class="text-card pt-3">{{ card.text }}</span>
          </v-col>
        </v-row>
      </v-col>
      <v-col cols="4">
        <v-col cols="12" class="px-5">
          <span class="text-title">Links Úteis</span>
        </v-col>
        <v-row>
          <v-col
            cols="1"
            class="d-flex flex-column align-items-center pr-4"
            style="min-width: 125px;"
          >
            <div class="d-flex align-items-center justify-center flex-grow-1">
              <card class="card d-block">
                link
              </card>
            </div>
          </v-col>
        </v-row>
      </v-col>
    </v-row>
    <v-row class="px-5 pt-8">
      <v-col cols="12" class="px-5">
        <span class="text-title">Gerenciar Dependentes</span>
      </v-col>
      <v-col cols="12">
        <v-table
          style="background-color: #F8F7F5;"
          density="compact"
          fixed-header
        >
          <thead>
            <tr>
              <th class="text-left">
                Nome
              </th>
              <th class="text-left">
                Data Nascimento
              </th>
              <th class="text-left">
                Tipo Usuario
              </th>
            </tr>
          </thead>
          <tbody>
            <tr
              v-for="item in pessoas"
              :key="item.Id"
            >
              <td>{{ item.Nome }}</td>
              <td>{{ item.IdadeFormatada }}</td>
              <td>{{ item.TipoPessoa === 1 ? 'Principal' : 'Dependente' }}</td>
            </tr>
          </tbody>
        </v-table>
      </v-col>
    </v-row>
  </div>
</template>


<script>
import { getPessoasByUsuarioId } from '@/api/controllers/pessoa';

export default {
  data () {
    return {
      credentials: JSON.parse(localStorage.getItem('credentials')),
      pessoas: []
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
    },
  },
  async created () {
    await this.fetchPessoas();
  },
  methods: {
    async fetchPessoas () {
      try {
        const response = await getPessoasByUsuarioId(this.credentials.UsuarioId);
        this.pessoas = response;
        console.log(response);
      } catch (error) {
        console.error('Erro ao buscar pessoas:', error);
      }
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
