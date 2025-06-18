import React, { useState, useEffect } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import { useAuth } from '../auth/AuthProvider';
import api from '../api/axios';


const OtpVerificationPage = () => {
    const location = useLocation();
    const { login } = useAuth();
    const navigate = useNavigate();
    const email = location.state?.email;

    const [otp, setOtp] = useState('');
    const [error, setError] = useState('');
    const [successMsg, setSuccessMsg] = useState('');

    useEffect(() => {
        if (!email) {
            navigate('/adminlogin'); // If no email was passed, redirect back
        }
    }, [email, navigate]);

    const handleVerifyOtp = async (e) => {
        e.preventDefault();
        setError('');
        setSuccessMsg('');

        try {
            const response = await api.post('/Login/AdminloginOTPVerification', {
                email,
                otp
            });
            const token = response.data.jwtToken;
            login(token);
            setSuccessMsg('OTP Verified!');
            setTimeout(()=>{

                navigate('/admin');
            },2000)
        } catch (err) {
            setError(err.response?.data?.message || 'Invalid OTP or server error');
        }
    };

    return (
        <div className="flex flex-col items-center justify-center min-h-screen bg-gray-100 p-4">
            <div className="bg-white p-8 rounded-lg shadow-md w-full max-w-md">
                <h2 className="text-2xl font-bold mb-4 text-center">OTP Verification</h2>
                <p className="mb-6 text-center text-gray-600">
                    Enter the OTP sent to <strong>{email}</strong>
                </p>

                <form onSubmit={handleVerifyOtp}>
                    <input
                        type="text"
                        placeholder="Enter OTP"
                        value={otp}
                        onChange={(e) => setOtp(e.target.value)}
                        className="w-full px-4 py-2 border rounded mb-4 focus:outline-none focus:ring focus:border-blue-300"
                        required
                    />

                    {error && <p className="text-red-600 mb-3">{error}</p>}
                    {successMsg && <p className="text-green-600 mb-3">{successMsg}</p>}

                    <button
                        type="submit"
                        className="w-full bg-blue-600 text-white py-2 rounded hover:bg-blue-700 transition"
                    >
                        Verify OTP
                    </button>
                </form>
            </div>
        </div>
    );
};

export default OtpVerificationPage;
