import { createRouter, createWebHistory } from 'vue-router'
import LoginView from '../views/LoginView.vue'
import SignUpView from '../views/SignUpView.vue'
import MenuView from '../views/MenuView.vue'
import AddMenuItemView from '../views/AddMenuItemView.vue'
import OrderEditView from '../views/OrderEditView.vue'

const routes = [
  { path: '/', redirect: '/login' },
  { path: '/login', name: 'Login', component: LoginView },
  { path: '/signup', name: 'SignUp', component: SignUpView },
  { path: '/menu', name: 'Menu', component: MenuView },
  { path: '/menu/new', name: 'menu-new', component: AddMenuItemView},
  { path: '/orders/:id/edit', name: 'order-edit', component: OrderEditView }, 
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router
