// Plugins
import { registerPlugins } from '@/plugins'

// Components
import App from './App.vue'
import { getPessoasByUsuarioId } from '../src/api/controllers/pessoa';

// Composables
import { createApp } from 'vue'
import './styles/settings.scss';
import VueTheMask from 'vue-the-mask'
import PhosphorIcons from "@phosphor-icons/vue"


const app = createApp(App)

app.config.globalProperties.$api = {
	Pessoa: getPessoasByUsuarioId,
};

registerPlugins(app)

app.use(VueTheMask)
app.use(PhosphorIcons)

app.mount('#app')
