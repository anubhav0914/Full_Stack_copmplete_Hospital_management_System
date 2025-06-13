import React from 'react'
import Navbar from '../components/Navbar'
import Footer from '../components/Footer'

export default function Contact() {
  return (
    <>
    <Navbar/>
    <div className='p-18'>


    <div className="max-w-xl mx-auto mt-10 bg-white p-6 rounded-lg shadow-2xl">
      <h2 className="text-2xl font-bold mb-6 text-center text-blue-600">Contact Us</h2>
      <form className="space-y-4">
        
        <div>
          <label className="block text-gray-700">Email</label>
          <input
            type="email"
            placeholder="Enter your email"
            className="w-full px-4 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
        </div>

        <div>
          <label className="block text-gray-700">Mobile Number</label>
          <input
            type="tel"
            placeholder="Enter your mobile number"
            className="w-full px-4 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
        </div>

        <div>
          <label className="block text-gray-700">Occupation</label>
          <select className="w-full px-4 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500">
            <option value="">-- Select Occupation --</option>
            <option value="Doctor">Doctor</option>
            <option value="Nurse">Nurse</option>
            <option value="Cleaner">Cleaner</option>
            <option value="Compounder">Compounder</option>
            <option value="Patient">Patient</option>
            <option value="Driver">Driver</option>
            <option value="Other">Other</option>
          </select>
        </div>

        <div>
          <label className="block text-gray-700">Message (Optional)</label>
          <textarea
            rows="4"
            placeholder="Write your message here..."
            className="w-full px-4 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
          ></textarea>
        </div>

        <div className="text-center">
          <button
            type="submit"
            className="bg-blue-600 text-white px-6 py-2 rounded-md hover:bg-blue-700 transition"
          >
            Send Message
          </button>
        </div>

      </form>
    </div>
    </div>
    <Footer></Footer>
    </>
  )
}
