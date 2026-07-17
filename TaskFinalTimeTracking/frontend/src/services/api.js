import axios from 'axios'

// Локально — .env.development, на проде — переменная окружения VITE_API_URL на хостинге
const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL || 'https://localhost:5001/api'
})

export default api
