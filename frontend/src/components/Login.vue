<template>
  <v-container class="fill-height">
    <v-responsive
      color="white"
      class="align-center fill-height mx-auto"
      max-width="900"
    >
      <v-card color="#F8F7F5" class="pa-3">
        <v-row>
          <v-col cols="12">
            <v-img src="@/assets/logo.svg" height="25" />
          </v-col>
          <v-col cols="12">
            <v-text-field label="E-mail" prepend-icon="mdi mdi-email-outline" variant="outlined" />
          </v-col>
          <v-col cols="12">
            <v-text-field label="Senha" prepend-icon="mdi mdi-lock-outline" variant="outlined" />
          </v-col>
          <v-col cols="3">
            <v-btn>Entrar</v-btn>
          </v-col>
        </v-row>
      </v-card>
    </v-responsive>
  </v-container>
</template>

<script setup>
  //
</script>
