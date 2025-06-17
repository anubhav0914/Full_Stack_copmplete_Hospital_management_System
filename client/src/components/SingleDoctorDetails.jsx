import React, { useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import { Link } from 'react-router-dom';
import Navbar from './Navbar';
import Footer from './Footer';

const days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];

export default function SingleDoctorDetails() {
  const location = useLocation();
  const navigate = useNavigate();
  const doctor = location.state?.doctor;

  if (!doctor) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        <div className="text-center space-y-4">
          <h1 className="text-3xl font-bold text-red-500">Doctor data not found</h1>
          <button
            onClick={() => navigate(-1)}
            className="bg-blue-600 text-white px-6 py-3 text-lg rounded"
          >
            Go Back
          </button>
        </div>
      </div>
    );
  }

  const fullName = `${doctor.firstName} ${doctor.lastName}`;
  const availableDays = doctor.availability.map((dayIndex) => days[dayIndex]).join(', ');

  return (
    <>
      <Navbar />

      <div className="min-h-screen bg-blue-100 flex justify-center items-center py-16 px-6">
        <div className="bg-white w-full max-w-7xl rounded-3xl shadow-2xl p-12 flex flex-col lg:flex-row items-center gap-12">

          {/* Left Side – Doctor Image and Info */}
          <div className="text-center lg:text-left">
                     <button
            onClick={() => navigate(-1)}
            className="mb-8 text-2xl bg-gray-200 hover:bg-gray-300 text-gray-800 font-semibold  px-1 rounded shadow cursor-pointer"
          >
            ←
          </button>
            <div className="w-64 h-64 mx-auto lg:mx-0 rounded-full overflow-hidden border-8 border-red-500 shadow-md">
              <img
                src={doctor.profileImage}
                alt="Doctor"
                className="w-full h-full object-cover"
              />
            </div>

            <div className="mt-6">
              <h2 className="text-4xl font-extrabold text-red-600 uppercase">{`Dr. ${fullName}`}</h2>
              <p className="text-xl text-gray-700 font-semibold uppercase mt-2">{doctor.specialization}</p>
              <p className="text-md mt-1 font-medium text-gray-500">National Hospital</p>
            </div>
          </div>

          {/* Right Side – Description and Details */}
          <div className="flex-1 space-y-6 text-left">
            <h3 className="text-3xl font-bold text-blue-700">
              Meet our new <span className="text-yellow-500 uppercase">{doctor.specialization}</span>
            </h3>

            <p className="text-lg text-gray-600">
              Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.
            </p>

            <div className="grid grid-cols-1 sm:grid-cols-2 gap-6 text-lg text-gray-800">
              <p><strong>Email:</strong> {doctor.email}</p>
              <p><strong>Phone:</strong> {doctor.phoneNumber}</p>
              <p><strong>Experience:</strong> {doctor.experienceYear} years</p>
              <p><strong>Qualification:</strong> {doctor.qualification}</p>
              <p><strong>Available Days:</strong> {availableDays}</p>
              <p><strong>Joining Date:</strong> {new Date(doctor.joiningDate).toDateString()}</p>
            </div>

            <div className="pt-6">
              <Link to="/appointments">
                <button className="bg-red-600 hover:bg-red-700 text-white px-8 py-4 text-xl font-bold rounded-xl shadow-md transition cursor-pointer">
                  Book Appointment
                </button>
              </Link>

            </div>
          </div>
        </div>
      </div>

    </>
  );
}
