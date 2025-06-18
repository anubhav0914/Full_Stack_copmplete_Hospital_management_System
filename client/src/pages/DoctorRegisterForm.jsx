import React, { useState, useEffect } from 'react';
import Navbar from '../components/Navbar';
import Footer from '../components/Footer';
import { useNavigate } from 'react-router-dom';
import api from '../api/axios';
import { useAuth } from '../auth/AuthProvider';
import { toast, ToastContainer } from 'react-toastify';

export default function DoctorRegisterForm() {
    const navigate = useNavigate();
    const { user } = useAuth();
    const [departments, setDepartments] = useState(null);
    const [image, setImage] = useState(null);
    const [loading, setLoading] = useState(false);


    useEffect(() => {
        const fetchDepartments = async () => {
            try {
                const response = await api.get('/Department/allDepartment');
                setDepartments(response.data.data);
            } catch (err) {
                console.error('Failed to fetch departments:', err);
                alert(err);
            }
        };
        fetchDepartments();
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
    
    const isFormReady = [
        formData.firstName &&
        formData.lastName &&
        formData.specialization && 
        formData.phoneNumber &&  email,
        formData.qualification &&
        formData.experienceYear &&
        formData.joiningDate &&
        formData.availability &&
        formData.departmentId &&
        formData.password &&
        image
    ]
    const handleChange = (e) => {
        const { name, value, type } = e.target;
        setFormData(prev => ({
            ...prev,
            [name]: type === 'number' ? Number(value) : value
        }));
    };

    const handleCheckboxChange = (dayIndex) => {
        setFormData(prev => {
            const updatedAvailability = prev.availability.includes(dayIndex)
                ? prev.availability.filter(d => d !== dayIndex)
                : [...prev.availability, dayIndex];
            return { ...prev, availability: updatedAvailability };
        });
    };

    const handleImageChange = (e) => {
        if (e.target.files && e.target.files[0]) {
            setImage(e.target.files[0]);
        }
    };

    const handleRegister = async (e) => {
        e.preventDefault();

        const payload = new FormData();
        Object.entries(formData).forEach(([key, value]) => {
            if (key === 'availability') {
                value.forEach(day => payload.append('availability', day));
            } else {
                payload.append(key, value);
            }
        });

        // Image is required
        payload.append('Image', image);

        try {
            await api.post('/Doctor/register', payload, {
                headers: { 'Content-Type': 'multipart/form-data' }
            });
            setLoading(false)
            toast.success('Doctor registration successful!');
            setFormData({
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
            setImage(null);
        } catch (err) {
            setLoading(false)
            console.log(err)
            toast.error(err.response.data.message);
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
            <div className='p-8 md:p-20'>
                <ToastContainer position="top-right" autoClose={2000} />
                <div className="max-w-4xl mx-auto bg-white shadow-lg rounded-lg p-10">
                    <button
                        onClick={() => navigate(-1)}
                        className="mb-6 text-[16px] bg-gray-200 hover:bg-gray-300 text-gray-800 px-4 py-1 rounded shadow cursor-pointer"
                    >
                        ← Back
                    </button>
                    <h2 className="text-3xl font-bold mb-8 text-center text-blue-700">Doctor Registration</h2>

                    <form className="grid grid-cols-1 md:grid-cols-2 gap-6" onSubmit={handleRegister} encType="multipart/form-data">
                        <input type="text" name="firstName" placeholder="First Name" value={formData.firstName} onChange={handleChange} required className="p-2 border rounded" />
                        <input type="text" name="lastName" placeholder="Last Name" value={formData.lastName} onChange={handleChange} required className="p-2 border rounded" />
                        <input type="text" name="specialization" placeholder="Specialization" value={formData.specialization} onChange={handleChange} required className="p-2 border rounded" />
                        <input type="text" name="qualification" placeholder="Qualification" value={formData.qualification} onChange={handleChange} required className="p-2 border rounded" />
                        <input type="number" name="experienceYear" placeholder="Years of Experience" value={formData.experienceYear} onChange={handleChange} required className="p-2 border rounded" />
                        <input type="date" name="joiningDate" placeholder="Joining Date" value={formData.joiningDate} onChange={handleChange} required className="p-2 border rounded" />
                        <input type="tel" name="phoneNumber" placeholder="Phone Number" value={formData.phoneNumber} onChange={handleChange} required className="p-2 border rounded" />
                        <input type="email" name="email" placeholder="Email" value={formData.email} onChange={handleChange} required className="p-2 border rounded" />
                        <input
                            type="file"
                            name="Image"
                            accept="image/*"
                            onChange={handleImageChange}
                            required
                            className="p-2 border rounded"
                        />
                        <select name="departmentId" value={formData.departmentId} onChange={handleChange} required className="p-2 border rounded">
                            <option value="">Select Department</option>
                            {departments?.map(dept => (
                                <option key={dept.departmentId} value={dept.departmentId}>
                                    {dept.departmentName}
                                </option>
                            ))}
                        </select>
                        <input type="password" name="password" placeholder="Password" value={formData.password} onChange={handleChange} required className="p-2 border rounded" />

                        <div className="md:col-span-2">
                            <label className="block font-semibold mb-2">Availability (Days):</label>
                            <div className="flex flex-wrap gap-4">
                                {daysOfWeek.map(day => (
                                    <label key={day.value} className="flex items-center space-x-2">
                                        <input type="checkbox" checked={formData.availability.includes(day.value)} onChange={() => handleCheckboxChange(day.value)} />
                                        <span>{day.label}</span>
                                    </label>
                                ))}
                            </div>
                        </div>

                        <div className="md:col-span-2 pt-4">
                            <button type="submit"
                             disabled={!isFormReady}
                             className=" cursor-pointer w-full bg-blue-600 text-white py-2 rounded-md hover:bg-blue-700 transition" onClick={() => setLoading(true)}>
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
                                            <p className="text-blue-700 font-semibold text-center">Registering doctor... Please Wait</p>
                                        </div>
                                    </div>
                                )}
                                {loading ? "Register....." : "Register"}
                            </button>
                        </div>

                        <div className="md:col-span-2 flex justify-between text-sm text-blue-600 mt-2">
                            <a href="/login" className="hover:underline">Already registered? Login</a>
                        </div>
                    </form>
                </div>
            </div>
        </>
    );
}
