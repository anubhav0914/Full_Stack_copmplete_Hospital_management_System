import React, { useState } from 'react';
import Navbar from '../components/Navbar';
import Footer from '../components/Footer';
import { useNavigate } from 'react-router-dom';
import api from '../api/axios'; // centralized API instance

export default function RegisterForm() {
  const navigate = useNavigate();
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

    const payload = {
      firstName: formData.firstName,
      lastName: formData.lastName,
      gender: formData.gender,
      addressLine1: formData.addressLine1,
      addressLine2: formData.addressLine2,
      admissionDate: new Date().toISOString(), // auto-set
      phoneNumber: formData.phoneNumber,
      email: formData.email,
      bloodGroup: formData.bloodGroup,
      password: formData.password
    };

    try {
      await api.post('/Patient/register', payload);
      alert('Registration successful!');
      navigate('/login');
    } catch (err) {
      console.error('Registration failed:', err);
      alert('Registration failed. Please try again.');
    }
  };

  return (
    <>
      <Navbar />
      <div className='p-8 md:p-20'>
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

            <div className="md:col-span-2 pt-4">
              <button
                type="submit"
                className="w-full bg-blue-600 text-white py-2 rounded-md hover:bg-blue-700 transition"
              >
                Register
              </button>
            </div>

            <div className="md:col-span-2 flex justify-between text-sm text-blue-600 mt-2">
              <a href="/login" className="hover:underline">Already registered? Login</a>
            </div>
          </form>
        </div>
      </div>
      <Footer />
    </>
  );
}
