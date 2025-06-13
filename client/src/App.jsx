import { useState } from 'react';
import './App.css';
import Navbar from './components/Navbar.jsx';
import { Home } from './pages/Home';
import Footer from './components/Footer.jsx';
import { Route, Routes } from 'react-router-dom';
import Login from './pages/Login.jsx';
import AdminLogin from './pages/AdminLogin.jsx';
import ProtectedRoute from './components/ProtectedRoute.jsx';
import { AuthProvider } from './auth/AuthProvider.jsx';
import About from './pages/About.jsx';
import PatientDashboard from './components/PatientDashboard.jsx';
import RegisterForm from './pages/RegisterForm.jsx';
import Doctors from './components/Doctors.jsx';
import Appointments from './pages/Appointments.jsx';
import SingleDoctorDetails from './components/SingleDoctorDetails.jsx';
import DoctorDashboard from './components/DoctorDashboard.jsx';
import Contact from './pages/Contact.jsx';
import Unauthorized from './components/Unauthorized.jsx';
import EmployeeDashboard from './components/EmployeeDashboard.jsx';
import AdminDashboard from './components/AdminDashboard.jsx';
import DoctorRegisterForm from './pages/DoctorRegisterForm.jsx';
import EmployeeRegisterForm from './components/EmployeeRegister.jsx';
import BillSection from './pages/BillSection.jsx';

function App() {
  return (
    <>
      <div className="max-w-10xl mx-auto px-4">

        <AuthProvider>
          <Navbar />
          <Routes>
            <Route path='/' element={<Home></Home>} />
            <Route path='/about' element={<About />} />
            <Route path='/contact' element={<Contact />} />

            <Route path='/login' element={<Login />} />
            <Route path='/adminlogin' element={<AdminLogin />} />
            <Route path='/doctor-register' element={<ProtectedRoute role="admin">
              < DoctorRegisterForm />

            </ProtectedRoute>} />
            <Route path='/employee-register' element={<ProtectedRoute role="admin">
              < EmployeeRegisterForm />
            </ProtectedRoute>} />
            <Route path='/bills' element={<ProtectedRoute role="admin">
              < BillSection />
            </ProtectedRoute>} />

            <Route path='/register' element={<RegisterForm />} />
            <Route path='/all-doctors' element={<Doctors />} />
            <Route path='/unauthorized' element={<Unauthorized />} />

            <Route path='/single-doctor-details' element={<SingleDoctorDetails />} />
            <Route path='/admin-login' element={<AdminLogin />} />
            <Route path='/patient' element={
              <ProtectedRoute role="patient">
                <PatientDashboard />
              </ProtectedRoute>
            } />

            <Route path='/doctor' element={
              <ProtectedRoute role="doctor">
                <DoctorDashboard />
              </ProtectedRoute>
            } />

            <Route path='/appointments' element={
              <ProtectedRoute role={"patient"}>
                <Appointments />
              </ProtectedRoute>
            } />

            <Route path='/employee' element={
              <ProtectedRoute role="employee">
                <EmployeeDashboard />
              </ProtectedRoute>
            } />

            <Route path='/admin' element={
              <ProtectedRoute role="admin">
                <AdminDashboard />
              </ProtectedRoute>
            } />

          </Routes>
          <Footer />
        </AuthProvider>
      </div>

    </>
  );
}

export default App;
