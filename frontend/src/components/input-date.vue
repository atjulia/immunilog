<template>
  <v-container class="pa-0">
    <v-form v-model="formValid">
      <v-text-field
        :label="label"
        v-model="formattedDate"
        variant="outlined"
        @click="showDatePicker = !showDatePicker"
				@focus="showDatePicker = true"
        :rules="rules"
        readonly
      />
      <v-date-picker
        v-if="showDatePicker"
        v-model="selectedDate"
        :min="minDate"
				:max="maxDate"
        @change="onDateSelected"
        color="primary"
        locale="pt-br"
        @click:outside="showDatePicker = !showDatePicker"
      ></v-date-picker>
    </v-form>
  </v-container>
</template>

<script>
export default {
	props: {
		modelValue: {
      type: String,
      default: ''
    },
		label: { type: String, default: '' },
    minDate: { type: String, default: '1910-01-01' },
    rules: { type: Array, default: [] }
	},
  data() {
    return {
      selectedDate: null,
      formattedDate: "",
      showDatePicker: false,
      formValid: false,
      minDate: this.minDate,
			maxDate: new Date()
    };
  },
  methods: {
    formatDate(date) {
			const d = new Date(date);
			return d.toLocaleDateString('pt-BR');
		},
  },
  watch: {
		'selectedDate' (newVal) {
			if (newVal) {
        this.formattedDate = this.formatDate(newVal);
				this.$emit('update:modelValue', this.formattedDate)
			}
		}
  }
};
</script>

<style scoped>
.v-date-picker {
  position: absolute;
  z-index: 9999;
}
</style>
