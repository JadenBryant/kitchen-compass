<script setup>
import { ref } from 'vue'

const menuItems = ref([
  { 
    id: 1,
    name: 'Spicy Chicken Sandwich',
    price: 9.99,
    category: 'Sandwich',
    active: true,
    image: '/images/Chicken-Sandwich.jpg',
    calories: 720,
    ingredients: [
      'Spicy fried chicken breast',
      'Brioche bun',
      'Pickles',
      'Coleslaw',
      'House spicy mayo'
    ],
    description: 'Crispy fried chicken tossed in our spicy sauce, topped with crunchy slaw and pickles on a toasted brioche bun.'
  },
  { 
    id: 2,
    name: 'Veggie Wrap',
    price: 7.49,
    category: 'Wrap',
    active: true,
    image: '/images/Veggie-Wrap.jpg',
    calories: 540,
    ingredients: [
      'Spinach tortilla',
      'Mixed greens',
      'Roasted peppers',
      'Cucumber',
      'Avocado',
      'Hummus'
    ],
    description: 'Fresh and light wrap stuffed with crunchy veggies, creamy hummus, and avocado in a spinach tortilla.'
  },
  { 
    id: 3,
    name: 'Tomato Soup',
    price: 4.99,
    category: 'Soup',
    active: false,
    image: '/images/Tomato-Soup.jpg',
    calories: 310,
    ingredients: [
      'Roasted tomatoes',
      'Vegetable stock',
      'Garlic',
      'Onion',
      'Cream',
      'Basil'
    ],
    description: 'Slow-simmered tomato soup with roasted tomatoes, herbs, and a touch of cream.'
  },
])

const expandedItemId = ref(null)

function toggleDetails(item) {
  expandedItemId.value = expandedItemId.value === item.id ? null : item.id
}
</script>

<template>
  <div class="menu-page">
    <h2>Menu</h2>

    <div class="menu-grid">
      <div
        v-for="item in menuItems"
        :key="item.id"
        class="menu-card"
      >
        <img :src="item.image" class="card-img" />

        <button class="card-name" @click="toggleDetails(item)">
          {{ item.name }}
        </button>

        <div
          class="card-details"
          :class="{ open: expandedItemId === item.id }"
        >
          <p><strong>Price:</strong> ${{ item.price.toFixed(2) }}</p>
          <p><strong>Calories:</strong> {{ item.calories }} kcal</p>
          <p><strong>Category:</strong> {{ item.category }}</p>

          <p class="card-description">
            {{ item.description }}
          </p>

          <h4>Ingredients</h4>
          <ul class="ingredients-list">
            <li v-for="(ingredient, idx) in item.ingredients" :key="idx">
              {{ ingredient }}
            </li>
          </ul>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.menu-page {
  max-width: 1000px;
  margin: 0 auto;
}

.menu-page h2 {
  text-align: center;
  margin-bottom: 20px;
}

.menu-grid {
  display: flex;
  flex-wrap: wrap;
  gap: 20px;
  justify-content: center;
  align-items: flex-start;
}

.menu-card {
  border: 1px solid #ddd;
  border-radius: 10px;
  background-color: #ffffff;
  padding: 12px;
  box-shadow: 0 3px 8px rgba(0, 0, 0, 0.03);
  display: flex;
  flex-direction: column;
  align-items: center;
  flex: 0 1 260px;
}

.card-img {
  width: 100%;
  height: 160px;
  object-fit: cover;
  border-radius: 8px;
}

.card-name {
  margin-top: 10px;
  margin-bottom: 4px;
  background: none;
  border: none;
  padding: 0;
  font: inherit;
  font-weight: 600;
  color: #2c7be5;
  cursor: pointer;
}

.card-name:hover {
  text-decoration: underline;
}

.card-details {
  overflow: hidden;
  max-height: 0;
  transition: max-height 0.25s ease;
  width: 100%;
  text-align: left;
  font-size: 0.9rem;
}

.card-details.open {
  max-height: 260px;
  margin-top: 6px;
  padding-top: 4px;
  padding-bottom: 4px;
  overflow-y: auto;
}

.card-description {
  margin-top: 6px;
  margin-bottom: 6px;
}

.ingredients-list {
  margin: 0;
  padding-left: 18px;
  margin-bottom: 8px;
}
</style>
