<script setup>
import { useRouter } from 'vue-router'
import { ref } from 'vue'

// Temporary mock data for now
const menuItems = ref([
  {
    id: '1',
    name: 'Spicy Chicken Sandwich',
    category: 'Sandwich',
    price: 9.99,
    calories: 720,
    active: true
  },
  {
    id: '2',
    name: 'Veggie Wrap',
    category: 'Wrap',
    price: 7.49,
    calories: 540,
    active: true
  },
  {
    id: '3',
    name: 'Tomato Soup',
    category: 'Soup',
    price: 4.99,
    calories: 310,
    active: false
  }
])

const router = useRouter()

// Navigate to Add New Item page
function goToAddPage() {
  router.push('/menu/new')
}

// Navigate to OrderEdit page (using the item's id)
function editItem(id) {
  router.push({ name: 'order-edit', params: { id } })
}

// Toggle active/inactive status
function toggleActive(item) {
  item.active = !item.active
}

// Remove an item
function deleteItem(id) {
  menuItems.value = menuItems.value.filter(i => i.id !== id)
}
</script>

<template>
  <div class="admin-container">
    <div class="header-row">
      <h2>Menu Management</h2>

      <button class="add-btn" @click="goToAddPage">
        + Add New Item
      </button>
    </div>

    <table class="menu-table">
      <thead>
        <tr>
          <th>Name</th>
          <th>Category</th>
          <th>Price</th>
          <th>Calories</th>
          <th>Active</th>
          <th>Actions</th>
        </tr>
      </thead>

      <tbody>
        <tr v-for="item in menuItems" :key="item.id">
          <td>{{ item.name }}</td>
          <td>{{ item.category }}</td>
          <td>${{ item.price.toFixed(2) }}</td>
          <td>{{ item.calories }}</td>

          <td>
            <button
              class="status-btn"
              :class="{ active: item.active }"
              @click="toggleActive(item)"
            >
              {{ item.active ? 'Active' : 'Inactive' }}
            </button>
          </td>

          <td class="action-buttons">
            <button
              class="edit-btn"
              @click="editItem(item.id)"
            >
              Edit
            </button>
            <button
              class="delete-btn"
              @click="deleteItem(item.id)"
            >
              Delete
            </button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<style scoped>
.admin-container {
  max-width: 900px;
  margin: 0 auto;
  padding: 20px;
}

.header-row {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 20px;
}

.add-btn {
  background-color: #42b883;
  color: white;
  padding: 8px 14px;
  border: none;
  border-radius: 6px;
  cursor: pointer;
}

.add-btn:hover {
  opacity: 0.9;
}

.menu-table {
  width: 100%;
  border-collapse: collapse;
  background: white;
  border-radius: 8px;
  overflow: hidden;
}

.menu-table th,
.menu-table td {
  padding: 12px 16px;
  border-bottom: 1px solid #eaeaea;
  text-align: left;
}

.status-btn {
  padding: 4px 10px;
  border-radius: 6px;
  border: none;
  cursor: pointer;
}

.status-btn.active {
  background-color: #42b883;
  color: white;
}

.status-btn:not(.active) {
  background-color: #ccc;
  color: black;
}

.action-buttons button {
  margin-right: 8px;
  padding: 6px 10px;
  border-radius: 6px;
  border: none;
  cursor: pointer;
}

.edit-btn {
  background-color: #2c7be5;
  color: white;
}

.delete-btn {
  background-color: #ff6b6b;
  color: white;
}
</style>
