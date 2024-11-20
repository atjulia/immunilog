<template>
  <div>
    <v-row class="px-5 pt-8">
      <v-col cols="6">
        <v-col cols="12">
          <span class="text-title">Minha saúde</span>
        </v-col>
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
        <!-- <v-row>
          <v-col cols="12" class="px-5">
            <span class="text-title2">Programas governamentais para saúde</span>
          </v-col>
          <v-row class="pl-6 pt-6">
            <div class="d-flex align-items-center justify-center card2">
              <v-img src="../assets/link1.svg" />
            </div>
          </v-row>
        </v-row> -->
      </v-col>
      <v-col cols="6">
        <v-col cols="12" class="d-flex justify-space-between">
          <span class="text-title">Gerenciar Pessoas</span>
          <v-btn density="comfortable" color="primary" @click="addDependente">
            Adicionar Dependente
            <v-icon  class="pl-2">
              <PhPlus :size="24" />
            </v-icon>
          </v-btn>
        </v-col>
        <v-col cols="12" class="">
          <v-row v-for="(pessoa, i) in paginatedPessoas" :key="i">
            <v-col cols="12">
              <v-card variant="outlined" color="primary" class="mb-4 pa-5">
                <v-row>
                  <v-col cols="7">
                    <div class="text-start">
                      <strong>{{ pessoa.nome }}</strong>
                    </div>
                    <div class="text-start">
                      <span class="text-body-2">Idade: {{ pessoa.idadeFormatada}}</span><br>
                      <span class="text-body-2">Última imunização: Não informado</span>
                    </div>
                  </v-col>
  
                  <v-col cols="5" class="d-flex justify-end align-items-center">
                    <v-chip color="secondary" class="secondary--text text-caption" variant="outlined">
                      {{ pessoa.tipoPessoa === 1 ? 'Principal' : 'Dependente' }}
                    </v-chip>
                    <v-menu>
                      <template v-slot:activator="{ props }">
                        <v-icon color="primary" v-bind="props" class="pl-2" style="cursor: pointer;">
                          <PhDotsThreeVertical :size="32" />
                        </v-icon>
                      </template>
                      <v-list>
                        <v-list-item
                          v-for="(item, index) in items"
                          :key="index"
                          :value="index"
                        >
                          <v-list-item-title @click="handleDeletePessoa(pessoa)" :disabled="pessoa.tipoPessoa === 1"> 
                            <v-icon color="primary" v-bind="props" class="pr-2" style="cursor: pointer;">
                              <PhTrash :size="32" />
                            </v-icon>
                            {{ item.title }}
                          </v-list-item-title>
                        </v-list-item>
                      </v-list>
                    </v-menu>
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
    <ConfirmacaoDeletePessoa ref="deletePessoa" @refresh="fetchPessoas" />
    <ModalDependenteList ref="dependenteList" @dependente="addVacina"/>
  </div>
</template>


<script>
import { getPessoasByUsuarioId } from '@/api/controllers/pessoa';
import DependenteEdit from './DependenteEdit.vue';
import VacinaEdit from './VacinaEdit.vue';
import ConfirmacaoDeletePessoa from './ConfirmacaoDeletePessoa.vue';
import ModalDependenteList from '../components/ModalDependenteList.vue';

import AddVacina from '../assets/cards/AddVacina.svg';
import CarteiraVacina from '../assets/cards/CarteiraVacina.svg';
import ProgramaImunizacao from '../assets/cards/ProgramaImunizacao.svg';
import GerenciarPerfil from '../assets/cards/GerenciarPerfil.svg';

export default {
  components: { 
    DependenteEdit,
    VacinaEdit,
    ModalDependenteList,
    ConfirmacaoDeletePessoa
  },
  data () {
    return {
      credentials: JSON.parse(localStorage.getItem('credentials')),
      pessoas: [],
      currentPage: 1,
      itemsPerPage: 3,
      items: [
        { title: 'Deletar Pessoa' },
      ]
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
        // { 
        //   id: 3, 
        //   text: 'Gerenciar Meu Perfil', 
        //   action: this.gerenciarPerfil, 
        //   image: GerenciarPerfil 
        // }
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
      window.open('https://www.gov.br/saude/pt-br/acesso-a-informacao/acoes-e-programas/pni', '_blank', 'noopener');
    },
    gerenciarPerfil() {
      console.log('Gerenciando Perfil');
    },
    handleDeletePessoa (pessoa) {
      this.$refs.deletePessoa.openModal(pessoa)
    },
    async fetchPessoas () {
      try {
        const response = await getPessoasByUsuarioId(this.credentials.usuarioId);
        this.pessoas = response;
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
.text-title2 {
  font-weight: 500;
  font-size: 16px;
  color: #384593;
}
.card {
  width: 100px;
  height: 100px;
  border: 2px solid #384593;
  border-radius: 16px;
}
.card2 {
  min-width: 450px;
  height: 165px;
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
