<template>
  <div class="order-edit-page">
    <h1>Edit Order</h1>

    <!-- Error -->
    <div v-if="errorMessage" class="alert error">
      {{ errorMessage }}
    </div>

    <!-- Success -->
    <div v-if="successMessage" class="alert success">
      {{ successMessage }}
    </div>

    <!-- Loading -->
    <div v-if="loadingOrder">
      Loading order...
    </div>

    <div v-else-if="order">
      <!-- Basic info -->
      <section class="card">
        <h2>Order Info</h2>
        <p><strong>Order ID:</strong> {{ order.id }}</p>
        <p v-if="order.customerName"><strong>Customer:</strong> {{ order.customerName }}</p>
        <p v-if="order.tableNumber !== undefined && order.tableNumber !== null">
          <strong>Table:</strong> {{ order.tableNumber }}
        </p>
        <p v-if="order.createdAt">
          <strong>Created:</strong> {{ formatDate(order.createdAt) }}
        </p>
      </section>

      <!-- Items list (read only) -->
      <section class="card" v-if="order.items && order.items.length">
        <h2>Items</h2>
        <ul class="items-list">
          <li v-for="(item, index) in order.items" :key="index">
            <span class="item-name">{{ item.name }}</span>
            <span class="item-qty">x{{ item.quantity ?? 1 }}</span>
          </li>
        </ul>
      </section>

      <!-- Editable fields -->
      <section class="card">
        <h2>Edit Details</h2>

        <form @submit.prevent="saveOrder" class="edit-form">
          <!-- Status -->
          <div class="form-group">
            <label for="status">Status</label>
            <select id="status" v-model="status">
              <!-- Adjust options to match your backend enum -->
              <option value="Pending">Pending</option>
              <option value="InProgress">In Progress</option>
              <option value="Completed">Completed</option>
              <option value="Cancelled">Cancelled</option>
            </select>
          </div>

          <!-- Notes -->
          <div class="form-group">
            <label for="notes">Notes</label>
            <textarea
              id="notes"
              v-model="notes"
              placeholder="Kitchen notes, special requests, etc."
            ></textarea>
          </div>

          <div class="buttons-row">
            <button type="submit" :disabled="saving">
              {{ saving ? "Saving..." : "Save Changes" }}
            </button>

            <button
              type="button"
              class="secondary-btn"
              @click="router.back()"
            >
              Cancel
            </button>
          </div>
        </form>
      </section>
    </div>

    <div v-else>
      No order found.
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'

const route = useRoute()
const router = useRouter()

const orderId = route.params.id

const order = ref(null)
const loadingOrder = ref(false)
const saving = ref(false)
const errorMessage = ref('')
const successMessage = ref('')

// editable fields
const status = ref('Pending')
const notes = ref('')

const formatDate = (dateStr) => {
  try {
    const d = new Date(dateStr)
    return d.toLocaleString()
  } catch {
    return dateStr
  }
}

const loadOrder = async () => {
  loadingOrder.value = true
  errorMessage.value = ''

  try {
    // 🔧 change this URL if your backend uses a different route
    const response = await fetch(`/api/orders/${orderId}`)

    if (!response.ok) {
      const body = await response.json().catch(() => null)
      throw new Error(body?.message || 'Failed to load order.')
    }

    const data = await response.json()
    order.value = data

    // initialize editable fields from loaded order
    status.value = data.status ?? 'Pending'
    notes.value = data.notes ?? ''
  } catch (e) {
    errorMessage.value = e.message || 'Something went wrong loading the order.'
  } finally {
    loadingOrder.value = false
  }
}

const saveOrder = async () => {
  if (!order.value) return

  saving.value = true
  errorMessage.value = ''
  successMessage.value = ''

  try {
    const updatedOrder = {
      ...order.value,
      status: status.value,
      notes: notes.value,
    }

    // 🔧 change to PUT/PATCH and URL your backend expects
    const response = await fetch(`/api/orders/${orderId}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(updatedOrder),
    })

    if (!response.ok) {
      const body = await response.json().catch(() => null)
      throw new Error(body?.message || 'Failed to update order.')
    }

    successMessage.value = 'Order updated successfully.'

    // optionally go back automatically
    setTimeout(() => {
      router.back()
    }, 500)
  } catch (e) {
    errorMessage.value = e.message || 'Something went wrong saving the order.'
  } finally {
    saving.value = false
  }
}

onMounted(() => {
  loadOrder()
})
</script>

<style scoped>
.order-edit-page {
  max-width: 800px;
  margin: 0 auto;
  padding: 20px;
}

h1 {
  margin-bottom: 1rem;
}

.card {
  background: #ffffff;
  border-radius: 8px;
  padding: 14px 16px;
  margin-bottom: 16px;
  box-shadow: 0 1px 3px rgba(0,0,0,0.05);
}

.items-list {
  list-style: none;
  padding-left: 0;
}

.items-list li {
  display: flex;
  justify-content: space-between;
  padding: 4px 0;
}

.item-name {
  font-weight: 500;
}

.item-qty {
  color: #555;
}

.edit-form {
  display: flex;
  flex-direction: column;
}

.form-group {
  display: flex;
  flex-direction: column;
  margin-bottom: 0.9rem;
}

.form-group label {
  font-weight: 600;
  margin-bottom: 0.25rem;
}

.form-group select,
.form-group textarea {
  padding: 0.4rem;
  border-radius: 6px;
  border: 1px solid #ccc;
}

.form-group textarea {
  min-height: 80px;
  resize: vertical;
}

.buttons-row {
  display: flex;
  gap: 0.5rem;
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

.secondary-btn {
  background: #e0e0e0;
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
