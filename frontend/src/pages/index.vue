<template>
  <div class="fill-height">
    <Login v-if="!token"/>
    <v-toolbar v-else density="compact" color="#F8F7F5">
      <v-toolbar-title text="immunilog" class="d-flex justify-center"/>
    </v-toolbar>
  </div>
</template>

<script>
export default {
  data () {
    return {
      token: localStorage.getItem('credentials')
    }
  }
}
</script>
