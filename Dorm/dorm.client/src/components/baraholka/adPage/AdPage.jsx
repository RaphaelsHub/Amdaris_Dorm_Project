import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import axios from 'axios';
import Button from '../../common/button/Button';
import './AdPage.css';

export default function AdPage() {
    const { adId } = useParams();
    const navigate = useNavigate();
    const [adData, setAdData] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchAdData = async () => {
            try {
                const response = await axios.get(`http://localhost:5077/api/ads/${adId}`, {
                    headers: {
                      'Content-Type': 'application/json',
                    },
                    withCredentials: true,
                });
                setAdData(response.data);
            } 
            catch (err) {
                setError("Ошибка загрузки объявления");
                console.error("Ошибка:", err.response ? err.response.data : err.message);
            }  
            finally {
                setLoading(false);
            }
        };

        fetchAdData();
    }, [adId]);


    const handleEdit = async () => {
        navigate(`/edit-ad/${adId}`);
    };


    const handleDelete = async () => {
        try {
          await axios.delete(`http://localhost:5077/api/ads/${adId}`, {
            headers: {
              'Content-Type': 'application/json',
            },
            withCredentials: true,
          });
          navigate('/');
        } catch (err) {
          setError("Ошибка при удалении объявления");
        }
      };

    if (loading) return <div>Загрузка...</div>;
    if (error) return <div>{error}</div>;

    const { name, number, type, status, subject, description, price, currency, image, createdDate, canEdit } = adData;


    return (
        <div className="ad-page">
          <div className="ad-header">
            <h1>{subject}</h1>
            <div className={`ad-status ${status === 1 ? 'active' : 'sold'}`}>
              {status === 1 ? 'Активно' : 'Продано'}
            </div>
          </div>
    
          <div className="ad-content">
            <div className="ad-details">
              <img src={image} alt={subject} className="ad-image" />
              <div className="ad-info">
                <p><strong>Имя владельца:</strong> {name}</p>
                <p><strong>Телефон:</strong> {number}</p>
                <p><strong>Тип объявления:</strong> {type === 0 ? 'Продажа' : type === 1 ? 'Покупка' : 'Другое'}</p>
                <p><strong>Описание:</strong> {description}</p>
                <p><strong>Цена:</strong> {price} {currency}</p>
                <p><strong>Дата создания:</strong> {new Date(createdDate).toLocaleString()}</p>
              </div>
            </div>
          </div>
    
          {canEdit && (
            <div className="ad-actions">
              <Button label="Редактировать" onClick={handleEdit} />
              <Button label="Удалить" onClick={handleDelete} />
            </div>
          )}
        </div>
    );
}
