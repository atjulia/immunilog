// Plugins
import { registerPlugins } from '@/plugins'

// Components
import App from './App.vue'
import { getPessoasByUsuarioId } from '../src/api/controllers/pessoa';

// Composables
import { createApp } from 'vue'
import PhosphorIcons from "@phosphor-icons/vue"
import './styles/settings.scss';

const app = createApp(App)

app.config.globalProperties.$api = {
	Pessoa: getPessoasByUsuarioId,
};

registerPlugins(app)

app.use(PhosphorIcons)

app.mount('#app')
