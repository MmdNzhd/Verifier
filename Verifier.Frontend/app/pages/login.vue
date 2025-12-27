<template>
  <div class="login-container">
    <h1>Login</h1>
    <div v-if="step === 1">
      <form @submit.prevent="sendOtp">
        <div>
          <label for="phone">Phone Number:</label>
          <input
            id="phone"
            v-model="phone"
            type="tel"
            required
            placeholder="Enter your phone number"
          />
        </div>
        <button type="submit" :disabled="loading">Send OTP</button>
      </form>
    </div>
    <div v-if="step === 2">
      <form @submit.prevent="verifyOtp">
        <div>
          <label for="code">OTP Code:</label>
          <input
            id="code"
            v-model="code"
            type="text"
            required
            placeholder="Enter OTP code"
          />
        </div>
        <button type="submit" :disabled="loading">Verify OTP</button>
      </form>
    </div>
    <p v-if="message">{{ message }}</p>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import axios from 'axios'

const phone = ref('')
const code = ref('')
const step = ref(1)
const loading = ref(false)
const message = ref('')

onMounted(async () => {
  const token = localStorage.getItem('token')
  if (token) {
    try {
      // Validate token by making a request to profile
      await axios.get('http://localhost:5222/api/profile', {
        headers: {
          Authorization: `Bearer ${token}`
        }
      })
      // Token is valid, redirect to profile
      navigateTo('/profile')
    } catch (error) {
      // Only remove token if it's invalid (401), not if server is down
      if (error.response && error.response.status === 401) {
        localStorage.removeItem('token')
      }
      // For other errors (server down, network issues), keep token and stay on login
    }
  }
})

const sendOtp = async () => {
  loading.value = true
  message.value = ''
  try {
    await axios.post('http://localhost:5222/api/auth/send-otp', {
      phoneNumber: phone.value
    })
    step.value = 2
    message.value = 'OTP sent to your phone.'
  } catch (error) {
    message.value = 'Failed to send OTP.'
  }
  loading.value = false
}

const verifyOtp = async () => {
  loading.value = true
  message.value = ''
  try {
    const response = await axios.post('http://localhost:5222/api/auth/verify-otp', {
      phoneNumber: phone.value,
      code: code.value
    })
    // Store token
    localStorage.setItem('token', response.data.token)
    message.value = 'Login successful!'
    // Redirect to profile
    await navigateTo('/profile')
  } catch (error) {
    message.value = 'Invalid OTP.'
  }
  loading.value = false
}
</script>

<style scoped>
.login-container {
  max-width: 400px;
  margin: 0 auto;
  padding: 20px;
}
form {
  margin-bottom: 20px;
}
div {
  margin-bottom: 10px;
}
label {
  display: block;
  margin-bottom: 5px;
}
input {
  width: 100%;
  padding: 8px;
  box-sizing: border-box;
}
button {
  padding: 10px 20px;
  background-color: #007bff;
  color: white;
  border: none;
  cursor: pointer;
}
button:disabled {
  background-color: #ccc;
}
</style>