import React, { useState } from 'react';
import Navbar from '../components/Navbar';
import Footer from '../components/Footer';
import { useNavigate } from 'react-router-dom';
import api from '../api/axios';
import { toast, ToastContainer } from 'react-toastify';

export default function RegisterForm() {
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const [image, setImage] = useState(null);

  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    gender: '',
    addressLine1: '',
    addressLine2: '',
    phoneNumber: '',
    email: '',
    bloodGroup: '',
    password: ''
  });

  const handleChange = (e) => {
    setFormData(prev => ({
      ...prev,
      [e.target.name]: e.target.value
    }));
  };

  const handleRegister = async (e) => {
    e.preventDefault();

    if (!image) {
      alert('Please upload an image.');
      return;
    }

    const formDataToSend = new FormData();
    for (let key in formData) {
      formDataToSend.append(key, formData[key]);
    }
    formDataToSend.append('admissionDate', new Date().toISOString());
    formDataToSend.append('image', image);

    setLoading(true);
    try {
      console.log(formDataToSend)
      await api.post('/Patient/register', formDataToSend, {
        headers: {
          'Content-Type': 'multipart/form-data'
        }
      });
      toast.success('Registration successful!');
      navigate('/login');
      setFormData(
        {
          firstName: '',
          lastName: '',
          gender: '',
          addressLine1: '',
          addressLine2: '',
          phoneNumber: '',
          email: '',
          bloodGroup: '',
          password: ''
        })
    } catch (err) {
      console.error('Registration failed:', err);
      toast.error(err.response.data.message);
    } finally {
      setLoading(false);
    }
  };

  return (
    <>
     <ToastContainer position="top-right" autoClose={2000} />
      <div className='p-8 md:p-20 mt-20 relative'>
        <div className="max-w-4xl mx-auto bg-white shadow-lg rounded-lg p-10">
          <h2 className="text-3xl font-bold mb-8 text-center text-blue-700">User Registration</h2>

          <form className="grid grid-cols-1 md:grid-cols-2 gap-6" onSubmit={handleRegister}>
            <input
              type="text"
              name="firstName"
              placeholder="First Name"
              value={formData.firstName}
              onChange={handleChange}
              required
              className="p-2 border rounded"
            />
            <input
              type="text"
              name="lastName"
              placeholder="Last Name"
              value={formData.lastName}
              onChange={handleChange}
              required
              className="p-2 border rounded"
            />
            <input
              type="tel"
              name="phoneNumber"
              placeholder="Mobile Number"
              value={formData.phoneNumber}
              onChange={handleChange}
              required
              className="p-2 border rounded"
            />
            <input
              type="email"
              name="email"
              placeholder="Email"
              value={formData.email}
              onChange={handleChange}
              required
              className="p-2 border rounded"
            />
            <input
              type="text"
              name="bloodGroup"
              placeholder="Blood Group (optional)"
              value={formData.bloodGroup}
              onChange={handleChange}
              className="p-2 border rounded"
            />
            <select
              name="gender"
              value={formData.gender}
              onChange={handleChange}
              required
              className="p-2 border rounded"
            >
              <option value="">Select Gender</option>
              <option>Male</option>
              <option>Female</option>
              <option>Other</option>
            </select>
            <input
              type="password"
              name="password"
              placeholder="Password"
              value={formData.password}
              onChange={handleChange}
              required
              className="p-2 border rounded"
            />
            <input
              type="text"
              name="addressLine1"
              placeholder="Address Line 1"
              value={formData.addressLine1}
              onChange={handleChange}
              required
              className="p-2 border rounded md:col-span-2"
            />
            <input
              type="text"
              name="addressLine2"
              placeholder="Address Line 2"
              value={formData.addressLine2}
              onChange={handleChange}
              required
              className="p-2 border rounded md:col-span-2"
            />
            <input
              type="file"
              accept="image/*"
              onChange={(e) => setImage(e.target.files[0])}
              required
              className="p-2 border rounded md:col-span-2"
            />

            <div className="md:col-span-2 pt-4">
              <button
                type="submit"
                disabled={loading}
                className={`w-full text-white py-2 rounded-md transition ${loading ? 'bg-blue-400 cursor-not-allowed' : 'bg-blue-600 hover:bg-blue-700'}`}
              >
                {loading ? "Registering..." : "Register"}
              </button>
            </div>

            <div className="md:col-span-2 flex justify-between text-sm text-blue-600 mt-2">
              <a href="/login" className="hover:underline">Already registered? Login</a>
            </div>
          </form>
        </div>

        {loading && (
          <div className="fixed top-1/2 left-1/2 z-50 transform -translate-x-1/2 -translate-y-1/2 opacity-125">
            <div className="w-64 h-64 bg-white/80 backdrop-blur-md rounded-xl shadow-xl flex flex-col items-center justify-center space-y-4">
              <svg
                className="animate-spin h-10 w-10 text-blue-600"
                xmlns="http://www.w3.org/2000/svg"
                fill="none"
                viewBox="0 0 24 24"
              >
                <circle
                  className="opacity-25"
                  cx="12"
                  cy="12"
                  r="10"
                  stroke="currentColor"
                  strokeWidth="4"
                ></circle>
                <path
                  className="opacity-75"
                  fill="currentColor"
                  d="M4 12a8 8 0 018-8v8H4z"
                ></path>
              </svg>
              <p className="text-blue-700 font-semibold text-center">Registering doctor... Please Wait</p>
            </div>
          </div>
        )}
        {loading ? "Register....." : "Register"}
      </div>
    </>
  );
}
