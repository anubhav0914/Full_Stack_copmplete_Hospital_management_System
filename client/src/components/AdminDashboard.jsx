import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../api/axios';
import Navbar from '../components/Navbar';
import Footer from '../components/Footer';
import { FaUserMd, FaUserTie, FaUser, FaSearch, FaFileInvoiceDollar, FaSignOutAlt, FaListAlt } from 'react-icons/fa';
import { useAuth } from '../auth/AuthProvider';

const AdminDashboard = () => {
    const navigate = useNavigate();
    const { user } = useAuth();
    const [searchType, setSearchType] = useState('doctor');
    const [searchId, setSearchId] = useState('');
    const [searchResult, setSearchResult] = useState(null);
    const [doctors, setDoctors] = useState(null);
    const [employees, setEmployees] = useState([]);
    const [appointment, setAppointment] = useState(null)
    const [patients, setPatients] = useState(null);
    const [admissions, setAdmissions] = useState(null);
    const [department, setDepartment] = useState(null)
    const [showDoctor, setshowDoctor] = useState(false)
    const [showPatient, setshowPatient] = useState(false)
    const [showEmployee, setshowEmployee] = useState(false)
    const [showAppointments, setshowAppointments] = useState(false)
    const [showAdmission, setshowAdmission] = useState(false)
    const [showdepartment, setshowDepartment] = useState(false)
    const [availableDays, setavailableDays] = useState([])



    const dayMap = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];




    useEffect(() => {
        const fetchAll = async (type) => {
            try {
                const patientsList = await api.get(`/Patient/allPatients`);
                const doctorsList = await api.get(`/Doctor/allDoctors`);
                const employeeList = await api.get(`/Employee/allEmployee`);
                const appointmnetsList = await api.get(`/Appointmentt/allAppointments`);
                const admissionList = await api.get(`/AdmissionDischarge/getAllDeatilOfAdmissionDischarge`);
                const departmentList = await api.get(`/Department/allDepartment`);

                setDoctors(doctorsList.data.data);
                setEmployees(employeeList.data.data);
                setPatients(patientsList.data.data);
                setAdmissions(admissionList.data.data);
                setAppointment(appointmnetsList.data.data)
                setDepartment(departmentList.data.data)


            } catch {
                alert(`Failed to fetch ${type}`);
            }
        };
        fetchAll();
    }, [user?.id])


    const handleSearch = () => {
        try {
            if (searchType === "patient") {
                const pat = patients.find(d => d.patientId === Number(searchId));
                setSearchResult(pat);
            }
            if (searchType === "doctor") {
                const doc = doctors.find(d => d.doctorId === Number(searchId));
                setavailableDays(doc.availability.map(day => dayMap[day]).join(', '));
                setSearchResult(doc);
            }
            if (searchType === "employee") {
                const emp = employees.find(d => d.empId === Number(searchId));
                setSearchResult(emp);
            }


        } catch {
            alert(`No ${searchType} found with ID ${searchId}`);
        }
    };

    return (
        <>
            <Navbar />
            <div className="max-w-7xl mx-auto mt-30 px-4 sm:px-6 lg:px-8">

                <div className="flex h-full bg-gray-100">
                    {/* Left Panel */}

                    <div className="w-1/4 bg-blue-500 text-white flex flex-col items-center py-10  border-4">
                        <FaUser className="text-6xl mb-4" />
                        <h1 className="text-xl font-bold mb-2 ">{user.email}</h1>
                        <p className="text-lg">Welcome to Admin Portal</p>
                    </div>

                    {/* Right Panel */}
                    <div className="w-3/4 p-10">


                        {/* Action Buttons Grid */}
                        <div className="grid grid-cols-2 gap-6">

                            <ActionButton icon={<FaUserMd size={40} />} label="Add Doctor" onClick={() => navigate('/doctor-register')} />
                            <ActionButton icon={<FaUserTie size={40} />} label="Add Employee" onClick={() => navigate('/employee-register')} />
                            <ActionButton icon={<FaUser size={40} />} label="Get All Patients" onClick={() => setshowPatient(!showPatient)} />
                            <ActionButton icon={<FaUserTie size={40} />} label="Get All Employees" onClick={() => setshowEmployee(!showEmployee)} />
                            <ActionButton icon={<FaUserMd size={40} />} label="Get All Doctors" onClick={() => setshowDoctor(!showDoctor)} />
                            <ActionButton icon={<FaListAlt size={40} />} label="Admission/Discharge" onClick={() => setshowAdmission(!showAdmission)} />
                            <ActionButton icon={<FaListAlt size={40} />} label="Appointments" onClick={() => setshowAppointments(!showAppointments)} />
                            <ActionButton icon={<FaListAlt size={40} />} label="Department" onClick={() => setshowDepartment(!showdepartment)} />

                            <ActionButton icon={<FaFileInvoiceDollar size={40} />} label="Billing Section" onClick={() => navigate('/bills')} />
                        </div>

                        {/* Search Box */}
                        <div className="mt-10">
                            <h2 className="text-lg font-semibold mb-2">Search Record by ID</h2>
                            <div className="flex gap-4">
                                <select value={searchType} onChange={e => setSearchType(e.target.value)} className="border px-3 py-2 rounded">
                                    <option value="doctor">Doctor</option>
                                    <option value="patient">Patient</option>
                                    <option value="employee">Employee</option>
                                </select>
                                <input
                                    className="border px-3 py-2 rounded w-1/2"
                                    placeholder="Enter ID"
                                    value={searchId}
                                    onChange={e => setSearchId(e.target.value)}
                                />
                                <button onClick={handleSearch} className="bg-green-600 text-white px-4 py-2 rounded flex items-center gap-2">
                                    <FaSearch /> Search
                                </button>
                            </div>
                            {searchType == "employee" && searchResult && (
                                    <div className="max-w-sm mt-8 mx-auto bg-white rounded-xl shadow-md overflow-hidden border p-6 mb-4">
                                        <div className="mb-4">
                                            <h2 className="text-xl font-semibold text-blue-700">{searchResult.firstName} {searchResult.lastName}</h2>
                                            <p className="text-sm text-gray-600">{searchResult.role}</p>
                                        </div>

                                        <div className="space-y-1 text-sm text-gray-700">
                                            <p><span className="font-semibold">ID:</span> {searchResult.empId}</p>
                                            <p><span className="font-semibold">Department ID:</span> {searchResult.departmentId}</p>
                                            <p><span className="font-semibold">Gender:</span> {searchResult.gender}</p>
                                            <p><span className="font-semibold">Phone:</span> {searchResult.phoneNumber}</p>
                                            <p><span className="font-semibold">Email:</span> {searchResult.email}</p>
                                            <p><span className="font-semibold">Salary:</span> ₹{searchResult.salary}</p>
                                            <p><span className="font-semibold">Joining Date:</span> {new Date(searchResult.joiningDate).toLocaleDateString()}</p>
                                        </div>
                                    </div>
                            )}
                            {searchType == "doctor" && searchResult && (<div className="max-w-sm mt-8 mx-auto bg-white rounded-xl shadow-md overflow-hidden border p-6 mb-4">
                                <div className="mb-4">
                                    <h2 className="text-xl font-semibold text-blue-700">{searchResult.firstName} {searchResult.lastName}</h2>
                                    <p className="text-sm text-gray-600">{searchResult.specialization}</p>
                                </div>

                                <div className="space-y-1 text-sm text-gray-700">
                                    <p><span className="font-semibold">Doctor ID:</span> {searchResult.doctorId}</p>
                                    <p><span className="font-semibold">Department ID:</span> {searchResult.departmentId}</p>
                                    <p><span className="font-semibold">Qualification:</span> {searchResult.qualification}</p>
                                    <p><span className="font-semibold">Experience:</span> {searchResult.experienceYear} years</p>
                                    <p><span className="font-semibold">Phone:</span> {searchResult.phoneNumber}</p>
                                    <p><span className="font-semibold">Email:</span> {searchResult.email}</p>
                                    <p><span className="font-semibold">Joining Date:</span> {new Date(searchResult.joiningDate).toLocaleDateString()}</p>
                                    <p><span className="font-semibold">Available on:</span> {availableDays}</p>
                                </div>
                            </div>
                            )}
                            {searchResult && searchType == "patient" && (
                                <div className="max-w-sm mx-auto bg-white border shadow-md rounded-xl mt-8 p-6 mb-6">
                                    <div className="mb-3">
                                        <h2 className="text-xl font-bold text-blue-700">
                                            {searchResult.firstName} {searchResult.lastName}
                                        </h2>
                                        <p className="text-sm text-gray-600">Patient ID: {searchResult.patientId}</p>
                                    </div>

                                    <div className="space-y-1 text-sm text-gray-700">
                                        <p><span className="font-semibold">Gender:</span> {searchResult.gender}</p>
                                        <p><span className="font-semibold">Phone:</span> {searchResult.phoneNumber}</p>
                                        <p><span className="font-semibold">Email:</span> {searchResult.email}</p>
                                        <p><span className="font-semibold">Blood Group:</span> {searchResult.bloodGroup}</p>
                                        <p><span className="font-semibold">Admission Date:</span> {new Date(searchResult.admissionDate).toLocaleDateString()}</p>
                                        <p><span className="font-semibold">Address:</span> {searchResult.addressLine1}, {searchResult.addressLine2}</p>
                                        <p><span className="font-semibold">Created:</span> {new Date(searchResult.createdDate).toLocaleDateString()}</p>
                                        <p><span className="font-semibold">Last Updated:</span> {new Date(searchResult.updatedDate).toLocaleDateString()}</p>
                                    </div>
                                </div>

                            )}
                        </div>

                    </div>
                </div>
            </div>
            {/* Table Displays */}
            <div className="bg-white border-2 mb-20 border-green-800 max-w-6xl mx-auto mt-6 p-6 text-lg">
                {showDoctor ? <TableDisplay title="Doctors" data={doctors} /> : ""}
                {showEmployee ? <TableDisplay title="Employees" data={employees} /> : ""}
                {showdepartment ? <TableDisplay title="Depatment" data={department} /> : ""}
                {showPatient ? <TableDisplay title="Patients" data={patients} /> : ""}
                {showAdmission ? <TableDisplay title="Admission/Discharge Records" data={admissions} /> : ""}
                {showAppointments ? <TableDisplay title="All Appointments Records" data={appointment} /> : ""}
            </div>
        </>
    );
};

// Reusable Action Button
const ActionButton = ({ icon, label, onClick }) => (
    <div
        onClick={onClick}
        className="bg-white shadow-lg hover:bg-green-50 transition cursor-pointer p-6 rounded flex flex-col items-center justify-center text-center"
    >
        <div className="text-green-600 mb-2">{icon}</div>
        <p className="font-medium">{label}</p>
    </div>
);

// Table Display
const TableDisplay = ({ title, data }) => {
    if (!data || data.length === 0) return null;
    const columns = Object.keys(data[0]);

    return (
        <div className="mt-10 max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
            <h2 className="text-xl font-semibold mb-3">{title}</h2>
            <div className="overflow-x-auto">
                <table className="w-full table-auto border-collapse bg-white shadow rounded">
                    <thead className="bg-blue-100">
                        <tr>
                            {columns.map((col, idx) => (
                                <th key={idx} className="px-4 py-2 text-left border">{col.charAt(0).toUpperCase() + col.slice(1)}</th>
                            ))}
                        </tr>
                    </thead>
                    <tbody>
                        {data.map((row, i) => (
                            <tr key={i} className={`border-t ${i % 2 === 0 ? "bg-green-100" : "bg-white"}`}>
                                {columns.map((col, j) => (
                                    <td key={j} className={`px-4 py-2 text-sm border `}>{col.includes("Date") ? row[col].split("T")[0] : row[col]}</td>
                                ))}
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
};

export default AdminDashboard;
