import React, { useState, useEffect } from 'react';
import Navbar from '../components/Navbar';
import Footer from '../components/Footer';
import api from '../api/axios';
import { toast, ToastContainer } from 'react-toastify';

export default function BillSection() {
    const [showForm, setShowForm] = useState(false);
    const [bills, setBills] = useState([]);
    const [doctors, setDoctors] = useState([]);
    const [patients, setPatients] = useState([]);
    const [appointments, setAppointments] = useState([]);

    const [formData, setFormData] = useState({
        patientId: 0,
        appointmentId: 0,
        totalAmount: 0,
        doctorId: 0
    });

    useEffect(() => {
        api.get('/Doctor/allDoctors').then(res => setDoctors(res.data.data || []));
        api.get('/Patient/allPatients').then(res => setPatients(res.data.data || []));
        api.get('/Appointmentt/allAppointments').then(res => setAppointments(res.data.data || []));
    }, []);

    const fetchBills = async () => {
        try {
            const res = await api.get('/BillingTransaction/allBills');
            setBills(res.data.data || []);
            setShowForm(false);
        } catch (error) {
            toast.error('Failed to fetch bills');
        }
    };
    const patientOptions = patients.map(p => ({
        value: p.patientId,
        label: `${p.firstName} ${p.lastName}`
    }));


    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData(prev => ({ ...prev, [name]: value }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        const payload = {
            patientId: Number(formData.patientId),
            appointmentId: Number(formData.appointmentId),
            totalAmount: Number(formData.totalAmount),
            doctorId: Number(formData.doctorId)
        };

        try {
            const res = await api.post('/BillingTransaction/AddNewBill', payload);
            toast.success('Bill created successfully!');
            setFormData({
                patientId: '',
                appointmentId: '',
                totalAmount: '',
                doctorId: ''
            });
        } catch (err) {
            console.log(err.response);
        }
    };

    return (
        <>
            <div className="p-10 mt-25">
                <ToastContainer ></ToastContainer>
                <div className="flex justify-center gap-6 mb-6">
                <button
                    onClick={() => navigate(-1)}
                    className="mb-8 text-xl bg-gray-200 hover:bg-gray-300 text-gray-800   px-1 rounded shadow cursor-pointer"
                >
                    ←Back
                </button>
                    <button
                        onClick={() => setShowForm(true)}
                        className="bg-blue-600 text-white px-6 py-2 rounded shadow hover:bg-blue-700"
                    >
                        Add New Bill
                    </button>
                    <button
                        onClick={fetchBills}
                        className="bg-green-600 text-white px-6 py-2 rounded shadow hover:bg-green-700"
                    >
                        Display All Bills
                    </button>
                </div>

                {showForm && (
                    <form
                        onSubmit={handleSubmit}
                        className="max-w-3xl mx-auto bg-white p-8 rounded-lg shadow-lg grid grid-cols-1 md:grid-cols-2 gap-6"
                    >
                        <select
                            name="patientId"
                            value={formData.patientId}
                            onChange={handleChange}
                            required

                            className="border p-2 rounded"
                        >
                            <option value="">Select Patient</option>
                            {patients.map(p => (
                                <option key={p.patientId} value={p.patientId}>
                                    {p.firstName} {p.lastName}
                                </option>
                            ))}
                        </select>

                        <select
                            name="doctorId"
                            value={formData.doctorId}
                            onChange={handleChange}
                            required
                            className="border p-2 rounded"
                        >
                            <option value="">Select Doctor</option>
                            {doctors.map(d => (
                                <option key={d.doctorId} value={d.doctorId}>
                                    {d.firstName} {d.lastName}
                                </option>
                            ))}
                        </select>

                        <select
                            name="appointmentId"
                            value={formData.appointmentId}
                            onChange={handleChange}
                            required
                            className="border p-2 rounded"
                        >
                            <option value="">Select Appointment</option>
                            {appointments.map(a => (
                                <option key={a.appointmentId} value={a.appointmentId}>
                                    #{a.appointmentId} - {patients.find(p => p.patientId === a.patientId).firstName} with {doctors.find(p => p.doctorId === a.doctorId).firstName}
                                </option>
                            ))}
                        </select>

                        <input
                            type="number"
                            name="totalAmount"
                            placeholder="Total Amount"
                            value={formData.totalAmount}
                            onChange={handleChange}
                            required
                            className="border p-2 rounded"
                        />



                        <div className="md:col-span-2">
                            <button
                                type="submit"
                                className="w-full bg-blue-600 text-white py-2 rounded hover:bg-blue-700"
                            >
                                Submit Bill
                            </button>
                        </div>
                    </form>
                )}

                {!showForm && bills.length > 0 && (
                    <div className="mt-10 overflow-x-auto">
                        <table className="min-w-full border border-gray-300 text-center">
                            <thead className="bg-blue-200">
                                <tr>
                                    <th className="py-2 border">Bill ID</th>
                                    <th className="py-2 border">Patient</th>
                                    <th className="py-2 border">Doctor</th>
                                    <th className="py-2 border">Appointment ID</th>
                                    <th className="py-2 border">Total Amount</th>
                                    <th className="py-2 border">Billing Date</th>
                                </tr>
                            </thead>
                            <tbody>
                                {bills.map(bill => (
                                    <tr key={bill.billId} className={`border-t ${bill.billId % 2 === 0 ? "bg-green-100" : "bg-white"}`}>
                                        <td className="py-2 border">{bill.billId}</td>
                                        <td className="py-2 border">{patients.find(p => p.patientId === bill.patientId).firstName} {patients.find(p => p.patientId === bill.patientId).lastName}</td>
                                        <td className="py-2 border">{doctors.find(p => p.doctorId === bill.doctorId).firstName} {doctors.find(p => p.doctorId === bill.doctorId).lastName}</td>
                                        <td className="py-2 border">{bill.appointmentId}</td>
                                        <td className="py-2 border">₹{bill.totalAmount}</td>
                                        <td className="py-2 border">{bill.billingDate?.split('T')[0]}</td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>
                )}
            </div>
        </>
    );
}
