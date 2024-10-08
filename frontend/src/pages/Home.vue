<template>
  <div>
    <v-row class="px-5 pt-8">
      <v-col cols="7">
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
        <v-row>
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
        </v-row>
      </v-col>
      <v-col cols="5">
        <v-col cols="12" class="px-5 d-flex justify-space-between">
          <span class="text-title">Gerenciar Dependentes</span>
          <v-btn density="comfortable" color="primary" variant="outlined" @click="addDependente">
            Adicionar Dependente
            <v-icon color="primary" class="pl-2">
              <PhPlus :size="24" />
            </v-icon>
          </v-btn>
        </v-col>
        <v-col cols="12" class="mx-5">
          <v-row v-for="(pessoa, i) in paginatedPessoas" :key="i">
            <v-col cols="12">
              <v-card variant="outlined" color="primary" class="mb-4 pa-5">
                <v-row>
                  <v-col cols="7">
                    <div class="text-start">
                      <strong>{{ pessoa.Nome }}</strong>
                    </div>
                    <div class="text-start">
                      <span class="text-body-2">Idade: {{ pessoa.IdadeFormatada}}</span><br>
                      <span class="text-body-2">Última imunização: 10/09/2024</span>
                    </div>
                  </v-col>
  
                  <v-col cols="5" class="d-flex justify-end align-items-center">
                    <v-chip color="secondary" class="secondary--text text-caption" variant="outlined">
                      {{ pessoa.TipoPessoa === 1 ? 'Principal' : 'Dependente' }}
                    </v-chip>
                    <v-icon color="primary" class="pl-2" style="cursor: pointer;">
                      <PhDotsThreeVertical :size="32" />
                    </v-icon>
                  </v-col>
                </v-row>
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
       </v-col>
      </v-col>
    </v-row>
    <dependenteEdit ref="dependente" @refresh="fetchPessoas" />
    <VacinaEdit ref="vacinaAdd" />
    <ModalDependenteList ref="dependenteList" @dependente="addVacina"/>
  </div>
</template>


<script>
import { getPessoasByUsuarioId } from '@/api/controllers/pessoa';
import DependenteEdit from './DependenteEdit.vue';
import VacinaEdit from './VacinaEdit.vue';
import ModalDependenteList from '../components/ModalDependenteList.vue';

import AddVacina from '../assets/cards/AddVacina.svg';
import CarteiraVacina from '../assets/cards/CarteiraVacina.svg';
import ProgramaImunizacao from '../assets/cards/ProgramaImunizacao.svg';
import GerenciarPerfil from '../assets/cards/GerenciarPerfil.svg';

export default {
  components: { 
    DependenteEdit,
    VacinaEdit,
    ModalDependenteList
  },
  data () {
    return {
      credentials: JSON.parse(localStorage.getItem('credentials')),
      pessoas: [],
      currentPage: 1,
      itemsPerPage: 3
    }
  },
  computed: {
    paginatedPessoas() {
      const start = (this.currentPage - 1) * this.itemsPerPage;
      const end = start + this.itemsPerPage;
      return this.pessoas.slice(start, end);
    },
    totalPages() {
      return Math.ceil(this.pessoas.length / this.itemsPerPage);
    },
    cards () {
      return [
        { 
          id: 0, 
          text: 'Adicionar Vacina', 
          action: this.choseDependenteVacina, 
          image: AddVacina
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
    updatePage(page) {
      this.currentPage = page;
    },
    getImageUrl(imagePath) {
      return new URL(imagePath, import.meta.url).href;
    },
    addDependente () {
      this.$refs.dependente.openModal()
    },
    choseDependenteVacina () {
      this.$refs.dependenteList.openModal(this.pessoas, 1)
    },
    addVacina (dependente) {
      this.$refs.vacinaAdd.openModal(dependente)
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
  font-weight: bold;
  font-size: 14px;
  color: #384593;
  text-align: center;
}
.text-title {
  font-weight: 500;
  font-size: 24px;
  color: #384593;
}
.card {
  width: 100px;
  height: 100px;
  border: 2px solid #384593;
  border-radius: 16px;
}
.text-body-2 {
  font-size: 15px;
}
strong {
  font-size: 16px;
  color: #2f2f8d;
}
.v-card {
  border-color: #2f2f8d;
  border-radius: 12px;
}
</style>
