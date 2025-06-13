import React from 'react';

export default function WhatsAppButton() {
  const phoneNumber = '919876543210'; // e.g., India number (91) + mobile
  const message = 'Hello, I would like to book an appointment!';
  const encodedMessage = encodeURIComponent(message);

  return (
    <a
      href={`https://wa.me/${phoneNumber}?text=${encodedMessage}`}
      target="_blank"
      rel="noopener noreferrer"
    >
      <button className="bg-green-500 text-white px-6 py-2 rounded-lg hover:bg-green-600">
        Chat on WhatsApp
      </button>
    </a>
  );
}
