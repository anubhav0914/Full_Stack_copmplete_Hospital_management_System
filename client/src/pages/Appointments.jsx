import React, { useState, useEffect } from 'react';
import Footer from '../components/Footer';
import Navbar from '../components/Navbar';
import api from '../api/axios';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../auth/AuthProvider';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { toast } from 'react-toastify';

export default function Appointments() {
  const { user } = useAuth();
  const navigate = useNavigate();

  const [doctors, setDoctors] = useState([]);
  const [doctorId, setDoctorId] = useState('');
  const [appointmentDate, setAppointmentDate] = useState('');
  const [appTime, setAppTime] = useState('');
  const [message, setMessage] = useState(null);

  <ToastContainer position="top-right" autoClose={3000} />

  useEffect(() => {
    api.get('/Doctor/allDoctors')
      .then(res => setDoctors(res.data.data))
      .catch(() => setDoctors([]));
  }, []);

  const timeSlots = Array.from({ length: 20 }, (_, i) => {
    const hour = 9 + Math.floor(i / 2);
    const minutes = i % 2 === 0 ? '00' : '30';
    return `${hour.toString().padStart(2, '0')}:${minutes}`;
  });

  const today = new Date().toISOString().split('T')[0];

  const handleSubmit = async (e) => {
    e.preventDefault();

    const payload = {
      doctorId: parseInt(doctorId),
      appointmentDate,
      appTime: appTime + ':00'
    };

    try {
      const res = await api.post('/Appointmentt/AddAppointment', payload);
      setMessage({ type: 'success', text: 'Appointment booked successfully!' });
      toast.success("Your Appointment has been booked successfully ");
      setMessage(null);
      setAppTime('');
      setAppointmentDate('');
      setDoctorId()
      
    } catch (err) {
      if (err.response) {

        console.log(err)
        if (err.response.data.statusCode === 409) {
          setMessage({ type: 'error', text: err.response.data.message });
        } else if (err.response.status === 400) {
          setMessage({ type: 'error', text:  err.response.data.message});
        } else {
          setMessage({ type: 'error', text: 'Something went wrong. Please try again.' });
        }
      }
    }
  };

  return (
    <>
      <Navbar />
      <div className='p-10'>
        <div className="max-w-2xl mx-auto p-6 bg-white shadow-2xl rounded-lg mt-20 mb-18">
          <button
            onClick={() => navigate(-1)}
            className="mb-8 text-2xl bg-gray-200 hover:bg-gray-300 text-gray-800 font-semibold  px-1 rounded shadow cursor-pointer"
          >
            ←
          </button>
          <h2 className="text-2xl font-bold mb-6 text-center text-blue-600">Book an Appointment</h2>

          {message && (
            <div

              className={`mb-4 text-center px-4 py-2 rounded-md ${message.type === 'success' ? 'bg-green-100 text-green-700' : 'bg-red-100 text-red-700'
                }`}
            >
              {message.text}
            </div>
          )}

          <form className="space-y-6" onSubmit={handleSubmit}>
            <div>
              <label className="block text-gray-700">Select Doctor</label>
              <select
                value={doctorId}
                onChange={(e) => setDoctorId(e.target.value)}
                className="w-full px-4 py-2 border rounded-md"
                required
              >
                <option value="">-- Select Doctor --</option>
                {doctors.map((doctor) => (
                  <option key={doctor.doctorId} value={doctor.doctorId}>
                    {doctor.firstName} {doctor.lastName}
                  </option>
                ))}
              </select>
            </div>

            <div>
              <label className="block text-gray-700">Select Date</label>
              <input
                type="date"
                value={appointmentDate}
                onChange={(e) => setAppointmentDate(e.target.value)}
                className="w-full px-4 py-2 border rounded-md"
                min={today}
                required
              />
            </div>

            <div>
              <label className="block text-gray-700">Select Time</label>
              <select
                value={appTime}
                onChange={(e) => setAppTime(e.target.value)}
                className="w-full px-4 py-2 border rounded-md"
                required
              >
                <option value="">-- Select Time --</option>
                {timeSlots.map((slot, index) => (
                  <option key={index} value={slot}>{slot}</option>
                ))}
              </select>
            </div>

            <div className="text-center mt-4">
              <button
                type="submit"
                className="bg-blue-600 text-white px-6 py-2 rounded-md hover:bg-blue-700 transition cursor-pointer"
              >
                Submit Appointment
              </button>
            </div>
          </form>
        </div>
      </div>
    </>
  );
}
