import { Link } from 'react-router-dom';
import React from 'react';
import { FaFlask, FaHeartbeat, FaProcedures } from 'react-icons/fa';

const About = () => {
  return (
    <div className="max-w-7xl mt-20 mx-auto px-4 py-20 space-y-20">
      
      {/* About Section */}
      <section className="grid md:grid-cols-2 gap-12 items-center">
        <div>
          <h2 className="text-4xl font-bold text-slate-900 mb-4">About Our Hospital</h2>
          <p className="text-gray-700 text-lg mb-6">
            Our hospital is committed to providing world-class healthcare with compassion, integrity, and innovation.
            We offer a wide range of medical services, advanced treatments, and caring support to our patients.
          </p>
          <button onClick={()=>("/appointments")} className=" px-6 py-3 border border-slate-900 rounded-lg text-slate-900 font-semibold hover:bg-slate-900 hover:text-white transition">
            Get an Appointment
          </button>
        </div>
        <img
          src="./public/hospital.png" // Replace with real image path
          alt="Doctor"
          className="rounded-xl shadow-lg w-full h-auto object-cover"
        />
      </section>

      {/* Our Mission Section */}
      <section className="grid md:grid-cols-2 gap-12 items-center">
        <img
          src="./public/hospital.png" // Replace with real image path
          alt="Mission"
          className="rounded-xl shadow-lg w-full h-auto object-cover"
        />
        <div>
          <h3 className="text-3xl font-bold text-slate-900 mb-3">Our Mission</h3>
          <p className="text-lg text-gray-700 mb-6">
            "To deliver exceptional care and improve lives through compassion, excellence, and innovation."
          </p>
          <p className="text-gray-600 mb-6">
            We aim to be at the forefront of medical advancements while treating every patient with dignity and respect.
            From routine checkups to complex surgeries, our staff ensures the highest level of professionalism and care.
          </p>
          <Link to="/contact">
          <button className="px-6 py-3 bg-slate-900 text-white rounded-lg font-semibold hover:bg-slate-800 transition">
            Contact Us
           </button>
          </Link>
        </div>
      </section>

      {/* Services */}
      <section className="grid md:grid-cols-3 gap-8 text-center">
        <div className="p-6 border rounded-xl hover:shadow-lg transition">
          <FaHeartbeat className="text-4xl text-green-600 mx-auto mb-4" />
          <h4 className="text-xl font-semibold text-slate-800 mb-2">Mental Health Care</h4>
          <p className="text-gray-600">Compassionate care for mental wellness, inc/appointmentsluding therapy and counseling.</p>
        </div>
        <div className="p-6 border rounded-xl hover:shadow-lg transition">
          <FaProcedures className="text-4xl text-blue-600 mx-auto mb-4" />
          <h4 className="text-xl font-semibold text-slate-800 mb-2">Physical Therapy</h4>
          <p className="text-gray-600">Recovery support with expert physical therapy and rehabilitation programs.</p>
        </div>
        <div className="p-6 border rounded-xl hover:shadow-lg transition">
          <FaFlask className="text-4xl text-purple-600 mx-auto mb-4" />
          <h4 className="text-xl font-semibold text-slate-800 mb-2">Lab & Diagnostics</h4>
          <p className="text-gray-600">State-of-the-art diagnostic labs for fast, accurate results and testing.</p>
        </div>
      </section>

      {/* FAQ Section */}
      <section>
        <h3 className="text-3xl font-bold text-slate-900 text-center mb-10">Frequently Asked Questions</h3>
        <div className="space-y-4">
          {faqData.map((faq, index) => (
            <details key={index} className="p-4 border rounded-lg cursor-pointer">
              <summary className="font-medium text-slate-800">{faq.question}</summary>
              <p className="mt-2 text-gray-600">{faq.answer}</p>
            </details>
          ))}
        </div>
      </section>
    </div>
  );
};

const faqData = [
  {
    question: 'How can I book an appointment?',
    answer: 'You can book appointments through our website, mobile app, or by calling our front desk.',
  },
  {
    question: 'Do you offer emergency services?',
    answer: 'Yes, our emergency department is open 24/7 with trained staff and rapid response teams.',
  },
  {
    question: 'Can I get lab results online?',
    answer: 'Yes, registered patients can view their lab reports securely through our patient portal.',
  },
];

export default About;
