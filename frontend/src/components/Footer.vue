<template>
    <v-footer height="auto" color="background" fixed padless>
      <v-row class="fill-height">
        <v-col class="d-flex align-center" cols="6">
          <span class="footer-text">© {{ currentYear }} - Immunilog</span>
        </v-col>
        <v-col class="d-flex justify-end align-center" cols="6">
          <span class="footer-text">Developed by Júlia Karolina Atanazio</span>
        </v-col>
      </v-row>
    </v-footer>
  </template>
  
  <script>
  export default {
    data() {
      return {
        currentYear: new Date().getFullYear(),
      };
    },
  };
  </script>
  
  <style scoped>
  .footer-text {
    color: #979797; /* Cor do texto */
    font-size: 12px; /* Tamanho da fonte */
    font-weight: 600; /* Semibold */
  }
  </style>
  