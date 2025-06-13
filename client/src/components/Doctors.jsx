import React from 'react'
import axios from "axios"
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faWhatsapp } from '@fortawesome/free-brands-svg-icons';
import { useState, useEffect } from 'react';
import SingleDoctorDetails from './SingleDoctorDetails';
import { Link } from 'react-router-dom';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../auth/AuthProvider';

// Inside your component

export default function Doctors() {

  const [doctors, setDoctors] = useState([]);
  const days = ['Sun', 'Mon', 'Tues', 'Wed', 'Thur', 'Fri', 'Sat'];


  useEffect(() => {
    // Replace this URL with your actual API endpoint
    axios.get('http://localhost:5297/api/Doctor/allDoctors')
      .then(response => {
        setDoctors(response.data.data);
      })
      .catch(error => {
        console.error('Error fetching doctors:', error);
      });
  }, []);
  const navigate = useNavigate();

  const displaySingleDoctor = (doctor) => {
    navigate('/single-doctor-details', { state: { doctor } });
  }


  return (
    <div className="w-full">
      {/* Title */}
      <div className="bg-blue-100 text-center font-bold pt-10 text-4xl">
        <h1>Our Doctors</h1>
      </div>

      {/* Card Grid */}
      <div className="bg-blue-100 p-7 h-auto sm:p-10 px-4 sm:px-10 md:px-20 lg:px-36 grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6">

        {
          doctors.map((doctor) => (
            <div
              key={doctor.doctorId}

              className="bg-amber-50 h-auto w-full rounded-2xl shadow-md p-4 sm:max-w-md md:max-w-lg lg:max-w-xl xl:max-w-2xl mx-auto"
            >
              <div className="h-[250px] rounded-2xl overflow-hidden">
                <img
                  className="w-full h-full object-cover"
                  src={doctor.image}
                  alt="doctor"
                />
              </div>

              <div className="pt-3 px-2 sm:px-4">
                <h1 className="text-xl sm:text-2xl font-bold">{doctor.firstName + " " + doctor.lastName}</h1>
                <h3 className="text-gray-500 text-base sm:text-lg">
                  Speciality: {doctor.specialization}
                </h3>
                <h3 className="text-gray-500 text-base sm:text-lg">
                  Experience : {doctor.experienceYear}
                </h3>
                <h3 className="text-gray-500 text-base sm:text-lg">
                  Availability : {doctor.availability.map((dayIndex) => days[dayIndex]).join(', ')}
                </h3>

              </div>

              <div className="flex flex-col sm:flex-row gap-3 mt-4 px-2 sm:px-0">
                <button
                  onClick={() => displaySingleDoctor(doctor)}
                  className="sm:flex-1 w-full bg-blue-500 border-2 border-blue-500 px-4 py-2 text-sm sm:text-base md:text-lg rounded-xl font-semibold text-white hover:bg-blue-600 transition text-center"
                >
                  Detail
                </button>

                <a
                  href="https://wa.me/919329163682?text=Hello%2C%20I%20would%20like%20to%20know%20more"
                  target="_blank"
                  rel="noopener noreferrer"
                >
                  <button
                    className="sm:flex-1 w-full bg-green-500 border-2 border-green-500 px-4 py-2 text-sm sm:text-base md:text-lg rounded-xl font-semibold text-white flex items-center justify-center hover:bg-green-600 transition text-center"
                  >
                    <FontAwesomeIcon icon={faWhatsapp} className="mr-2" />
                    WhatsApp
                  </button>
                </a>
              </div>


            </div>
          ))
        }

      </div>

    </div>

  )
}

