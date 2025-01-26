import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import './NavBar.css';

export default function NavBar() {
  const [isMarketplaceDropdownOpen, setIsMarketplaceDropdownOpen] = useState(false);
  const [isTicketsDropdownOpen, setIsTicketsDropdownOpen] = useState(false);

  const toggleMarketplaceDropdown = () => {
    setIsMarketplaceDropdownOpen((prev) => !prev);
    setIsTicketsDropdownOpen(false); // Закрыть другой дропдаун
  };

  const toggleTicketsDropdown = () => {
    setIsTicketsDropdownOpen((prev) => !prev);
    setIsMarketplaceDropdownOpen(false); // Закрыть другой дропдаун
  };

  const handleClickOutside = (e) => {
    if (!e.target.closest('.dropdown')) {
      setIsMarketplaceDropdownOpen(false);
      setIsTicketsDropdownOpen(false);
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

        <Link to="/chat" className='nav-link'>
          Чат
        </Link>

        <div className="dropdown">
          <button onClick={toggleMarketplaceDropdown} className="dropdown-toggle">
            Барахолка
          </button>

          {isMarketplaceDropdownOpen && (
            <div className="dropdown-menu">
              <Link to="/ads" className="dropdown-item" onClick={() => setIsMarketplaceDropdownOpen(false)}>
                Все объявления
              </Link>
              <Link to="/adform" className="dropdown-item" onClick={() => setIsMarketplaceDropdownOpen(false)}>
                Создать объявление
              </Link>
            </div>
          )}
        </div>

        <Link to="/reservation" className='nav-link'>
          Стиралки
        </Link>

        <div className="dropdown">
          <button onClick={toggleTicketsDropdown} className="dropdown-toggle">
            Тикеты
          </button>

          {isTicketsDropdownOpen && (
            <div className="dropdown-menu">
              <Link to="/tickets" className="dropdown-item" onClick={() => setIsTicketsDropdownOpen(false)}>
                Все мои тикеты
              </Link>
              <Link to="/ticket" className="dropdown-item" onClick={() => setIsTicketsDropdownOpen(false)}>
                Создать тикет
              </Link>
            </div>
          )}
        </div>

        <Link to="/profile" className='nav-link'>
          Профиль
        </Link>

        <Link to="/login" className='nav-link'>
          Войти
        </Link>

        <Link to="/register" className='nav-link'>
          Регистрация
        </Link>
        
      </div>
    </nav>
  );
}
