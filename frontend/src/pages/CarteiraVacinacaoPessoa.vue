<template>
	<v-container class="fill-height" v-if="load">
		<v-responsive
			color="white"
			class="align-center fill-height mx-auto"
			max-width="900"
		>
			<v-card color="#F8F7F5" class="pa-3" flat>
				<v-row>
					<v-col cols="6">
						<span>Carteira de Vacinação</span>
					</v-col>
					<v-col cols="">
						dsadadasdasdad
					</v-col>
				</v-row>
			</v-card>
		</v-responsive>
	</v-container>
</template>

<script>
import { getPessoaById } from '@/api/controllers/pessoa';
export default {
	data () {
		return {
			model: {},
			load: false
		}
	},
	async mounted () {
		this.model = await getPessoaById(this.$route.query.id);
		this.load = true;
		console.log(this.model)
  }
}
</script>