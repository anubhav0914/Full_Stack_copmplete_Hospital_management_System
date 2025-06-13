import React from 'react';
import { Mail, Phone, MapPin, Hospital } from 'lucide-react';
import Doctors from './Doctors';
import { Link } from 'react-router-dom';
export default function Footer() {
  return (
    <>
       
    <div className="bg-gray-900 text-white ">
      <div className="max-w-7.3xl mx-auto py-10 px-10 sm:px-6 lg:py-16 lg:px-8">
        <div className="grid grid-cols-1 md:grid-cols-4 gap-8">
          <div className='justify-items-center'>
            <div className="flex  items-center mr-10" >

              <span className="ml-2 text-xl font-bold">MedCare</span>
            </div>
            <p className="mt-4 text-gray-400 text-center pl-10">
              Providing quality healthcare services with compassion and excellence.
            </p>
          </div>

          <div className='pl-30'>
            <h3 className="text-sm font-semibold uppercase tracking-wider">Quick Links</h3>
            <ul className="mt-4 space-y-4">
              <li className='flex'>
                <Hospital className="h-5 w-5 text-blue-400 mr-2" />
                <Link to="/about" className="text-gray-400 hover:text-white">About Us</Link>
              </li>
              <li className='flex'>
                <Hospital className="h-5 w-5 text-blue-400 mr-2" />
                <Link to="/doctor" className="text-gray-400 hover:text-white">Our Doctors</Link>
              </li>
             
              <li className='flex'>
                <Hospital className="h-5 w-5 text-blue-400 mr-2" />
                <Link to="/appointments" className="text-gray-400 hover:text-white">Book Appointment</Link>
              </li>
            </ul>
          </div>
          <div className='pl-10'>
            <h3 className="text-sm font-semibold uppercase tracking-wider">For Professionals</h3>
            <ul className="mt-4 space-y-4">
              <li className='flex'>
                <Hospital className="h-5 w-5 text-blue-400 mr-2" />
                <Link to="/login" className="text-gray-400 hover:text-white"> Login</Link>
              </li>
              <li className='flex'>
                <Hospital className="h-5 w-5 text-blue-400 mr-2" />
                <Link to="/adminlogin" className="text-gray-400 hover:text-white">Admin Login</Link>
              </li>
            </ul>
          </div>

          <div>
            <h3 className="text-sm font-semibold uppercase tracking-wider">Contact Info</h3>
            <ul className="mt-4 space-y-4">
              <li className="flex items-center">
                <MapPin className="h-5 w-5 text-blue-400 mr-2" />
                <span className="text-gray-400">123 Healthcare Ave, Medical City</span>
              </li>
              <li className="flex items-center">
                <Phone className="h-5 w-5 text-blue-400 mr-2" />
                <span className="text-gray-400">+91 9161241901</span>
              </li>
              <li className="flex items-center">
                <Mail className="h-5 w-5 text-blue-400 mr-2" />
                <span className="text-gray-400">contact@medcare.com</span>
              </li>
            </ul>
          </div>
        </div>

        <div className="mt-8 border-t border-gray-700 pt-8">
          <p className="text-center text-gray-400">
            © {new Date().getFullYear()} MedCare. All rights reserved.
          </p>
        </div>
      </div>
    </div>
    </>

  );
}
