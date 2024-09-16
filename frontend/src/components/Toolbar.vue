<template>
	<v-toolbar density="compact" color="background" class="d-flex py-3 px-5 justify-space-between align-center">
		<!-- Item da esquerda -->
		<v-toolbar-title>
			<v-icon color="primary" size="small" icon="mdi mdi-menu"></v-icon>
			<span class="text pl-3">Menu</span>
		</v-toolbar-title>
		
		<!-- Item central -->
		<v-toolbar-title class="text-center">
			<v-img src="@/assets/logo.svg" height="25" />
		</v-toolbar-title>
		
		<!-- Item da direita -->
		<v-toolbar-title class="text-right pr-2">
			<span class="text pr-3">
				{{ credentials.unique_name }}
			</span>
			<v-icon color="primary" class="pr-3" size="small" icon="mdi mdi-bell"></v-icon>
			<v-icon color="primary" class="pr-3" size="small" icon="mdi mdi-white-balance-sunny"></v-icon>
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