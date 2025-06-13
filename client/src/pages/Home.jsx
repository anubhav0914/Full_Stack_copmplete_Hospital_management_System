import React from 'react';
import { Link } from 'react-router-dom';
import Doctors from '../components/Doctors';
import { Slideshow } from "../components/Slidshow"
import AdminDashboard from '../components/AdminDashboard';

export function Home() {
  return (
    <>
      <Slideshow></Slideshow>
      {/* Hero Section */}
      <div className='bg-blue-100'>

      <div className="relative w-[1600px] ml-30 h-[350px] sm:h-[450px] lg:h-[550px]">
        {/* Background Image */}
        <img
          src="./public/hospital.png"
          alt="Hospital Background"
          className="absolute inset-0 w-full h-full object-cover"
        />

        {/* Overlay Box */}
        <div className="absolute left-0 ml-40 top-0 h-full  sm:w-[45%] bg-blue-600/40 text-white p-6 sm:p-10 flex flex-col justify-center z-10">
          <h1 className="text-xl sm:text-2xl md:text-4xl font-bold mb-4">Revolutionizing Care</h1>
          <p className="text-sm sm:text-base md:text-2xl mb-6">
            Mass General is consistently ranked as one of the top hospitals in the nation by <em>U.S. News & World Report</em>.
          </p>
          <div className="flex flex-col sm:flex-row items-start sm:items-center mt-8 gap-4 sm:gap-8 pl-0 sm:pl-6 md:pl-12">
            <Link to="/appointments" >
              <button className="appointment-btn border-2 px-6 py-3 bg-blue-500 border-blue-500 text-white font-bold text-xl sm:text-2xl rounded-2xl w-full sm:w-auto">
                Book Appointment
              </button>
            </Link>
            <Link to="/contact" >
              <button className="contact-btn border-2 px-6 py-3 bg-white border-blue-500 text-black font-bold text-xl sm:text-2xl rounded-2xl w-full sm:w-auto">
                Contact
              </button>
            </Link>
          </div>
        </div>
      </div>
      </div>

      {/* Doctors Section */}
      <Doctors />
    </>
  );
}
