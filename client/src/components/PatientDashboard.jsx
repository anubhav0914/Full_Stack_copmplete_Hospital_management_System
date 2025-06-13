import { useEffect, useState } from 'react';
import api from '../api/axios';
import axios from 'axios';
import { useAuth } from '../auth/AuthProvider';

const PatientDashboard = () => {
  const { user } = useAuth();
  const [profile, setProfile] = useState(null);
  const [appointnets, setAppointment] = useState(null);
  const [messge, setMessage] = useState("");
  const [doctors,setDoctors] = useState();



  useEffect(() => {
    if (user?.email) {

      api.get(`/Patient/getByEmail/${user.email}`)
        .then(res => setProfile(res.data.data))
        .catch(err => console.error('Profile fetch failed', err));
    }
  }, [user?.email]);

   useEffect(() => {
    api.get('/Doctor/allDoctors')
      .then(response => {
        setDoctors(response.data.data);
      })
      .catch(error => {
        console.error('Error fetching doctors:', error);
      });
  }, []);

  const handleAppointments = () => {
    api.get(`/Appointmentt/GetAppointmentByPatientID/${profile.patientId}`)
      .then(res => {
        setAppointment(res.data.data)})
      .catch(err => {
        setMessage(err.data.message)
      })
  }
  
  const getDoctorName = (id) => {
  const doc = doctors.find(d => d.doctorId === id);
  return doc ? `${doc.firstName} ${doc.lastName}` : 'Unknown Doctor';
  };
  const getDepartmentName = (id) => {
  const doc = doctors.find(d => d.doctorId === id);
  return doc ? `${doc.specialization}` : 'Unknown Doctor';
  };


  if (!profile) return <div className="text-center p-6">Loading...</div>;

  return (
   <div className="bg-green-100 p-6 text-base font-sans mt-10">
  {/* Title */}
  <div className="bg-green-800 text-white text-center p-4 font-bold text-2xl rounded-md shadow">
    Your Record's
  </div>

  {/* Profile Card */}
  <div className="bg-white shadow-xl mt-10 p-8 border-2 border-green-800 max-w-6xl mx-auto grid grid-cols-1 md:grid-cols-3 gap-8 text-lg">
    {/* Left details */}
    <div className="md:col-span-2 grid gap-3">
      <div><strong>Name:</strong> {profile.firstName} {profile.lastName}</div>
      <div><strong>Email:</strong> {profile.email}</div>
      <div><strong>Phone Number:</strong> {profile.phoneNumber}</div>
      <div><strong>Gender:</strong> {profile.gender}</div>
      <div><strong>Blood Group:</strong> {profile.bloodGroup}</div>
      <div><strong>Patient ID:</strong> {profile.patientId}</div>
      <div><strong>Admission Date:</strong> {new Date(profile.admissionDate).toLocaleDateString()}</div>
      <div><strong>Address:</strong> {profile.addressLine1}, {profile.addressLine2}</div>
    </div>

    {/* Right image */}
    <div className="flex justify-center items-start">
      <img
        src="/placeholder-profile.png"
        alt="Patient"
        className="border-4 border-green-700 rounded-lg w-64 h-64 object-cover"
      />
    </div>
  </div>

  {/* Admission Date */}
  <div className="bg-white border-2 border-green-800 max-w-6xl mx-auto mt-6 p-6 text-lg">
    <div><strong>Admitted on:</strong> {new Date(profile.createdDate).toLocaleString()}</div>
  </div>

  {/* Button */}
  <div className="text-center mt-8">
    <button
      className="px-6 py-4 bg-green-600 hover:bg-green-800 text-white font-bold text-lg rounded shadow"
      onClick={handleAppointments}
    >
      Get all appointments
    </button>
  </div>

  {/* Appointments Table */}
  {appointnets?.length > 0 && (
    <div className="bg-white border-2 border-green-800 max-w-6xl mx-auto mt-8 p-6">
      <h2 className="text-3xl font-bold mb-6 text-center text-green-700">Patient Appointments</h2>
      <div className="overflow-x-auto">
        <table className="min-w-full border border-gray-300 text-lg rounded-lg">
          <thead className="bg-green-200 text-green-800">
            <tr>
              <th className="text-left py-4 px-6 border-b">Doctor</th>
              <th className="text-left py-4 px-6 border-b">Date</th>
              <th className="text-left py-4 px-6 border-b">Time</th>
              <th className="text-left py-4 px-6 border-b">Department</th>
            </tr>
          </thead>
          <tbody>
            {appointnets.map((appt, index) => (
              <tr key={index} className={index % 2 === 0 ? 'bg-white' : 'bg-gray-100'}>
                <td className="py-4 px-6 border-b">{getDoctorName(appt.doctorId)}</td>
                <td className="py-4 px-6 border-b">{appt.appointmentDate.split('T')[0]}</td>
                <td className="py-4 px-6 border-b">{appt.appTime}</td>
                <td className="py-4 px-6 border-b">{getDepartmentName(appt.doctorId)}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  )}

  <div className="bg-green-800 h-1 mt-10" />
</div>

  );
};

export default PatientDashboard;
