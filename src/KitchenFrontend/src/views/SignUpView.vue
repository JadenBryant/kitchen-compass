<script setup>
import { ref } from "vue"
import { useRouter } from "vue-router"

const name = ref("")
const email = ref("")
const password = ref("")
const confirmPassword = ref("")

const errorMessage = ref("")
const successMessage = ref("")
const isLoading = ref(false)

const router = useRouter()

async function handleSignUp() {
  console.log("Sign up clicked") // debug log

  errorMessage.value = ""
  successMessage.value = ""

  if (!name.value || !email.value || !password.value || !confirmPassword.value) {
    errorMessage.value = "Please fill out all fields."
    return
  }

  if (password.value !== confirmPassword.value) {
    errorMessage.value = "Passwords do not match."
    return
  }

  isLoading.value = true

  try {
    const response = await fetch("/api/auth/register", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        email: email.value,
        password: password.value,
        // name is ignored by backend for now; we can store it later if needed
      }),
    })

    const data = await response.json()
    console.log("register response:", data)

    if (!response.ok) {
      const msg =
        data.error?.message || "Sign up failed. Please check your inputs."
      errorMessage.value = msg
      return
    }

    successMessage.value = "Account created! Redirecting to login..."

    setTimeout(() => {
      router.push("/login")
    }, 1500)
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
  <div class="signup-page">
    <h1 class="site-title">Kitchen Compass</h1>

    <div class="signup-card">
      <h2 class="signup-title">Create an Account</h2>

      <div class="signup-form">
        <label class="field">
          <span>Name</span>
          <input
            v-model="name"
            type="text"
            placeholder="Your name"
            required
          />
        </label>

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

        <label class="field">
          <span>Confirm Password</span>
          <input
            v-model="confirmPassword"
            type="password"
            required
          />
        </label>

        <button
          class="signup-button"
          :disabled="isLoading"
          @click.prevent="handleSignUp"
        >
          {{ isLoading ? "Creating account..." : "Sign Up" }}
        </button>

        <p v-if="errorMessage" class="error-message">
          {{ errorMessage }}
        </p>

        <p v-if="successMessage" class="success-message">
          {{ successMessage }}
        </p>

        <p class="login-link">
          Already have an account?
          <router-link to="/login">Log In</router-link>
        </p>
      </div>
    </div>
  </div>
</template>

<style scoped>
.signup-page {
  display: flex;
  flex-direction: column;
  align-items: center;
  padding-top: 4rem;
}

.site-title {
  font-size: 2.5rem;
  margin-bottom: 2rem;
}

.signup-card {
  max-width: 400px;
  width: 100%;
  border-radius: 12px;
  padding: 2rem;
  box-shadow: 0 10px 30px rgba(0, 0, 0, 0.08);
  background: white;
}

.signup-title {
  text-align: center;
  margin-bottom: 1.5rem;
}

.signup-form {
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

.signup-button {
  width: 100%;
  padding: 0.6rem 0.75rem;
  border-radius: 6px;
  border: 1px solid black;
  cursor: pointer;
  background: white;
}

.error-message {
  color: #c0392b;
  font-size: 0.9rem;
  margin-top: 0.5rem;
}

.success-message {
  color: #2ecc71;
  font-size: 0.9rem;
  margin-top: 0.5rem;
}

.login-link {
  font-size: 0.9rem;
  text-align: center;
  margin-top: 0.75rem;
}
</style>
