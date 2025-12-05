<script setup>
import { ref } from "vue"
import { useRouter } from "vue-router"

const email = ref("")
const password = ref("")
const errorMessage = ref("")
const isLoading = ref(false)

const router = useRouter()

async function handleLogin() {
  errorMessage.value = ""
  isLoading.value = true

  try {
    const response = await fetch("/api/auth/login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        email: email.value,
        password: password.value,
      }),
    })

    const data = await response.json()

    if (!response.ok) {
      // Firebase-style error message
      const msg =
        data.error?.message || "Login failed. Check your email and password."
      errorMessage.value = msg
      return
    }

    const idToken = data.idToken

    if (!idToken) {
      errorMessage.value = "Login succeeded but no token was returned."
      return
    }

    // Save token (simple approach for now)
    localStorage.setItem("kc_idToken", idToken)

    // Go to menu page after login
    router.push("/menu")
  } catch (err) {
    console.error(err)
    errorMessage.value =
      "Network error. Make sure Docker is running and try again."
  } finally {
    isLoading.value = false
  }
}
</script>

<template>
  <div class="login-page">
    <h1 class="site-title">Kitchen Compass</h1>

    <div class="login-card">
      <h2 class="login-title">Kitchen Login</h2>

      <form @submit.prevent="handleLogin" class="login-form">
        <label class="field">
          <span>Email</span>
          <input
            v-model="email"
            type="email"
            placeholder="you@example.com"
            required
          />
        </label>

        <label class="field">
          <span>Password</span>
          <input
            v-model="password"
            type="password"
            required
          />
        </label>

        <button type="submit" :disabled="isLoading" class="login-button">
          {{ isLoading ? "Logging in..." : "Log In" }}
        </button>

        <p v-if="errorMessage" class="error-message">
          {{ errorMessage }}
        </p>

        <p class="signup-link">
          Don’t have an account?
          <router-link to="/signup">Sign Up</router-link>
        </p>
      </form>
    </div>
  </div>
</template>

<style scoped>
/* You can tweak these or let your existing global styles handle layout */

.login-page {
  display: flex;
  flex-direction: column;
  align-items: center;
  padding-top: 4rem;
}

.site-title {
  font-size: 2.5rem;
  margin-bottom: 2rem;
}

.login-card {
  max-width: 400px;
  width: 100%;
  border-radius: 12px;
  padding: 2rem;
  box-shadow: 0 10px 30px rgba(0, 0, 0, 0.08);
  background: white;
}

.login-title {
  text-align: center;
  margin-bottom: 1.5rem;
}

.login-form {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.field span {
  display: block;
  font-size: 0.9rem;
  margin-bottom: 0.25rem;
}

.field input {
  width: 100%;
  padding: 0.5rem 0.75rem;
  border-radius: 6px;
  border: 1px solid #ccc;
}

.login-button {
  width: 100%;
  padding: 0.6rem 0.75rem;
  border-radius: 6px;
  border: none;
  cursor: pointer;
}

.error-message {
  color: #c0392b;
  font-size: 0.9rem;
  margin-top: 0.5rem;
}

.signup-link {
  font-size: 0.9rem;
  text-align: center;
  margin-top: 0.75rem;
}
</style>
