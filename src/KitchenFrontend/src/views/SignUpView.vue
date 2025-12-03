<script setup>
import { ref } from 'vue'
import { RouterLink } from 'vue-router'

const name = ref('')
const email = ref('')
const password = ref('')
const confirmPassword = ref('')

function handleSignUp() {
  if (password.value !== confirmPassword.value) {
    alert('Passwords do not match')
    return
  }

  console.log('Sign up:', {
    name: name.value,
    email: email.value,
    password: password.value,
  })

  // TODO: call backend API, e.g. POST /auth/register
}
</script>

<template>
  <div class="signup-page">
    <h2>Create an Account</h2>

    <form @submit.prevent="handleSignUp" class="signup-form">
      <label>
        Name
        <input v-model="name" type="text" required />
      </label>

      <label>
        Email
        <input v-model="email" type="email" required />
      </label>

      <label>
        Password
        <input v-model="password" type="password" required />
      </label>

      <label>
        Confirm Password
        <input v-model="confirmPassword" type="password" required />
      </label>

      <button type="submit">Sign Up</button>
    </form>

    <p style="margin-top: 12px;">
      Already have an account?
      <RouterLink to="/login">Log In</RouterLink>
    </p>
  </div>
</template>

<style scoped>
.signup-page {
  max-width: 400px;
  margin: 80px auto;
  padding: 24px;
  border-radius: 12px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.08);
  background: white;
}

.signup-form {
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
