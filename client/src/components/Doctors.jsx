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

      <div className="bg-blue-100 p-6 sm:p-10 md:px-20 lg:px-36 grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6">
        {doctors.map((doctor) => (
          <div
            key={doctor.doctorId}
            className="bg-white rounded-2xl shadow-lg overflow-hidden flex flex-col"
          >
            {/* Image with fixed height and full view */}
            <div className="h-[250px] bg-white flex items-center justify-center overflow-hidden">
              <img
                className="h-full w-full object-contain"
                src={doctor.profileImage}
                alt="doctor"
              />
            </div>

            {/* Info Section */}
            <div className="flex-grow p-4 flex flex-col justify-between">
              <div>
                <h1 className="text-xl font-bold text-gray-800">
                  {doctor.firstName + " " + doctor.lastName}
                </h1>
                <h3 className="text-gray-600 mt-1">
                  Speciality: {doctor.specialization}
                </h3>
                <h3 className="text-gray-600 mt-1">
                  Experience: {doctor.experienceYear} yrs
                </h3>
                <h3 className="text-gray-600 mt-1">
                  Availability: {doctor.availability.map((dayIndex) => days[dayIndex]).join(", ")}
                </h3>
              </div>

              {/* Action Buttons */}
              <div className="flex flex-col sm:flex-row gap-3 mt-4">
                <button
                  onClick={() => displaySingleDoctor(doctor)}
                  className="sm:flex-1 bg-blue-500 text-white py-2 px-4 rounded-lg hover:bg-blue-600 transition cursor-pointer"
                >
                  Detail
                </button>

                <a
                  href="https://wa.me/919329163682?text=Hello%2C%20I%20would%20like%20to%20know%20more"
                  target="_blank"
                  rel="noopener noreferrer"
                  className="sm:flex-1"
                >
                  <button className="w-full bg-green-500 text-white py-2 px-4 rounded-lg flex items-center justify-center hover:bg-green-600 transition cursor-pointer">
                    <FontAwesomeIcon icon={faWhatsapp} className="mr-2" />
                    WhatsApp
                  </button>
                </a>
              </div>
            </div>
          </div>
        ))}
      </div>



    </div>

  )
}

