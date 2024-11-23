<template>
  <div>
    <v-row class="px-5 pt-8">
      <v-col cols="12">
        <v-row>
          <v-col cols="12">
            <span class="text-title">Minha saúde</span>
          </v-col>
        </v-row>
        <v-row>
          <v-col
            v-for="card in cards"
            :key="card.id"
            cols="12"
            md="4"
            class="d-flex"
          >
            <v-card
              class="pa-6 hover"
              elevation="2"
              rounded="lg"
              @click="card.action"
              style="cursor: pointer"
              color="background"
              width="100%"
            >
              <v-row class="align-center mb-4">
                <v-avatar
                  size="48"
                >
                  <v-img :src="card.image" />
                </v-avatar>
                <span class="pl-3 text-title2 font-weight-medium">{{ card.text }}</span>
              </v-row>
              <p class="text-body-2 mt-3 mb-5">{{ card.description }}</p>
              <span
                text
                class="text-primary text-capitalize"
              >
                Ver mais
                <v-icon size="18" class="ml-1">mdi-arrow-right</v-icon>
              </span>
            </v-card>
          </v-col>
        </v-row>
      </v-col>
      <v-col cols="12" class="d-flex justify-space-between">
        <span class="text-title">Gerenciar Pessoas</span>
        <v-btn density="comfortable" color="primary" @click="addDependente">
          Adicionar Dependente
          <v-icon  class="pl-2">
            <PhPlusCircle :size="32" />
          </v-icon>
        </v-btn>
      </v-col>
      <v-col cols="12">
        <v-row>
          <v-col cols="12" v-for="(pessoa, i) in paginatedPessoas" :key="i">
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
                        :disabled="pessoa.tipoPessoa === 1"
                      >
                        <v-list-item-title @click="handleDeletePessoa(pessoa)"> 
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
          description: 'Registre suas novas imunizações no sistema. Garanta a organização e o acompanhamento eficaz do calendário vacinal.',
          action: this.choseDependenteVacina,
          avatarColor: 'primary-darken-1',
          image: AddVacina
        },
        { 
          id: 1, 
          text: 'Carteira de Vacinação', 
          description: 'Acesse o histórico completo de vacinas registradas, incluindo datas de aplicação, doses e vacinas pendentes.',
          action: this.verCarteira,
          avatarColor: 'primary-darken-1',
          image: CarteiraVacina 
        },
        { 
          id: 2, 
          text: 'Programa de Imunização', 
          description: 'Consulte o calendário completo de vacinação. Tenha acesso a informações sobre campanhas, grupos prioritários e vacinas disponíveis.', 
          action: this.verProgramaImunizacao,
          avatarColor: 'primary-darken-1', 
          image: ProgramaImunizacao 
        },
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
  font-weight: 600;
  color: #333; 
  font-size: 16px;
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
  transition: box-shadow 0.3s ease, transform 0.3s ease;
  border: 1px solid #e5e5e5;
}
.hover:hover {
  box-shadow: 0 10px 15px rgba(0, 0, 0, 0.1);
  transform: translateY(-2px);
}

.v-card p {
  color: #777;
  line-height: 1.5;
}

.v-avatar {
  background-color: #f5f5f5;
  display: flex;
  align-items: center;
  justify-content: center;
}

.v-avatar img {
  object-fit: cover;
}
</style>
