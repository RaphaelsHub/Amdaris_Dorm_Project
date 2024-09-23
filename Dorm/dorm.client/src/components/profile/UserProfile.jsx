import React, { useState, useEffect } from "react";
import axios from "axios";
import Cookies from "js-cookie";
import "./UserProfile.css";
import InputField from "../inputs/InputField";
import Button from "../common/button/Button";

const UserProfile = () => {
    const [isEditing, setIsEditing] = useState(false);
    const [profile, setProfile] = useState({
        photo: "",
        firstName: "",
        lastName: "", 
        gender: "",
        email:"", 
        phoneNumber: "",
        userStatus: 1,
        faculty: "",
        specialty: "",
        group: "", 
        roomNumber: ""
    });

    useEffect(() => {
        const fetchProfile = async () => {
            const id = Cookies.get('userId'); // Получаем ID из cookie
            if (!id) {
                console.error("User ID not found in cookies");
                return;
            }
            try {
                const response = await axios.get(`http://localhost:5077/api/StudentProfile/${id}`, {
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    withCredentials: true
            });
                setProfile(response.data);
            } catch (error) {
                console.error("Error fetching profile:", error);
            }
        };

        fetchProfile();
    }, []);


    const handleEditClick = () => {
        setIsEditing(!isEditing);
    };

    const handleFileChange = (e) => {
        const file = e.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onloadend = () => {
                setProfile({ ...profile, photo: reader.result });
            };
            reader.readAsDataURL(file);
        }
    };
    

    const handleChange = (e) => {
        const { name, value } = e.target;
        setProfile({ ...profile, [name]: value });
    };

    const userStatusMap = {
        1: "Student",
        2: "Moderator",
        3: "Admin",
    };

    return (
    <div className="profile-container">

      <div className="profile-photo">
        <img src={profile.photo || "default-photo.jpg"} alt="Photo" />
        {isEditing && (
            <label className="upload-button">
                Загрузить фото
                <input type="file" accept="image/*" onChange={handleFileChange}/>
            </label>
        )}
      </div>

      <div className="profile-details">
        {isEditing ? (
          <>
            <InputField
                label="Имя"
                type="text"
                name="firstName"
                value={profile.firstName}
                onChange={handleChange}
            />

            <InputField
                label="Фамилия"
                type="text"
                name="lastName"
                value={profile.lastName}
                onChange={handleChange}
            />

            <InputField
                label="Пол"
                type="text"
                name="gender"
                value={profile.gender}
                onChange={handleChange}
            />

            <InputField
                label="Email"
                type="email"
                name="email"
                value={profile.email}
                onChange={handleChange}
            />

            <InputField
                label="Телефон"
                type="text"
                name="phoneNumber"
                value={profile.phoneNumber}
                onChange={handleChange}
            />

            {/* <SelectField
                label="Статус"
                name="userStatus"
                value={profile.userStatus}
                onChange={handleChange}
                options={[
                    { value: 0, label: "Студент" },
                    { value: 1, label: "Модератор" },
                    { value: 2, label: "Администратор" }
                ]}
                error={errors.type}
            /> */}

            <InputField
                label="Факультет"
                type="text"
                name="faculty"
                value={profile.faculty}
                onChange={handleChange}
            />

            <InputField
                label="Специальность"
                type="text"
                name="specialty"
                value={profile.specialty}
                onChange={handleChange}
            />

            <InputField
                label="Группа"
                type="text"
                name="group"
                value={profile.group}
                onChange={handleChange}
            />

            <InputField
                label="Комната"
                type="text"
                name="roomNumber"
                value={profile.roomNumber}
                onChange={handleChange}
            />

          </>
        ) : (
          <>
            <p><strong>Имя:</strong> {profile.firstName}</p>
            <p><strong>Фамилия:</strong> {profile.lastName}</p>
            <p><strong>Пол:</strong> {profile.gender}</p>
            <p><strong>Email:</strong> {profile.email}</p>
            <p><strong>Телефон:</strong> {profile.phoneNumber}</p>
            <p><strong>Факультет:</strong> {profile.faculty}</p>
            <p><strong>Специальность:</strong> {profile.specialty}</p>
            <p><strong>Группа:</strong> {profile.group}</p>
            <p><strong>Комната:</strong> {profile.roomNumber}</p>
          </>
        )}
        
        <Button 
            label={isEditing ? "Сохранить" : "Редактировать"} 
            buttonType="button" 
            onClick={handleEditClick}
        />
      </div>
    </div>
  );
};

export default UserProfile;
