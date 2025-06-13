import React, { useState } from 'react';
import Navbar from '../components/Navbar';
import Footer from '../components/Footer';
import { useNavigate } from 'react-router-dom';
import { useEffect } from 'react';
import api from '../api/axios';
import { useAuth } from '../auth/AuthProvider';
import { toast } from 'react-toastify';

export default function DoctorRegisterForm() {
    const navigate = useNavigate();
    const { user } = useAuth();
    const [departments, setDepartments] = useState(null);

    useEffect(() => {

        try {
            const response = api.get('/Department/allDepartment');
            response.then((res) => setDepartments(res.data.data))
            setDepartments(response.data);
        } catch (err) {
            console.error('Failed to fetch departments:', err);
            alert(err);
        }
    }, [user?.id]);

    const [formData, setFormData] = useState({
        firstName: '',
        lastName: '',
        specialization: '',
        phoneNumber: '',
        email: '',
        qualification: '',
        experienceYear: '',
        joiningDate: '',
        availability: [],
        departmentId: '',
        password: ''
    });

    const handleChange = (e) => {
        const { name, value, type } = e.target;
        if (type === 'number') {
            setFormData(prev => ({ ...prev, [name]: Number(value) }));
        } else {
            setFormData(prev => ({ ...prev, [name]: value }));
        }
    };

    const handleCheckboxChange = (dayIndex) => {
        setFormData(prev => {
            const alreadySelected = prev.availability.includes(dayIndex);
            const updatedAvailability = alreadySelected
                ? prev.availability.filter(d => d !== dayIndex)
                : [...prev.availability, dayIndex];
            return { ...prev, availability: updatedAvailability };
        });
    };

    const handleRegister = async (e) => {
        e.preventDefault();

        const payload = {
            firstName: formData.firstName,
            lastName: formData.lastName,
            specialization: formData.specialization,
            phoneNumber: formData.phoneNumber,
            email: formData.email,
            qualification: formData.qualification,
            experienceYear: formData.experienceYear,
            joiningDate: formData.joiningDate,
            availability: formData.availability,
            departmentId: formData.departmentId ? Number(formData.departmentId) : null,
            password: formData.password
        };

        try {
            await api.post('/Doctor/register', payload);
            toast.success('Doctor registration successful!');
            setFormData(
                {
                    firstName: '',
                    lastName: '',
                    specialization: '',
                    phoneNumber: '',
                    email: '',
                    qualification: '',
                    experienceYear: '',
                    joiningDate: '',
                    availability: [],
                    departmentId: '',
                    password: ''
                }
            )
        } catch (err) {
            console.error('Registration failed:', err);
            alert('Registration failed. Please try again.');
        }
    };

    const daysOfWeek = [
        { label: 'Sunday', value: 0 },
        { label: 'Monday', value: 1 },
        { label: 'Tuesday', value: 2 },
        { label: 'Wednesday', value: 3 },
        { label: 'Thursday', value: 4 },
        { label: 'Friday', value: 5 },
        { label: 'Saturday', value: 6 }
    ];

    return (
        <>
            <Navbar />
            <div className='p-8 md:p-20'>
                <div className="max-w-4xl mx-auto bg-white shadow-lg rounded-lg p-10">
                     <button
                        onClick={() => navigate(-1)}
                        className="mb-6 text-[16px] bg-gray-200 hover:bg-gray-300 text-gray-800 px-4 py-1 rounded shadow cursor-pointer"
                    >
                        ← Back
                    </button>
                    <h2 className="text-3xl font-bold mb-8 text-center text-blue-700">Doctor Registration</h2>

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
                        <input
                            type="text"
                            name="specialization"
                            placeholder="Specialization"
                            value={formData.specialization}
                            onChange={handleChange}
                            required
                            className="p-2 border rounded"
                        />
                        <input
                            type="text"
                            name="qualification"
                            placeholder="Qualification"
                            value={formData.qualification}
                            onChange={handleChange}
                            required
                            className="p-2 border rounded"
                        />
                        <input
                            type="number"
                            name="experienceYear"
                            placeholder="Years of Experience"
                            value={formData.experienceYear}
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
                        {departments ? <select
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
                            : ""}
                        <input
                            type="password"
                            name="password"
                            placeholder="Password"
                            value={formData.password}
                            onChange={handleChange}
                            required
                            className="p-2 border rounded"
                        />

                        <div className="md:col-span-2">
                            <label className="block font-semibold mb-2">Availability (Days):</label>
                            <div className="flex flex-wrap gap-4">
                                {daysOfWeek.map(day => (
                                    <label key={day.value} className="flex items-center space-x-2">
                                        <input
                                            type="checkbox"
                                            checked={formData.availability.includes(day.value)}
                                            onChange={() => handleCheckboxChange(day.value)}
                                        />
                                        <span>{day.label}</span>
                                    </label>
                                ))}
                            </div>
                        </div>

                        <div className="md:col-span-2 pt-4">
                            <button
                                type="submit"
                                className="w-full bg-blue-600 text-white py-2 rounded-md hover:bg-blue-700 transition"
                            >
                                Register as Doctor
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
