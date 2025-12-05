<script setup>
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()
const menus = ref([])
const allMenuItems = ref([])
const selectedMenu = ref(null)
const selectedMenuItems = ref([])
const newMenuName = ref('')
const showCreateDialog = ref(false)
const showItemsDialog = ref(false)

const API_BASE = 'http://localhost:8080/api'

// Fetch all menus
async function fetchMenus() {
  try {
    const response = await fetch(`${API_BASE}/menu/get_all`)
    menus.value = await response.json()
  } catch (error) {
    console.error('Failed to fetch menus:', error)
  }
}

// Fetch all menu items
async function fetchAllMenuItems() {
  try {
    const response = await fetch(`${API_BASE}/menuitem/get_all`)
    allMenuItems.value = await response.json()
  } catch (error) {
    console.error('Failed to fetch menu items:', error)
  }
}

// Create new menu
async function createMenu() {
  if (!newMenuName.value.trim()) return
  
  try {
    const response = await fetch(`${API_BASE}/menu/create_menu`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(newMenuName.value)
    })
    
    if (response.ok) {
      newMenuName.value = ''
      showCreateDialog.value = false
      await fetchMenus()
    }
  } catch (error) {
    console.error('Failed to create menu:', error)
  }
}

// Open menu items dialog
async function manageMenuItems(menu) {
  selectedMenu.value = menu
  
  try {
    const response = await fetch(`${API_BASE}/menu?menuId=${menu.id}`)
    const menuData = await response.json()
    selectedMenuItems.value = menuData.items || []
    showItemsDialog.value = true
  } catch (error) {
    console.error('Failed to fetch menu details:', error)
  }
}

// Check if item is in selected menu
function isItemInMenu(itemId) {
  return selectedMenuItems.value.includes(itemId)
}

// Add item to menu
async function addItemToMenu(itemId) {
  try {
    const response = await fetch(
      `${API_BASE}/menu/add_item?menuId=${selectedMenu.value.id}&itemId=${itemId}`,
      { method: 'POST' }
    )
    
    if (response.ok) {
      const data = await response.json()
      selectedMenuItems.value = data.items
    }
  } catch (error) {
    console.error('Failed to add item:', error)
  }
}

// Remove item from menu
async function removeItemFromMenu(itemId) {
  try {
    const response = await fetch(
      `${API_BASE}/menu/remove_item?menuId=${selectedMenu.value.id}&itemId=${itemId}`,
      { method: 'DELETE' }
    )
    
    if (response.ok) {
      const data = await response.json()
      selectedMenuItems.value = data.items
    }
  } catch (error) {
    console.error('Failed to remove item:', error)
  }
}

// Delete menu
async function deleteMenu(menuId) {
  if (!confirm('Are you sure you want to delete this menu?')) return
  
  // Note: You'll need to add a DELETE endpoint in MenuController
  console.warn('Delete endpoint not implemented in backend')
}

// Edit menu name
async function editMenuName(menu) {
  const newName = prompt('Enter new menu name:', menu.name)
  if (!newName || newName === menu.name) return
  
  try {
    const response = await fetch(
      `${API_BASE}/menu/edit_menu_name?menuId=${menu.id}`,
      {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(newName)
      }
    )
    
    if (response.ok) {
      await fetchMenus()
    }
  } catch (error) {
    console.error('Failed to update menu name:', error)
  }
}

onMounted(async () => {
  await fetchMenus()
  await fetchAllMenuItems()
})
</script>

<template>
  <div class="admin-container">
    <div class="header-row">
      <h2>Menu Management</h2>
      <button class="add-btn" @click="showCreateDialog = true">
        + Create New Menu
      </button>
    </div>

    <div class="menus-grid">
      <div v-for="menu in menus" :key="menu.id" class="menu-card">
        <h3>{{ menu.name }}</h3>
        <p class="item-count">{{ menu.items?.length || 0 }} items</p>
        
        <div class="menu-actions">
          <button class="manage-btn" @click="manageMenuItems(menu)">
            Manage Items
          </button>
          <button class="edit-btn" @click="editMenuName(menu)">
            Edit Name
          </button>
          <button class="delete-btn" @click="deleteMenu(menu.id)">
            Delete
          </button>
        </div>
      </div>
    </div>

    <!-- Create Menu Dialog -->
    <div v-if="showCreateDialog" class="modal-overlay" @click="showCreateDialog = false">
      <div class="modal-content" @click.stop>
        <h3>Create New Menu</h3>
        <input
          v-model="newMenuName"
          type="text"
          placeholder="Menu name"
          @keyup.enter="createMenu"
        />
        <div class="modal-actions">
          <button class="cancel-btn" @click="showCreateDialog = false">Cancel</button>
          <button class="confirm-btn" @click="createMenu">Create</button>
        </div>
      </div>
    </div>

    <!-- Manage Items Dialog -->
    <div v-if="showItemsDialog" class="modal-overlay" @click="showItemsDialog = false">
      <div class="modal-content large" @click.stop>
        <h3>Manage Items - {{ selectedMenu?.name }}</h3>
        
        <div class="items-list">
          <div
            v-for="item in allMenuItems"
            :key="item.id"
            class="item-row"
            :class="{ selected: isItemInMenu(item.id) }"
          >
            <div class="item-info">
              <span class="item-name">{{ item.name }}</span>
              <span class="item-price">${{ item.price.toFixed(2) }}</span>
            </div>
            
            <button
              v-if="!isItemInMenu(item.id)"
              class="add-item-btn"
              @click="addItemToMenu(item.id)"
            >
              Add
            </button>
            <button
              v-else
              class="remove-item-btn"
              @click="removeItemFromMenu(item.id)"
            >
              Remove
            </button>
          </div>
        </div>
        
        <div class="modal-actions">
          <button class="close-btn" @click="showItemsDialog = false">Close</button>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.admin-container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 20px;
}

.header-row {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 30px;
}

.add-btn {
  background-color: #42b883;
  color: white;
  padding: 10px 16px;
  border: none;
  border-radius: 6px;
  cursor: pointer;
  font-weight: 500;
}

.menus-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
  gap: 20px;
}

.menu-card {
  background: white;
  padding: 20px;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0,0,0,0.1);
}

.menu-card h3 {
  margin: 0 0 8px 0;
}

.item-count {
  color: #666;
  margin: 0 0 16px 0;
}

.menu-actions {
  display: flex;
  gap: 8px;
  flex-wrap: wrap;
}

.menu-actions button {
  padding: 6px 12px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-size: 14px;
}

.manage-btn {
  background-color: #42b883;
  color: white;
  flex: 1;
}

.edit-btn {
  background-color: #2c7be5;
  color: white;
}

.delete-btn {
  background-color: #ff6b6b;
  color: white;
}

.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(0,0,0,0.5);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 1000;
}

.modal-content {
  background: white;
  padding: 24px;
  border-radius: 8px;
  min-width: 400px;
  max-width: 90vw;
}

.modal-content.large {
  min-width: 600px;
  max-height: 80vh;
  overflow-y: auto;
}

.modal-content input {
  width: 100%;
  padding: 10px;
  margin: 16px 0;
  border: 1px solid #ddd;
  border-radius: 4px;
  font-size: 16px;
}

.modal-actions {
  display: flex;
  gap: 10px;
  justify-content: flex-end;
  margin-top: 16px;
}

.modal-actions button {
  padding: 8px 16px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

.cancel-btn, .close-btn {
  background-color: #ddd;
  color: #333;
}

.confirm-btn {
  background-color: #42b883;
  color: white;
}

.items-list {
  margin: 16px 0;
  max-height: 400px;
  overflow-y: auto;
}

.item-row {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 12px;
  border: 1px solid #eee;
  border-radius: 4px;
  margin-bottom: 8px;
}

.item-row.selected {
  background-color: #f0f9ff;
  border-color: #42b883;
}

.item-info {
  display: flex;
  gap: 16px;
  align-items: center;
}

.item-name {
  font-weight: 500;
}

.item-price {
  color: #666;
}

.add-item-btn {
  background-color: #42b883;
  color: white;
  padding: 6px 16px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

.remove-item-btn {
  background-color: #ff6b6b;
  color: white;
  padding: 6px 16px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}
</style>