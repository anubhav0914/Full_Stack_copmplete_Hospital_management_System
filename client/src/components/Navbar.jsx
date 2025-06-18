import React, { useEffect, useState } from 'react';
import { Link, UNSAFE_getPatchRoutesOnNavigationFunction } from 'react-router-dom';
import { useAuth } from '../auth/AuthProvider';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { FaSignOutAlt } from 'react-icons/fa';
import api from '../api/axios';

function Navbar() {
  const { user, logout } = useAuth();
  const [menuOpen, setMenuOpen] = useState(false);
  const [profile,setProfile] = useState(null);

  useEffect(() => {
    
    const getdata = async()=>{

      try {
        if (user) {
          if(user.role =="patient"){
            await api.get(`/Patient/getById/${user.id}`)
            .then(res => setProfile(res.data.data))
            .catch(err => console.error('Profile fetch failed', err));
          }
          if(user.role =="doctor"){
            await api.get(`/Doctor/GetById/${user.id}`)
            .then(res => setProfile(res.data.data))
            .catch(err => console.error('Profile fetch failed', err));
          }
          if(user.role =="employee"){
            await api.get(`/Employee/GetById/${user.id}`)
            .then(res => setProfile(res.data.data))
            .catch(err => console.error('Profile fetch failed', err));
          }
        }
      } catch (error) {
          console.log(err.message)
      }
    }
    getdata();
  },[]);

  const handleLogout = () => {
    toast.info(
      <div >
        <p>Are you sure you want to logout?</p>
        <div className="flex justify-end  gap-2 mt-4">
          <button
            onClick={() => {
              toast.dismiss();
              performLogout();
            }}
            className="bg-red-500 text-white px-3 py-1 rounded"
          >
            Yes
          </button>
          <button
            onClick={() => toast.dismiss()}
            className="bg-gray-300 px-3 py-1 rounded"
          >
            Cancel
          </button>
        </div>
      </div>,
      {
        autoClose: false,
        closeOnClick: false,
        draggable: false,
        position: "top-center",
      }
    );
  };
  

  const performLogout = () => {
    logout(); // calls context logout function
  };

  return (
    <nav className="bg-white shadow-md w-full fixed top-0 z-50">
      <div className="max-w-screen-xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="flex justify-between items-center h-20">
          {/* Logo */}
          <div className="flex items-center gap-2">
            <img
              src="https://static.vecteezy.com/system/resources/previews/017/177/954/large_2x/round-medical-cross-symbol-on-transparent-background-free-png.png"
              alt="logo"
              className="w-10 h-10"
            />
            <span className="text-2xl font-bold text-blue-600">MediCare</span>
          </div>

          {/* Desktop Nav Links */}
          <div className="hidden md:flex items-center gap-8">
            <Link to="/" className="text-gray-700 hover:text-blue-600 transition">Home</Link>
            <Link to="/about" className="text-gray-700 hover:text-blue-600 transition">About</Link>
            <Link to="/contact" className="text-gray-700 hover:text-blue-600 transition">Contact</Link>
            <Link to="/appointments" className="text-gray-700 hover:text-blue-600 transition">Appointment</Link>
            <Link to="/all-doctors" className="text-gray-700 hover:text-blue-600 transition">Doctors</Link>
          </div>

          {/* Right Section: Profile or Sign In */}
          <div className="hidden md:flex items-center gap-4">
            {user ? (
              <>

                <div className="flex flex-row items-center  gap-3 justify-between w-full mt-2">
                  {/* Profile Link */}
                  <Link
                    to={`/${user.role}`}
                    className="text-white text-sm font-medium text-center  rounded-full shadow bg-gradient-to-b from-blue-400 to-blue-600 hover:from-blue-500 hover:to-blue-700 border border-white"
                  >
                    <img
                      src={profile?.profileImage || "./public/WhatsApp Image 2024-09-21 at 11.21.17.jpeg"}
                      alt="Profile"
                      className="w-10 h-10 rounded-full border-2 border-blue-500 object-cover"
                    />
                  </Link>

                  {/* Logout Button */}
                  <button
                    className="bg-blue-600 text-white px-4 py-2 rounded flex items-center gap-2 cursor-pointer"
                    onClick={handleLogout}
                  >
                    <FaSignOutAlt /> Logout
                  </button>
                </div>

              </>
            ) : (
              <Link
                to="/register"
                className="bg-blue-600 text-white px-4 py-2 rounded-md hover:bg-blue-700 transition"
              >
                Sign In / Sign Up
              </Link>
            )}
          </div>

          {/* Mobile Menu Button */}
          <div className="md:hidden">
            <button
              onClick={() => setMenuOpen(!menuOpen)}
              className="text-gray-700 hover:text-blue-600 focus:outline-none"
            >
              <svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                {menuOpen ? (
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M6 18L18 6M6 6l12 12" />
                ) : (
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M4 6h16M4 12h16M4 18h16" />
                )}
              </svg>
            </button>
          </div>
        </div>
      </div>

      {/* Mobile Dropdown */}
      {menuOpen && (
        <div className="md:hidden bg-white shadow-lg px-4 py-4 space-y-3">
          <Link to="/" className="block text-gray-700 hover:text-blue-600">Home</Link>
          <Link to="/about" className="block text-gray-700 hover:text-blue-600">About</Link>
          <Link to="/contact" className="block text-gray-700 hover:text-blue-600">Contact</Link>
          <Link to="/appointments" className="block text-gray-700 hover:text-blue-600">Appointment</Link>
          <Link to="/all-doctors" className="block text-gray-700 hover:text-blue-600">Doctors</Link>

          {user ? (
            <div className="pt-2 border-t border-gray-200">
              <div className="flex items-center gap-3 mb-2">
                <img
                  src={profile.profileImage || "/placeholder-profile.png"}
                  alt="Profile"
                  className="w-10 h-10 rounded-full border"
                />
                <div>
                  <div className="font-medium text-gray-800">{user.name}</div>
                  <div className="text-sm text-blue-600">{user.role}</div>
                </div>
              </div>
              <div className="flex flex-col gap-3 w-full max-w-xs mx-auto mt-10">
                <Link
                  to={`/${user.role}`}
                  className="text-white text-xl font-semibold text-center py-3 rounded-full shadow-md bg-gradient-to-b from-blue-400 to-blue-600 hover:from-blue-500 hover:to-blue-700 border-t-2 border-white"
                >
                  Profile
                </Link>
                <button
                  onClick={handleLogout}
                  className="text-white text-xl font-semibold text-center py-3 rounded-full shadow-md bg-gradient-to-b from-blue-400 to-blue-600 hover:from-blue-500 hover:to-blue-700 border-t-2 border-white"
                >
                  Logout
                </button>
              </div>
            </div>
          ) : (
            <Link
              to="/register"
              className="block bg-blue-600 text-white text-center py-2 rounded-md hover:bg-blue-700 transition"
            >
              Sign In / Sign Up
            </Link>
          )}
        </div>
      )}

      {/* Toast Container */}
      <ToastContainer />
    </nav>
  );
}

export default Navbar;
