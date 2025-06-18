import React, { useState, useEffect } from 'react';
import Navbar from '../components/Navbar';
import Footer from '../components/Footer';
import { useNavigate } from 'react-router-dom';
import api from '../api/axios';
import { useAuth } from '../auth/AuthProvider';
import { toast, ToastContainer } from 'react-toastify';

export default function EmployeeRegisterForm() {
    const navigate = useNavigate();
    const { user } = useAuth();
    const [departments, setDepartments] = useState([]);
    const [loading, setLoading] = useState(false);
    const [image, setImage] = useState(null);

    useEffect(() => {
        const fetchDepartments = async () => {
            try {
                const res = await api.get('/Department/allDepartment');
                setDepartments(res.data.data);
            } catch (err) {
                console.error('Failed to fetch departments:', err);
                alert('Failed to load departments.');
            }
        };
        fetchDepartments();
    }, [user?.id]);

    const [formData, setFormData] = useState({
        firstName: '',
        lastName: '',
        gender: '',
        phoneNumber: '',
        email: '',
        role: '',
        salary: '',
        joiningDate: '',
        departmentId: '',
        password: ''
    });

    const handleChange = (e) => {
        const { name, value, type } = e.target;
        setFormData(prev => ({
            ...prev,
            [name]: type === 'number' ? Number(value) : value
        }));
    };

    const handleRegister = async (e) => {
        e.preventDefault();

        if (!image) {
            toast.error("Please upload an image.");
            return;
        }

        const payload = new FormData();
        payload.append('firstName', formData.firstName);
        payload.append('lastName', formData.lastName);
        payload.append('gender', formData.gender);
        payload.append('phoneNumber', formData.phoneNumber);
        payload.append('email', formData.email);
        payload.append('role', formData.role);
        payload.append('salary', formData.salary);
        payload.append('joiningDate', formData.joiningDate);
        payload.append('departmentId', formData.departmentId);
        payload.append('password', formData.password);
        payload.append('image', image);

        setLoading(true);
        try {
            await api.post('/Employee/register', payload, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            });
            toast.success('Employee registration successful!');
            setFormData({
                firstName: '',
                lastName: '',
                gender: '',
                phoneNumber: '',
                email: '',
                role: '',
                salary: '',
                joiningDate: '',
                departmentId: '',
                password: ''
            });
            setImage(null);
        } catch (err) {
            console.error('Registration failed:', err);
            setImage(null);
            toast.error('Registration failed. Please try again.');
        } finally {
            setLoading(false);
        }
    };

    return (
        <>
            <div className='p-8 md:p-20 mt-20'>
                <ToastContainer autoClose={2000}></ToastContainer>
                <div className="max-w-4xl mx-auto bg-white shadow-lg rounded-lg p-10 relative">

                    <button
                        onClick={() => navigate(-1)}
                        className="mb-6 text-[16px] bg-gray-200 hover:bg-gray-300 text-gray-800 px-4 py-1 rounded shadow cursor-pointer"
                    >
                        ← Back
                    </button>
                    <h2 className="text-3xl font-bold mb-8 text-center text-blue-700">
                        Employee Registration
                    </h2>

                    <form className="grid grid-cols-1 md:grid-cols-2 gap-6" onSubmit={handleRegister}>
                        <input type="text"
                            name="firstName"
                            placeholder="First Name"
                            value={formData.firstName}
                            onChange={handleChange}
                            required
                            className="p-2 border rounded" />

                        <input type="text"
                            name="lastName"
                            placeholder="Last Name"
                            value={formData.lastName}
                            onChange={handleChange}
                            required
                            className="p-2 border rounded" />

                        <select name="gender" value={formData.gender} onChange={handleChange} required className="p-2 border rounded">
                            <option value="">Select Gender</option>
                            <option value="Male">Male</option>
                            <option value="Female">Female</option>
                            <option value="Other">Other</option>
                        </select>
                        <input type="tel"
                            name="phoneNumber"
                            placeholder="Phone Number"
                            value={formData.phoneNumber}
                            onChange={handleChange}
                            required
                            className="p-2 border rounded" />

                        <input type="email"
                            name="email"
                            placeholder="Email"
                            value={formData.email}
                            onChange={handleChange}
                            required
                            className="p-2 border rounded" />

                        <input type="text"
                            name="role"
                            placeholder="Job Role"
                            value={formData.role}
                            onChange={handleChange}
                            required className="p-2 border rounded" />

                        <input type="number"
                            name="salary"
                            placeholder="Salary"
                            value={formData.salary}
                            onChange={handleChange}
                            required
                            className="p-2 border rounded" />

                        <input type="date"
                            name="joiningDate"
                            value={formData.joiningDate}
                            onChange={handleChange}
                            required
                            className="p-2 border rounded" />

                        {departments && (
                            <select name="departmentId"
                                value={formData.departmentId}
                                onChange={handleChange}
                                required
                                className="p-2 border rounded">
                                <option value="">Select Department</option>
                                {departments.map((dept) => (
                                    <option key={dept.departmentId} value={dept.departmentId}>
                                        {dept.departmentName}
                                    </option>
                                ))}
                            </select>
                        )}
                        <input type="password"
                            name="password"
                            placeholder="Password"
                            value={formData.password}
                            onChange={handleChange}
                            required
                            className="p-2 border rounded" />

                        <input
                            type="file"
                            accept="image/*"
                            onChange={(e) => setImage(e.target.files[0])}
                            required
                            className="md:col-span-2 p-2 border rounded"
                        />

                        <div className="md:col-span-2 pt-4">
                            <button
                                type="submit"
                                disabled={loading}
                                className={`w-full text-white py-2 rounded-md transition ${loading ? 'bg-blue-400 cursor-not-allowed' : 'bg-blue-600 hover:bg-blue-700'}`}
                            >
                                {loading ? "Registering..." : "Register as Employee"}
                            </button>
                        </div>

                     
                    </form>

                    {loading && (
                        <div className="fixed top-1/2 left-1/2 z-50 transform -translate-x-1/2 -translate-y-1/2 opacity-125">
                            <div className="w-64 h-64 bg-white/80 backdrop-blur-md rounded-xl shadow-xl flex flex-col items-center justify-center space-y-4">
                                <svg
                                    className="animate-spin h-10 w-10 text-blue-600"
                                    xmlns="http://www.w3.org/2000/svg"
                                    fill="none"
                                    viewBox="0 0 24 24"
                                >
                                    <circle
                                        className="opacity-25"
                                        cx="12"
                                        cy="12"
                                        r="10"
                                        stroke="currentColor"
                                        strokeWidth="4"
                                    ></circle>
                                    <path
                                        className="opacity-75"
                                        fill="currentColor"
                                        d="M4 12a8 8 0 018-8v8H4z"
                                    ></path>
                                </svg>
                                <p className="text-blue-700 font-semibold text-center">Registering employee... Please Wait</p>
                            </div>
                        </div>
                    )}
                </div>
            </div>
        </>
    );
}
