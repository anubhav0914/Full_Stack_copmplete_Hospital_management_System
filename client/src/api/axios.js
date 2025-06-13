import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:5297/api', // your backend API base URL
});

api.interceptors.request.use(
  config => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export default api;

api.interceptors.response.use(
  response => response,
  error => {
    if (error.response?.status === 401) {
      // Token expired or invalid
      localStorage.removeItem('token');
      window.location.href = '/login';  // Redirect to login
    }
    return Promise.reject(error);
  }
);
