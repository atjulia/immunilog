<template>
	<v-toolbar density="compact" color="background" class="d-flex py-3 px-5 justify-space-between align-center">
		<v-toolbar-title>
			<v-btn color="primary" v-if="$route.path !== '/home'" @click="$router.push('/home')" icon><PhCaretLeft :size="32" /></v-btn>
		</v-toolbar-title>
		
		<v-toolbar-title class="text-center">
			<v-img src="@/assets/logo.svg" @click="$router.push('/home')" style="cursor: pointer;" height="25" />
		</v-toolbar-title>
		
		<v-toolbar-title class="text-right pr-2">
			<span class="text pr-3">
				{{ credentials.nome }}
			</span>
			<v-btn color="primary" @click="logout" icon><PhSignOut :size="32" /></v-btn>
		</v-toolbar-title>
	</v-toolbar>
</template>

<script>
export default {
  data () {
    return {
      credentials: JSON.parse(localStorage.getItem('credentials'))
    }
  },
  methods: {
		logout () {
			localStorage.clear();
			this.$router.push('/login')
			location.reload();
		}
  }
}
</script>

<style scoped>
.text {
  font-weight: 600;
  font-size: 20px;
  color: #384593;
}
</style>