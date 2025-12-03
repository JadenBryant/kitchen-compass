<script setup>
import { ref } from 'vue'
import {RouterLink} from 'vue-router'

const email = ref('')
const password = ref('')

function handleLogin() {
  console.log('Login clicked with:', email.value, password.value)
  // later: call real API & redirect
}
</script>

<template>
  <div class="login-page">
    <h2>Kitchen Login</h2>

    <form @submit.prevent="handleLogin" class="login-form">
      <label>
        Email
        <input v-model="email" type="email" placeholder="you@example.com" />
      </label>

      <label>
        Password
        <input v-model="password" type="password" placeholder="••••••••" />
      </label>

      <button type="submit">Log In</button>
     </form>
     <p style="margin-top: 12px;">
      Don’t have an account?
      <RouterLink to="/signup">Sign Up</RouterLink>
    </p>
  </div>
</template>

<style scoped>
.login-page {
  max-width: 350px;
  margin: 80px auto;
  padding: 24px;
  border-radius: 12px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.08);
  background: white;
}

.login-form {
  display: flex;
  flex-direction: column;
  gap: 12px;
}

input {
  width: 100%;
  padding: 8px;
  margin-top: 4px;
  box-sizing: border-box;
}

button {
  margin-top: 8px;
  padding: 8px;
  cursor: pointer;
}
</style>
