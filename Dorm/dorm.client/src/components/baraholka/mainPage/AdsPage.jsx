import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import './AdsPage.css';

export default function AdsPage() {
    const [ads, setAds] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchAds = async () => {
            try {
                const response = await axios.get('http://localhost:5077/api/ads', {
                    withCredentials: true,
                });
                setAds(response.data);
            } catch (error) {
                setError('Ошибка при загрузке объявлений');
            } finally {
                setLoading(false);
            }
        };

        fetchAds();
    }, []);

    const handleAdClick = (adId) => {
        navigate(`/ads/${adId}`);
    };

    if (loading) return <p>Загрузка объявлений...</p>;
    if (error) return <p>{error}</p>;

    return (
        <div className="ads-page">
            <h1 className="ads-header">Все объявления</h1>
            <div className="ads-list">
                {ads.map(ad => (
                    <div 
                        key={ad.id} 
                        className={`ad-card ${ad.status === 1 ? 'sold-out' : ''}`}
                        onClick={() => handleAdClick(ad.id)}
                    >
                        <img 
                            src={`data:image/jpeg;base64,${ad.image}`} 
                            alt={ad.subject} 
                            className="ad-card-image"
                        />
                        <div className="ad-card-info">
                            <h2>{ad.subject}</h2>
                            <p><strong>Цена:</strong> {ad.price} {ad.currency}</p>
                            <p><strong>Тип:</strong> {ad.type === 0 ? 'Продажа' : ad.type === 1 ? 'Покупка' : 'Другое'}</p>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
}
