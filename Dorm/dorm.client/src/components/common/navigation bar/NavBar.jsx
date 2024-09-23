import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import './NavBar.css';

export default function NavBar() {
  const [isDropdownOpen, setIsDropdownOpen] = useState(false);

  const toggleDropdown = () => {
    setIsDropdownOpen((prev) => !prev);
  };

  
  const handleClickOutside = (e) => {
    if (!e.target.closest('.dropdown')) {
      setIsDropdownOpen(false);
    }
  };

  
  React.useEffect(() => {
    document.addEventListener('click', handleClickOutside);
    return () => {
      document.removeEventListener('click', handleClickOutside);
    };
  }, []);

  return (
    <nav className="navbar">
      <div className="container">

        <Link to="/" className="logo">
          DormHub
        </Link>

        <Link to="/register" className='nav-link'>
          Регистрация
        </Link>

        <div className="dropdown">
          <button onClick={toggleDropdown} className="dropdown-toggle">
            Барахолка
          </button>

          {isDropdownOpen && (
            <div className="dropdown-menu">
              <Link to="/ads" className="dropdown-item" onClick={() => setIsDropdownOpen(false)}>
                Все объявления
              </Link>
              <Link to="/adform" className="dropdown-item" onClick={() => setIsDropdownOpen(false)}>
                Создать объявление
              </Link>
            </div>
          )}
        </div>

      </div>
    </nav>
  );
}
