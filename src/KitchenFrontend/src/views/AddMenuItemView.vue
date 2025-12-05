<template>
  <div class="add-item-page">
    <h1>Add New Menu Item</h1>

    <!-- Error message -->
    <div v-if="errorMessage" class="alert error">
      {{ errorMessage }}
    </div>

    <!-- Success message -->
    <div v-if="successMessage" class="alert success">
      {{ successMessage }}
    </div>

    <form @submit.prevent="submitItem" class="form-container">
      <!-- Name -->
      <div class="form-group">
        <label>Item Name *</label>
        <input
          v-model="name"
          type="text"
          required
          placeholder="Spicy Chicken Sandwich"
        />
      </div>

      <!-- Category -->
      <div class="form-group">
        <label>Category *</label>
        <input
          v-model="category"
          type="text"
          required
          placeholder="Sandwich, Wrap, Soup..."
        />
      </div>

      <!-- Price -->
      <div class="form-group">
        <label>Price (USD) *</label>
        <input
          v-model.number="price"
          type="number"
          step="0.01"
          min="0"
          required
          placeholder="9.99"
        />
      </div>

      <!-- Calories -->
      <div class="form-group">
        <label>Calories *</label>
        <input
          v-model.number="calories"
          type="number"
          min="0"
          required
          placeholder="720"
        />
      </div>

      <!-- Active checkbox -->
      <div class="form-group checkbox-row">
        <label>Active?</label>
        <input v-model="active" type="checkbox" />
      </div>

      <!-- Buttons -->
      <div class="buttons-row">
        <button type="submit" :disabled="loading">
          {{ loading ? "Saving..." : "Add Item" }}
        </button>

        <button
          type="button"
          class="cancel-btn"
          @click="router.push('/menu')"
        >
          Cancel
        </button>
      </div>
    </form>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()

// Form fields
const name = ref('')
const category = ref('')
const price = ref(null)
const calories = ref(null)
const active = ref(true)

// UI state
const loading = ref(false)
const errorMessage = ref('')
const successMessage = ref('')

// Basic validation
const validate = () => {
  if (!name.value.trim()) return 'Name is required.'
  if (!category.value.trim()) return 'Category is required.'
  if (price.value === null || price.value <= 0) return 'Price must be greater than 0.'
  if (calories.value === null || calories.value < 0) return 'Calories must be 0 or more.'
  return null
}

const submitItem = async () => {
  const err = validate()
  if (err) {
    errorMessage.value = err
    return
  }

  loading.value = true
  errorMessage.value = ''
  successMessage.value = ''

  try {
    const body = {
      name: name.value,
      category: category.value,
      price: price.value,
      calories: calories.value,
      active: active.value,
    }

    // 🔧 Change this URL to match your backend if needed
    const response = await fetch('/api/menu-items', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(body),
    })

    if (!response.ok) {
      const data = await response.json().catch(() => null)
      throw new Error(data?.message || 'Failed to create item.')
    }

    successMessage.value = 'Item created successfully!'

    // Go back to menu after a short delay
    setTimeout(() => {
      router.push('/menu')
    }, 600)
  } catch (e) {
    errorMessage.value = e.message || 'Something went wrong.'
  } finally {
    loading.value = false
  }
}
</script>

<style scoped>
.add-item-page {
  max-width: 600px;
  margin: 0 auto;
  padding: 20px;
}

h1 {
  margin-bottom: 1rem;
}

.form-container {
  display: flex;
  flex-direction: column;
}

.form-group {
  margin-bottom: 0.8rem;
  display: flex;
  flex-direction: column;
}

.form-group input {
  padding: 0.4rem;
  border: 1px solid #ccc;
  border-radius: 6px;
}

.checkbox-row {
  flex-direction: row;
  align-items: center;
  gap: 0.5rem;
}

.buttons-row {
  display: flex;
  gap: 0.5rem;
  margin-top: 1rem;
}

button {
  padding: 0.5rem 1rem;
  border-radius: 6px;
  border: none;
  cursor: pointer;
}

button[disabled] {
  opacity: 0.6;
  cursor: not-allowed;
}

.cancel-btn {
  background: #ddd;
}

.alert {
  padding: 0.6rem;
  border-radius: 6px;
  margin-bottom: 1rem;
}

.alert.error {
  background: #ffe5e5;
  border: 1px solid #ff9393;
}

.alert.success {
  background: #e5ffe9;
  border: 1px solid #86ff9c;
}
</style>
