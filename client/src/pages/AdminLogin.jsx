import { useState } from "react";
import Navbar from "../components/Navbar";
import Footer from "../components/Footer";
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../auth/AuthProvider';
import api from '../api/axios';
import { toast, ToastContainer } from "react-toastify";

const AdminLogin = () => {
  const [email, setEmail] = useState('');
  const [userName, setUserName] = useState('');
  const [password, setPassword] = useState('');
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const res = await api.post('/Login/Adminlogin', { userName, email, password });
      toast.success(res.data.message +" on your email")
      setTimeout(()=>{
        navigate('/otp-verificaton' , {state : {email}});
      },2000)
    } catch (err) {
      toast.error('Admin login failed');
    }
  };

  return (
    <>
    <ToastContainer  autoClose={2000}></ToastContainer>
      <div className='pt-30 mb-30 '>
        <div className="max-w-md mx-auto bg-white shadow-lg rounded-lg p-8">
          <h2 className="text-2xl font-bold mb-6 text-center text-blue-700">Admin Login</h2>

          <form className="space-y-6" onSubmit={handleSubmit}>
            {/* Email */}
            <div>
              <label className="block text-sm font-medium text-gray-700">Email</label>
              <input
                type="email"
                value={email}
                required
                onChange={(e) => setEmail(e.target.value)}
                className="mt-1 w-full border rounded-md p-2"
              />
            </div>

            {/* Username */}
            <div>
              <label className="block text-sm font-medium text-gray-700">Username</label>
              <input
                type="text"
                value={userName}
                required
                onChange={(e) => setUserName(e.target.value)}
                className="mt-1 w-full border rounded-md p-2"
              />
            </div>

            {/* Password */}
            <div>
              <label className="block text-sm font-medium text-gray-700">Password</label>
              <input
                type="password"
                value={password}
                required
                onChange={(e) => setPassword(e.target.value)}
                className="mt-1 w-full border rounded-md p-2"
              />
            </div>

            {/* Submit Button */}
            <div>
              <button
                type="submit"
                onClick={()=>setLoading(true)}
                className="w-full bg-blue-600 text-white py-2 rounded-md hover:bg-blue-700 transition"
              >
                Login as Admin
              </button>
            </div>
          </form>
        </div>
        {loading && (
        <div className="absolute bg-white bg-opacity-90 inset-0 flex items-center justify-center z-50">
          <div className="bg-white w-60 h-40 p-6 rounded-lg flex flex-col items-center justify-center shadow-lg">
            <div className="loader mb-4 border-t-4 border-blue-600 rounded-full w-10 h-10 animate-spin"></div>
            <p className="text-gray-700 font-semibold">Sending OPT in…</p>
            <p className="text-sm text-gray-500">Please wait</p>
          </div>
        </div>
      )}
      </div>
    </>
  );
};

export default AdminLogin;
