<template>
	<v-toolbar density="compact" color="background" class="d-flex py-3 px-5 justify-space-between align-center">
		<v-toolbar-title>
			<v-icon color="primary"><PhList :size="32" /></v-icon>
			<span class="text pl-3">Menu</span>
		</v-toolbar-title>
		
		<v-toolbar-title class="text-center">
			<v-img src="@/assets/logo.svg" height="25" />
		</v-toolbar-title>
		
		<v-toolbar-title class="text-right pr-2">
			<span class="text pr-3">
				{{ credentials.Nome }}
			</span>
		</v-toolbar-title>
	</v-toolbar>
</template>

<script>
export default {
  data () {
    return {
      credentials: JSON.parse(localStorage.getItem('credentials'))
    }
  }
}
</script>

<style scoped>
.text {
  font-weight: 600; /* Semibold */
  font-size: 20px;  /* Tamanho da fonte */
  color: #384593;   /* Cor especificada */
}
</style>