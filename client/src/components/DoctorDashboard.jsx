import React, { useState, useEffect } from 'react';
import { useAuth } from '../auth/AuthProvider';
import api from '../api/axios';

const DoctorDashboard = () => {
    const [profile, setProfile] = useState(null);
    const [department, setDepartment] = useState(null);
    const [showDept, setShowdept] = useState(false)
    const [appointnets, setAppointment] = useState(null);
    const [patient, setPatient] = useState();
    const [message, setMessage] = useState();



    const { user } = useAuth();

    const daysMap = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];

    useEffect(() => {
        if (user?.id) {
            api.get(`Doctor/GetById/${user.id}`)
                .then(res => {
                    setProfile(res.data.data);
                })
                .catch(err => console.error('Profile fetch failed', err));
        }
    }, [user?.id]);

    useEffect(() => {
        if (profile?.departmentId) {
            api.get(`Department/GetById/${profile.departmentId}`)
                .then(res => {
                    setDepartment(res.data.data);
                })
                .catch(err => console.error('Department fetch failed', err));
        }
    }, [profile]);

    useEffect(() => {
        api.get('/Patient/allPatients')
            .then(response => {
                setPatient(response.data.data);
            })
            .catch(error => {
                console.error('Error fetching patients:', error);
            });
    }, [user?.id]);

    const getPatientName = (id) => {
        const pat = patient.find(d => d.patientId === id);
        return pat ? `${pat.firstName} ${pat.lastName}` : 'Unknown Doctor';
    };


    const handleAppointments = async () => {
        if (user?.id) {
            try {
                const res = await api.get(`/Appointmentt/GetAppointmentByDoctorID/${6}`);
                if (res.data.data && res.data.data.length > 0) {
                    setAppointment(res.data.data);
                    setMessage(null);
                } else {
                    setAppointment([]);
                    setMessage("No appointments found.");
                }
            } catch (err) {
                setAppointment([]);
                setMessage("No appointments found.");
            }
        }
    };

    if (!profile) {
        return <div className="text-center mt-12 text-green-700 text-xl font-semibold">Loading profile...</div>;
    }
    if (!department) {
        return <div className="text-center mt-12 text-green-700 text-xl font-semibold">Loading profile...</div>;
    }

    return (
        <div className="max-w-5xl mx-auto  bg-white rounded-2xl shadow-2xl overflow-hidden mt-30 mb-20 border border-gray-200">

            <div className="bg-green-600 h-44 relative">
                <div className="absolute -bottom-16 left-8 w-36 h-36 border-4 border-white rounded-full overflow-hidden shadow-lg">
                    <img
                        src="/public/WhatsApp Image 2024-09-21 at 11.21.17.jpeg"
                        alt="Doctor"
                        className="w-full h-full object-cover"
                    />
                </div>
            </div>

            {/* Main Content */}
            <div className="pt-20 px-10 pb-10">
                {/* Name & Specialization */}
                <div className="flex flex-col sm:flex-row sm:justify-between sm:items-center">
                    <div>
                        <h2 className="text-3xl font-bold text-gray-900">{`Dr. ${profile.firstName} ${profile.lastName}`}</h2>
                        <p className="text-green-700 text-xl mt-1">{profile.specialization}</p>
                        <p className="text-lg text-gray-600 mt-1">{department.departmentName}</p>
                    </div>
                    <div className="text-lg text-right mt-6 sm:mt-0">
                        <p className="text-green-700 font-semibold">📞 {profile.phoneNumber}</p>
                        <p className="text-gray-600">{profile.email}</p>
                    </div>
                </div>

                {/* Additional Info */}
                <div className="grid grid-cols-1 sm:grid-cols-2 gap-6 mt-8 text-lg text-gray-800">
                    <p><span className="font-semibold">Qualifications:</span> {profile.qualification || "MBBS"}</p>
                    <p><span className="font-semibold">Experience:</span> {profile.experienceYear || "0"} Years</p>
                    <p><span className="font-semibold">Department:</span> {department.departmentName || "not applicable"}</p>

                    <p><span className="font-semibold">Joining Date:</span> {new Date(profile.joiningDate).toLocaleDateString()}</p>
                    <p><span className="font-semibold">Availability:</span> {
                        profile.availability?.length > 0
                            ? profile.availability.map(day => daysMap[day]).join(', ')
                            : 'Not available'
                    }</p>
                </div>

                {/* Buttons */}
                <div className="mt-8 flex flex-col sm:flex-row gap-4">
                    <button
                        onClick={handleAppointments}
                        className="bg-green-600 hover:bg-green-700 text-white px-6 py-3 rounded-lg text-lg font-medium"
                    >
                        Get All Your Appointments
                    </button>
                    <button
                        onClick={() => setShowdept(!showDept)}
                        className="bg-green-100 hover:bg-green-200 text-green-800 px-6 py-3 rounded-lg text-lg font-medium border border-green-600"
                    >
                        {showDept ? "Hide Department" : "Get Department Details"}
                    </button>
                </div>
                {showDept ? <div className="bg-white border-2 border-green-800 max-w-6xl mx-auto mt-6 p-6 text-lg">
                    <div><strong> Name:</strong> {department.departmentName}</div>
                    <div><strong> Dept Id:</strong> {department.departmentId}</div>
                    <div><strong> Head:</strong> {department.departmentHead}</div>
                    <div><strong> Creation date:</strong> {department.creationDate.split("T")[0]}</div>
                    <div><strong> No. Employee:</strong> {department.noOfEmployees}</div>

                </div> : ""}
               

                {message && (
                    <div className="bg-red-100 border border-red-500 text-red-700 px-6 py-4 rounded mt-6">
                        {message}
                    </div>
                )}

                {appointnets?.length > 0 && (
                    <div className="bg-white border-2 border-green-800 max-w-6xl mx-auto mt-8 p-6">
                        <h2 className="text-3xl font-bold mb-6 text-center text-green-700">Patient Appointments</h2>
                        <div className="overflow-x-auto">
                            <table className="min-w-full border border-gray-300 text-lg rounded-lg">
                                <thead className="bg-green-200 text-green-800">
                                    <tr>
                                        <th className="text-left py-4 px-6 border-b">Patient Name</th>
                                        <th className="text-left py-4 px-6 border-b">Date</th>
                                        <th className="text-left py-4 px-6 border-b">Time</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {appointnets.map((appt, index) => (
                                        <tr key={index} className={index % 2 === 0 ? 'bg-white' : 'bg-gray-100'}>
                                            <td className="py-4 px-6 border-b">{getPatientName(appt.patientId)}</td>
                                            <td className="py-4 px-6 border-b">{appt.appointmentDate.split('T')[0]}</td>
                                            <td className="py-4 px-6 border-b">{appt.appTime}</td>
                                        </tr>
                                    ))}
                                </tbody>
                            </table>
                        </div>
                    </div>
                )}

            </div>
        </div>
    );
};

export default DoctorDashboard;
