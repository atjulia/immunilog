// Composables
import { createRouter, createWebHistory } from 'vue-router/auto'
import { routes as autoRoutes } from 'vue-router/auto-routes'

const routes = [
  {
    path: '/',
    redirect: '/login'
  },
  {
    path: '/login',
    name: 'Login',
    component: () => import('@/pages/Login.vue')
  },
  {
    path: '/home',
    name: 'Home',
    component: () => import('@/pages/Home.vue')
  },
  {
    path: '/carteira',
    name: 'CarteiraVacinacao',
    component: () => import('@/pages/CarteiraVacinacaoPessoa.vue') 
  },
  ...autoRoutes
]

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
})

router.beforeEach((to, from, next) => {
  const credentials = JSON.parse(localStorage.getItem('credentials'));
  if (!credentials && to.name !== 'Login') {
    next('/login')
  } else {
    next()
  }
});

export default router
