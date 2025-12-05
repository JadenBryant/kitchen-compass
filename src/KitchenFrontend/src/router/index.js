import { createRouter, createWebHistory } from 'vue-router'
import LoginView from '../views/LoginView.vue'
import SignUpView from '../views/SignUpView.vue'
import MenuView from '../views/MenuView.vue'
import MenuViewCustomer from '../views/MenuViewCustomer.vue'
import AddMenuItemView from '../views/AddMenuItemView.vue'
import OrderEditView from '../views/OrderEditView.vue'

const routes = [
  { path: '/', redirect: '/login' },
  { path: '/login', name: 'Login', component: LoginView },
  { path: '/signup', name: 'SignUp', component: SignUpView },
  { path: '/manager_menu', name: 'Manager Menu', component: MenuView, meta: { requiresManager: true } },
  { path: '/customer_menu', name: 'Menu', component: MenuViewCustomer},
  { path: '/menu/new', name: 'menu-new', component: AddMenuItemView},
  { path: '/orders/:id/edit', name: 'order-edit', component: OrderEditView }, 
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

router.beforeEach((to, from, next) => {
  const userType = localStorage.getItem('userType')

  if (to.meta.requiresManager && userType !== 'manager') {
    // deny access
    return next('/login')  // or a "403" page if you want
  }

  next()
})

export default router
