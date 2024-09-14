/**
 * main.js
 *
 * Bootstraps Vuetify and other plugins then mounts the App`
 */

// Plugins
import { registerPlugins } from '@/plugins'

// Components
import App from './App.vue'

// Composables
import { createApp } from 'vue'
import PhosphorIcons from "@phosphor-icons/vue"
import './styles/settings.scss';

const app = createApp(App)

registerPlugins(app)

app.use(PhosphorIcons)

app.mount('#app')
