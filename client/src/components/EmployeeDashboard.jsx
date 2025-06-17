import React, { useState, useEffect } from 'react';
import { useAuth } from '../auth/AuthProvider';
import api from '../api/axios';

const EmployeeDashboard = () => {
    const [profile, setProfile] = useState(null);
    const [department, setDepartment] = useState(null);
        const [showDept, setShowdept] = useState(false)
    
    const { user } = useAuth();

    useEffect(() => {
        if (user?.id) {
            api.get(`Employee/GetById/${user.id}`)
                .then(res => setProfile(res.data.data))
                .catch(err => console.error('Failed to fetch profile', err));
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
        if (profile?.departmentId) {
            api.get(`Department/GetById/${profile.departmentId}`)
                .then(res => setDepartment(res.data.data))
                .catch(err => console.error('Failed to fetch department', err));
        }
    }, [profile]);

    if (!profile || !department) {
        return <div className="text-center mt-20 text-green-600 text-2xl font-semibold">Loading profile...</div>;
    }

    return (
        <div className="max-w-6xl mx-auto mt-30 mb-20 bg-white rounded-3xl shadow-2xl overflow-hidden border border-green-300">
            {/* Header */}
            
            <div className="bg-green-700 pt-20 mb-10 flex h-50 items-center gap-8 text-white">
                <img
                    src={profile.profileImage}
                    alt="Employee"
                    className="w-52 h-52 rounded-full border-4 border-white object-cover shadow-lg"
                />
                <div >
                    <h1 className="text-4xl font-bold">{`${profile.firstName} ${profile.lastName}`}</h1>
                    <p className="text-xl">{profile.role}</p>
                    <p className="text-md mt-1 ">Joined on {new Date(profile.joiningDate).toLocaleDateString()}</p>
                </div>
            </div>

            {/* Main Info */}
            <div className="p-10 text-green-900 text-xl space-y-10">
                {/* Basic Info */}
                <section>
                    <h2 className="text-3xl font-bold mb-6 text-green-700">Your Info</h2>
                    <div className="grid grid-cols-1 sm:grid-cols-2 gap-6">
                        <p><span className="font-semibold">Employee :</span> {department.departmentName}</p>
                        <p><span className="font-semibold">Department:</span> {department.departmentName}</p>
                        <p><span className="font-semibold">Email:</span> {profile.email}</p>
                        <p><span className="font-semibold">Phone Number:</span> {profile.phoneNumber}</p>
                        <p><span className="font-semibold">Gender:</span> {profile.gender}</p>
                        <p><span className="font-semibold">Role:</span> {profile.role}</p>
                        <p><span className="font-semibold">Joining Date:</span> {new Date(profile.joiningDate).toLocaleDateString()}</p>
                        <p><span className="font-semibold">Salary:</span> ₹{profile.salary}</p>
                    </div>
                </section>
                <div className="mt-8 flex flex-col sm:flex-row gap-4">
                   
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
            </div>
        </div>
    );
};

export default EmployeeDashboard;
