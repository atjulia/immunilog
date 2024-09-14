<template>
  <v-app>
    <v-main>
      <Toolbar />
      <router-view />
      <!-- <Footer /> -->
    </v-main>
  </v-app>
</template>

<script>
import Toolbar from '@/components/Toolbar.vue'
import Footer from '@/components/Footer.vue'

export default {
  components: {
    Toolbar,
    Footer
  },
  data () {
    return {
      model: {},
      credentials: JSON.parse(localStorage.getItem('credentials'))
    }
  }
}
</script>
