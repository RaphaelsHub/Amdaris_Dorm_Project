import React from 'react';
import './HomePage.css';
import photo from '../../../assets/photo.png'

export default function HomePage() {
  return (
    <div className="homepage">
      <img 
        src={photo} 
        alt="Welcome to DormHub" 
        className="homepage-image" 
      />
    </div>
  );
}
