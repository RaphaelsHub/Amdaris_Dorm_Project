import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams, useNavigate } from 'react-router-dom';
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
      } catch (error) {
        setError('Ошибка при загрузке объявления');
      } finally {
        setLoading(false);
      }
    };

    fetchAdData();
  }, [adId]);

  const handleEdit = () => {
    navigate(`/ads/edit/${adId}`);
  };

  const handleDelete = async () => {
    try {
      await axios.delete(`http://localhost:5077/api/ads/${adId}`, {
        withCredentials: true,
      });
      navigate('/ads');
    } catch (error) {
      setError('Ошибка при удалении объявления');
    }
  };

  if (loading) return <p>Загрузка...</p>;
  if (error) return <p>{error}</p>;

  return (
    <div className="ad-page">

      <h1 className='ad-title'>{adData.subject}</h1>

      <div className='ad-content'>
        <div className='ad-left'>
          <img
            className='ad-image'
            src={`data:image/jpeg;base64,${adData.image}`}
            alt="Фото товара"
          />
          <p className='ad-description'>{adData.description}</p>
        </div>

        < div className='ad-right'>
          <p><strong>Имя владельца:</strong> {adData.name}</p>
          <p><strong>Дата:</strong> {new Date(adData.createdDate).toLocaleDateString()}</p>
          <p><strong>Тип:</strong> {adData.type === 0 ? 'Продажа' : adData.type === 1 ? 'Покупка' : 'Другое'}</p>
          <p><strong>Статус:</strong> {adData.status === 0 ? 'Активно' : 'Продано'}</p>
          <p className="ad-price">
            <strong>Цена:</strong> {adData.price} {adData.currency}
          </p>
        </div>
      </div>


      {adData.canEdit && (
        <div className="ad-page-actions">
          <Button label="Редактировать" onClick={handleEdit} />
          <Button label="Удалить" onClick={handleDelete} />
        </div>
      )}
    </div>
  );
}
