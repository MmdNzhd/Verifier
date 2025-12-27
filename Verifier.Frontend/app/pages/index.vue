<template>
  <div class="home-container">
    <header class="hero">
      <h1>Verifier</h1>
      <p class="tagline">Simple OTP-based authentication demo</p>
    </header>

    <nav class="actions">
      <NuxtLink v-if="hasToken" to="/profile" class="btn primary">Go to Profile</NuxtLink>
      <NuxtLink v-else to="/login" class="btn primary">Login</NuxtLink>
      <button v-if="hasToken" class="btn" @click="logout">Logout</button>
    </nav>

    <section class="info">
      <p>
        This frontend talks to the API at <code>http://localhost:5222</code> to send and verify OTPs,
        and to fetch/update your profile.
      </p>
      <ul>
        <li>Login via phone + OTP</li>
        <li>View and edit profile</li>
        <li>Optional phone change with OTP verification</li>
      </ul>
    </section>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'

const hasToken = ref(false)

onMounted(() => {
  try {
    hasToken.value = Boolean(localStorage.getItem('token'))
  } catch {}
})

const logout = () => {
  try {
    localStorage.removeItem('token')
  } catch {}
  hasToken.value = false
}
</script>

<style scoped>
.home-container {
  max-width: 800px;
  margin: 0 auto;
  padding: 24px;
}
.hero {
  text-align: center;
  margin-bottom: 24px;
}
.tagline {
  color: #666;
  margin-top: 8px;
}
.actions {
  display: flex;
  gap: 12px;
  justify-content: center;
  margin-bottom: 24px;
}
.btn {
  padding: 10px 16px;
  border: 1px solid #ddd;
  background: #f7f7f7;
  color: #333;
  text-decoration: none;
  cursor: pointer;
}
.btn.primary {
  background: #007bff;
  border-color: #007bff;
  color: #fff;
}
.btn:hover {
  opacity: 0.9;
}
.info code {
  background: #f0f0f0;
  padding: 2px 6px;
  border-radius: 4px;
}
</style>
