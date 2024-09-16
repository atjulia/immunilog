<template>
  <v-app>
    <Login v-if="!credentials"/>
    <v-main v-else>
      <Toolbar />
      <router-view class="px-8"/>
      <!-- <Footer /> -->
    </v-main>
  </v-app>
</template>

<script>
import Toolbar from '@/components/Toolbar.vue'
import Footer from '@/components/Footer.vue'
import Login from '@/pages/Login.vue'

export default {
  components: {
    Toolbar,
    Footer,
    Login
  },
  data () {
    return {
      model: {},
      credentials: JSON.parse(localStorage.getItem('credentials'))
    }
  }
}
</script>
