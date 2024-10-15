import React from 'react';
import './WasherCard.css';

const WasherCard = ({ washer }) => {
  return (
    <div className="washer-card">
      <h2>{washer.name}</h2>
    </div>
  );
};

export default WasherCard;
