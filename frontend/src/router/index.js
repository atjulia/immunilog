// Composables
import { createRouter, createWebHistory } from 'vue-router/auto'
import { routes as autoRoutes } from 'vue-router/auto-routes'

// Definindo as rotas manualmente
const routes = [
  {
    path: '/', // Rota inicial
    redirect: '/login' // Redireciona para a página de login
  },
  {
    path: '/login', // Página de login
    name: 'Login',
    component: () => import('@/pages/Login.vue') // Certifique-se de que o caminho para o componente Login está correto
  },
  {
    path: '/home', // Página home
    name: 'Home',
    component: () => import('@/pages/Home.vue') // Certifique-se de que o caminho para o componente Home está correto
  },
  // Inclua todas as outras rotas automáticas
  ...autoRoutes
]

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes, // Agora inclui as rotas manuais e automáticas
})

// Workaround for https://github.com/vitejs/vite/issues/11804
router.onError((err, to) => {
  if (err?.message?.includes?.('Failed to fetch dynamically imported module')) {
    if (!localStorage.getItem('vuetify:dynamic-reload')) {
      console.log('Reloading page to fix dynamic import error')
      localStorage.setItem('vuetify:dynamic-reload', 'true')
      location.assign(to.fullPath)
    } else {
      console.error('Dynamic import error, reloading page did not fix it', err)
    }
  } else {
    console.error(err)
  }
})

router.isReady().then(() => {
  localStorage.removeItem('vuetify:dynamic-reload')
})

export default router
