import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useNavigate, useParams } from 'react-router-dom';
import InputField from '../../inputs/InputField';
import SelectField from '../../inputs/SelectField';
import Button from '../../common/button/Button';
import TextareaField from '../../inputs/TextareaField';
import './AdForm.css';

export default function AdForm() {
    const { adId } = useParams();
    const [formData, setFormData] = useState({
        name: "",
        number: "",
        type: 0,
        status: 0,
        subject: "",
        description: "",
        price: 0,
        currency: "MDL",
        image: ""
    });
    const [errors, setErrors] = useState({});
    const navigate = useNavigate();
    const [isEditMode, setIsEditMode] = useState(false);

    
    useEffect(() => {
        if (adId) {
            setIsEditMode(true);
            const fetchAdData = async () => {
                try {
                    const response = await axios.get(`http://localhost:5077/api/ads/${adId}`, {
                        withCredentials: true
                    });
                    setFormData({
                        name: response.data.name,
                        number: response.data.number,
                        type: response.data.type,
                        status: response.data.status,
                        subject: response.data.subject,
                        description: response.data.description,
                        price: response.data.price,
                        currency: response.data.currency,
                        image: response.data.image ? `data:image/jpeg;base64,${response.data.image}` : ""
                    });
                } catch (error) {
                    console.error("Ошибка при загрузке данных объявления:", error.response ? error.response.data : error.message);
                }
            };

            fetchAdData();
        }
    }, [adId]);

    const handleChange = (e) => {
        const { name, value, files } = e.target;
        if (name === "image" && files) {
            const file = files[0];
            const reader = new FileReader();

            reader.onloadend = () => {
                setFormData({
                    ...formData,
                    image: reader.result
                });
            };

            reader.readAsDataURL(file);
        } else {
            setFormData({
                ...formData,
                [name]: value,
            });
        }
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        const newErrors = {};
        if (!formData.name) newErrors.name = "Поле обязательно для заполнения";
        if (!formData.number) newErrors.number = "Поле обязательно для заполнения";
        if (!formData.subject) newErrors.subject = "Поле обязательно для заполнения";
        if (!formData.price) newErrors.price = "Поле обязательно для заполнения";

        if (Object.keys(newErrors).length > 0) {
            setErrors(newErrors);
            return;
        }

        const formDataToSend = {
            ...formData,
            type: parseInt(formData.type),
            status: parseInt(formData.status),
            image: formData.image ? formData.image.split(',')[1] : null
        };

        try {
            if (isEditMode) {
                const response = await axios.put(`http://localhost:5077/api/ads/${adId}`, formDataToSend, {
                    headers: {
                        "Content-Type": "application/json",
                    },
                    withCredentials: true,
                });
                console.log("Товар обновлен:", response.data);
                navigate(`/ads/${response.data.id}`);
            } else {
                const response = await axios.post("http://localhost:5077/api/ads", formDataToSend, {
                    headers: {
                        "Content-Type": "application/json",
                    },
                    withCredentials: true,
                });
                console.log("Товар добавлен:", response.data);
                navigate(`/ads/${response.data.id}`);
            }

        } catch (error) {
            console.error("Ошибка при сохранении товара:", error.response ? error.response.data : error.message);
        }
    };

    return (
        <div className='ad-form-page'>
            <div className='ad-form-container'>
                <h2 className='ad-form-header'>
                    {isEditMode ? 'Редактировать объявление' : 'Заполните объявление'}
                </h2>

                <form onSubmit={handleSubmit}>
                    <InputField
                        label="Имя владельца"
                        type="text"
                        name="name"
                        value={formData.name}
                        onChange={handleChange}
                        error={errors.name}
                    />

                    <InputField
                        label="Телефон"
                        type="text"
                        name="number"
                        value={formData.number}
                        onChange={handleChange}
                        error={errors.number}
                    />

                    <SelectField
                        label="Тип объявления"
                        name="type"
                        value={formData.type}
                        options={[
                            { value: 0, label: "Продажа" },
                            { value: 1, label: "Покупка" },
                            { value: 2, label: "Другое" }
                        ]}
                        onChange={handleChange}
                        error={errors.type}
                    />

                    <SelectField
                        label="Статус объявления"
                        name="status"
                        value={formData.status}
                        options={[
                            { value: 0, label: "Активно" },
                            { value: 1, label: "Продано" }
                        ]}
                        onChange={handleChange}
                        error={errors.status}
                    />

                    <InputField
                        label="Название товара"
                        type="text"
                        name="subject"
                        value={formData.subject}
                        onChange={handleChange}
                        error={errors.subject}
                    />

                    <TextareaField
                        label="Описание"
                        name="description"
                        value={formData.description}
                        onChange={handleChange}
                        rows="4"
                        error={errors.description}
                    />

                    <div className="price-container">
                        <InputField
                            label="Цена"
                            type="number"
                            name="price"
                            value={formData.price}
                            onChange={handleChange}
                            error={errors.price}
                        />

                        <SelectField
                            label="Валюта"
                            name="currency"
                            value={formData.currency}
                            options={[
                                { value: "MDL", label: "MDL" },
                                { value: "EUR", label: "EUR" },
                                { value: "USD", label: "USD" }
                            ]}
                            onChange={handleChange}
                            error={errors.currency}
                        />
                    </div>

                    <InputField
                        label="Изображение"
                        type="file"
                        name="image"
                        onChange={handleChange}
                        error={errors.image}
                    />

                    <Button label={isEditMode ? "Сохранить изменения" : "Разместить объявление"} buttonType="submit" />
                </form>
            </div>
        </div>
    )
}