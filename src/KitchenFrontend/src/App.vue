<script setup>
import { RouterView, RouterLink } from 'vue-router'
</script>

<template>
  <div class="app-container">
    <header class="navbar">
      <h1>Kitchen Compass</h1>

      <nav class="links">
        <RouterLink to="/login">Login</RouterLink>
        <RouterLink to="/signup">Sign Up</RouterLink>
        <RouterLink to="/menu">Menu</RouterLink>
      </nav>
    </header>

    <main class="page-content">
      <RouterView />
    </main>
  </div>
</template>

<style scoped>
.app-container {
  font-family: Arial, sans-serif;
  padding: 20px;
}

.navbar {
  display: flex;
  justify-content: flex-start;
  align-items: center;
  padding-bottom: 15px;
  margin-bottom: 20px;
  border-bottom: 2px solid #ddd;
  gap: 40px;
}
.links {
  display: flex;
  gap: 20px;
}

.links a {
  text-decoration: none;
  color: #333;
}

.links a:hover {
  color: #42b883;
}

.page-content {
  padding: 20px;
}
</style>
