import React from 'react'
import Navbar from '../components/Navbar'
import Footer from '../components/Footer'
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../auth/AuthProvider';
import api from '../api/axios';
import { jwtDecode } from 'jwt-decode';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { toast } from 'react-toastify';


const Login = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [role, setRole] = useState('patient');
  const { login } = useAuth();

  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const res = await api.post('/Login/login', { email, password, role });
      if(res.data.jwtToken){
        console.log(res.data)
        login(res.data.jwtToken);
        toast.success("Logged In successfully ");
        const decoded = jwtDecode(res.data.jwtToken)
        const userRole = decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
  
        switch (userRole) {
          case 'patient': navigate('/patient'); break;
          case 'doctor': navigate('/doctor'); break;
          case 'employee': navigate('/employee'); break;
          default: navigate('/');
        }
      }
      else {
        toast.error(res.data.message)
      }
    } catch (err) {
      console.log(err)
      if (err.response && err.response.data && err.response.data.message) {
        console.log(err)
        toast.warning(err.response.data.message);
      } else {
        alert(err); // fallback message
      }

    }
  };


  return (
    <>
      <Navbar />
        <ToastContainer position="top-right" autoClose={2000} />
      <div className='pt-30 mb-30'>

        <div className="max-w-md mx-auto  bg-white shadow-lg rounded-lg p-8">
          <h2 className="text-2xl font-bold mb-6 text-center text-blue-700"> Login</h2>

          <form className="space-y-6" onSubmit={handleSubmit}>
            {/* Email */}
            <div>
              <label className="block text-sm font-medium text-gray-700">Email</label>
              <input
                type="email"
                name="email"
                value={email}
                required
                onChange={e => setEmail(e.target.value)}
                className="mt-1 w-full border rounded-md p-2"
              />
            </div>

            {/* Password */}
            <div>
              <label className="block text-sm font-medium text-gray-700">Password</label>
              <input
                type="password"
                name="password"
                value={password}
                required
                onChange={e => setPassword(e.target.value)}
                className="mt-1 w-full border rounded-md p-2"
              />
            </div>

            <div>
              <label className="block text-sm font-medium text-gray-700">Login as</label>
              <select value={role} onChange={e => setRole(e.target.value)}>
                <option value="patient">Patient</option>
                <option value="doctor">Doctor</option>
                <option value="employee">Employee</option>
              </select>
            </div>

            {/* Submit Button */}
            <div>
              <button
                type="submit"
                className="w-full bg-blue-600 text-white py-2 rounded-md hover:bg-blue-700 transition"
              >
                Login
              </button>
            </div>

            {/* {} Register */}
            <div className="flex justify-between text-sm text-blue-600 mt-2">
              <a href="/register" className="hover:underline">New user? Register</a>
            </div>
          </form>
        </div>

      </div>
      <Footer />
    </>
  )
}

export default Login;