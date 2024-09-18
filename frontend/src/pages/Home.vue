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
            <v-btn
              height="100"
              min-width="100"
              flat
              rounded="lg"
              color="background"
              @click="card.action"
            >
              <div class="d-flex align-items-center justify-center flex-grow-1">
                <card class="card d-block">
                  <div class="d-flex justify-center">
                    <v-img :src="card.image" :width="42" />
                  </div>
                </card>
              </div>
            </v-btn>
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
      <v-col cols="6" class="mx-5">
        <v-row class="pb-3">
          <v-btn density="comfortable" icon color="primary" variant="outlined" @click="addDependente">
            <v-tooltip
              activator="parent"
              location="top"
            >Adicionar Dependente</v-tooltip>
            <v-icon color="primary">
              <PhPlus :size="24" />
            </v-icon>
          </v-btn>
        </v-row>
        <v-row>
          <v-col cols="12" class="pa-0">
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
                    Idade
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
      </v-col>
    </v-row>
    <dependenteEdit ref="dependente" @refresh="fetchPessoas" />
    <ModalDependenteList ref="dependenteList" />
  </div>
</template>


<script>
import { getPessoasByUsuarioId } from '@/api/controllers/pessoa';
import DependenteEdit from './DependenteEdit.vue';
import ModalDependenteList from '../components/ModalDependenteList.vue';

import AddVacina from '../assets/cards/AddVacina.svg';
import CarteiraVacina from '../assets/cards/CarteiraVacina.svg';
import ProgramaImunizacao from '../assets/cards/ProgramaImunizacao.svg';
import GerenciarPerfil from '../assets/cards/GerenciarPerfil.svg';

export default {
  components: { 
    DependenteEdit 
  },
  data () {
    return {
      credentials: JSON.parse(localStorage.getItem('credentials')),
      pessoas: []
    }
  },
  computed: {
    cards () {
      return [
        { 
          id: 0, 
          text: 'Adicionar Vacina', 
          action: this.addDependente, 
          image: AddVacina // caminho da imagem importado
        },
        { 
          id: 1, 
          text: 'Carteira de Vacinação', 
          action: this.verCarteira, 
          image: CarteiraVacina 
        },
        { 
          id: 2, 
          text: 'Programa de Imunização', 
          action: this.verProgramaImunizacao, 
          image: ProgramaImunizacao 
        },
        { 
          id: 3, 
          text: 'Gerenciar Meu Perfil', 
          action: this.gerenciarPerfil, 
          image: GerenciarPerfil 
        }
      ];
    }
  },
  async created () {
    await this.fetchPessoas();
  },
  methods: {
    getImageUrl(imagePath) {
      return new URL(imagePath, import.meta.url).href;
    },
    addDependente () {
      this.$refs.dependenteList.openModal(this.pessoas, 1)
    },
    verCarteira() {
      this.$refs.dependenteList.openModal(this.pessoas, 2)
    },
    verProgramaImunizacao() {
      console.log('Visualizando Programa de Imunização');
    },
    gerenciarPerfil() {
      console.log('Gerenciando Perfil');
    },
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
