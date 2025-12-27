<template>
  <div class="profile-container">
    <h1>Profile</h1>
    <button @click="logout" class="logout-btn">Logout</button>
    <div v-if="!loading && profile">
      <div v-if="!editing">
        <p><strong>Name:</strong> {{ profile.fullName }}</p>
        <p><strong>Phone:</strong> {{ profile.phoneNumber }}</p>
        <button @click="editing = true">Edit</button>
      </div>
      <form v-else @submit.prevent="handleSubmit">
        <div>
          <label for="name">Name:</label>
          <input id="name" v-model="editData.fullName" required />
        </div>
        <div>
          <label for="phone">Phone:</label>
          <input id="phone" v-model="editData.phoneNumber" required @input="checkPhoneChange" />
        </div>
        <div v-if="phoneChanged && !otpSent">
          <button type="button" @click="sendOtpForPhone" :disabled="sendingOtp">Send OTP to new phone</button>
        </div>
        <div v-if="phoneChanged && otpSent">
          <label for="code">OTP Code:</label>
          <input id="code" v-model="otpCode" type="text" required placeholder="Enter OTP code" />
        </div>
        <button type="submit" :disabled="updating || (phoneChanged && !otpCode)">Save</button>
        <button type="button" @click="cancelEdit">Cancel</button>
      </form>
    </div>
    <p v-if="error">{{ error }}</p>
  </div>
</template>

<script setup>
import { ref, onMounted, reactive } from 'vue'
import axios from 'axios'

const profile = ref(null)
const editing = ref(false)
const loading = ref(true)
const updating = ref(false)
const error = ref('')

const editData = reactive({
  fullName: '',
  phoneNumber: ''
})

const token = ref('')
const phoneChanged = ref(false)
const otpSent = ref(false)
const otpCode = ref('')
const sendingOtp = ref(false)

const fetchProfile = async () => {
  try {
    const response = await axios.get('http://localhost:5222/api/profile', {
      headers: {
        Authorization: `Bearer ${token.value}`
      }
    })
    profile.value = response.data
    editData.fullName = response.data.fullName
    editData.phoneNumber = response.data.phoneNumber
  } catch (err) {
    // If server is down or any error, redirect to login
    navigateTo('/login')
  } finally {
    loading.value = false
  }
}

const checkPhoneChange = () => {
  phoneChanged.value = editData.phoneNumber !== profile.value.phoneNumber
  if (!phoneChanged.value) {
    otpSent.value = false
    otpCode.value = ''
  }
}

const sendOtpForPhone = async () => {
  sendingOtp.value = true
  error.value = ''
  try {
    await axios.post('http://localhost:5222/api/profile/send-otp', {
      phoneNumber: editData.phoneNumber
    }, {
      headers: {
        Authorization: `Bearer ${token.value}`
      }
    })
    otpSent.value = true
  } catch (err) {
    error.value = err.response?.data || 'Failed to send OTP.'
  } finally {
    sendingOtp.value = false
  }
}

const handleSubmit = async () => {
  if (phoneChanged.value && !otpCode.value) {
    error.value = 'Please enter the OTP code.'
    return
  }
  updating.value = true
  error.value = ''
  try {
    await axios.put('http://localhost:5222/api/profile', {
      fullName: editData.fullName,
      phoneNumber: editData.phoneNumber,
      code: phoneChanged.value ? otpCode.value : null
    }, {
      headers: {
        Authorization: `Bearer ${token.value}`
      }
    })
    profile.value = { ...editData }
    editing.value = false
    phoneChanged.value = false
    otpSent.value = false
    otpCode.value = ''
  } catch (err) {
    error.value = err.response?.data || 'Failed to update profile.'
  } finally {
    updating.value = false
  }
}

const cancelEdit = () => {
  editData.fullName = profile.value.fullName
  editData.phoneNumber = profile.value.phoneNumber
  editing.value = false
  phoneChanged.value = false
  otpSent.value = false
  otpCode.value = ''
}

const logout = () => {
  localStorage.removeItem('token')
  navigateTo('/login')
}

onMounted(() => {
  token.value = localStorage.getItem('token')
  if (!token.value) {
    navigateTo('/login')
  } else {
    fetchProfile()
  }
})
</script>

<style scoped>
.profile-container {
  max-width: 600px;
  margin: 0 auto;
  padding: 20px;
}
.logout-btn {
  background-color: #dc3545;
  color: white;
  border: none;
  padding: 10px 20px;
  cursor: pointer;
  margin-bottom: 20px;
}
.logout-btn:hover {
  background-color: #c82333;
}
form {
  margin-top: 20px;
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
  margin-right: 10px;
  background-color: #007bff;
  color: white;
  border: none;
  cursor: pointer;
}
button:disabled {
  background-color: #ccc;
}
</style>