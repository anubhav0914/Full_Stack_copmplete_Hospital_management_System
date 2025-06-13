import React, { useState, useEffect } from 'react';
import Navbar from '../components/Navbar';
import Footer from '../components/Footer';
import { useNavigate } from 'react-router-dom';
import api from '../api/axios';
import { useAuth } from '../auth/AuthProvider';
import { toast } from 'react-toastify';

export default function EmployeeRegisterForm() {
    const navigate = useNavigate();
    const { user } = useAuth();
    const [departments, setDepartments] = useState([]);

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

        const payload = {
            firstName: formData.firstName,
            lastName: formData.lastName,
            gender: formData.gender,
            phoneNumber: formData.phoneNumber,
            email: formData.email,
            role: formData.role,
            salary: formData.salary,
            joiningDate: formData.joiningDate,
            departmentId: formData.departmentId ? Number(formData.departmentId) : null,
            password: formData.password
        };

        try {
            await api.post('/Employee/register', payload);
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
            })
        } catch (err) {
            console.error('Registration failed:', err);
            alert('Registration failed. Please try again.');
        }
    };

    return (
        <>
            <Navbar />
            <div className='p-8 md:p-20 mt-20'>
                <div className="max-w-4xl mx-auto bg-white shadow-lg rounded-lg p-10">

                    <button
                        onClick={() => navigate(-1)}
                        className="mb-6 text-[16px] bg-gray-200 hover:bg-gray-300 text-gray-800 px-4 py-1 rounded shadow cursor-pointer"
                    >
                        ← Back
                    </button>
                    <h2 className="text-3xl font-bold mb-8 text-center text-blue-700">
                        Employee Registration</h2>

                    <form className="grid grid-cols-1 md:grid-cols-2 gap-6" onSubmit={handleRegister}>
                        <input
                            type="text"
                            name="firstName"
                            placeholder="First Name"
                            value={formData.firstName}
                            onChange={handleChange}
                            required
                            className="p-2 border rounded"
                        />
                        <input
                            type="text"
                            name="lastName"
                            placeholder="Last Name"
                            value={formData.lastName}
                            onChange={handleChange}
                            required
                            className="p-2 border rounded"
                        />
                        <select
                            name="gender"
                            value={formData.gender}
                            onChange={handleChange}
                            required
                            className="p-2 border rounded"
                        >
                            <option value="">Select Gender</option>
                            <option value="Male">Male</option>
                            <option value="Female">Female</option>
                            <option value="Other">Other</option>
                        </select>
                        <input
                            type="tel"
                            name="phoneNumber"
                            placeholder="Phone Number"
                            value={formData.phoneNumber}
                            onChange={handleChange}
                            required
                            className="p-2 border rounded"
                        />
                        <input
                            type="email"
                            name="email"
                            placeholder="Email"
                            value={formData.email}
                            onChange={handleChange}
                            required
                            className="p-2 border rounded"
                        />
                        <input
                            type="text"
                            name="role"
                            placeholder="Job Role"
                            value={formData.role}
                            onChange={handleChange}
                            required
                            className="p-2 border rounded"
                        />
                        <input
                            type="number"
                            name="salary"
                            placeholder="Salary"
                            value={formData.salary}
                            onChange={handleChange}
                            required
                            className="p-2 border rounded"
                        />
                        <input
                            type="date"
                            name="joiningDate"
                            placeholder="Joining Date"
                            value={formData.joiningDate}
                            onChange={handleChange}
                            required
                            className="p-2 border rounded"
                        />
                        {departments && (
                            <select
                                name="departmentId"
                                value={formData.departmentId}
                                onChange={handleChange}
                                required
                                className="p-2 border rounded"
                            >
                                <option value="">Select Department</option>
                                {departments.map((dept) => (
                                    <option key={dept.departmentId} value={dept.departmentId}>
                                        {dept.departmentName}
                                    </option>
                                ))}
                            </select>
                        )}
                        <input
                            type="password"
                            name="password"
                            placeholder="Password"
                            value={formData.password}
                            onChange={handleChange}
                            required
                            className="p-2 border rounded"
                        />

                        <div className="md:col-span-2 pt-4">
                            <button
                                type="submit"
                                className="w-full bg-blue-600 text-white py-2 rounded-md hover:bg-blue-700 transition"
                            >
                                Register as Employee
                            </button>
                        </div>

                        <div className="md:col-span-2 flex justify-between text-sm text-blue-600 mt-2">
                            <a href="/login" className="hover:underline">Already registered? Login</a>
                        </div>
                    </form>
                </div>
            </div>
            <Footer />
        </>
    );
}
