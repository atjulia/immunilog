<template>
  <v-app>
    <v-main v-if="credentials" class="app-main">
      <v-app-bar app flat v-if="credentials">
        <Toolbar />
      </v-app-bar>
      <div class="content">
        <router-view class="px-8" />
      </div>
      <v-footer app color="background">
        <Footer />
      </v-footer>
    </v-main>
    <div v-else>
      <v-app-bar flat>
        <v-toolbar density="compact" color="background" class="d-flex py-3 px-5 justify-space-between align-center">
          <v-toolbar-title class="text-center">
            <v-img src="@/assets/logo.svg" height="25" />
          </v-toolbar-title>
        </v-toolbar>
      </v-app-bar>
      <Login />
      <v-footer app color="background">
        <Footer />
      </v-footer>
    </div>
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
  },
  mounted () {
    if (this.credentials) {
      this.$router.push('/home')
    }
  }
};
</script>

<style scoped>
.app-main {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
}
.content {
  flex: 1;
}
</style>
