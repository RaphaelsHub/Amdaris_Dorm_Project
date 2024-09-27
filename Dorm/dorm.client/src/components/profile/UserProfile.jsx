import React, { useState, useEffect } from "react";
import axios from "axios";
import "./UserProfile.css";
import InputField from "../inputs/InputField";
import Button from "../common/button/Button";
import SelectField from "../inputs/SelectField";

const UserProfile = () => {
    const [isEditing, setIsEditing] = useState(false);
    const [errors, setErrors] = useState({});
    // const handleLogout = () => {
    //     localStorage.removeItem('token');
    //     localStorage.removeItem('userProfile');
    //     window.location.href = '/';
    // };

    const [profile, setProfile] = useState({
            photo: "",
            firstName: "",
            lastname: "", 
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
        const token = localStorage.getItem('token');
        
        if (!token) {
            console.error("No token found.");
            return;
        }
    
        const fetchProfile = async () => {
            try {
                const response = await axios.get('http://localhost:5077/api/StudentProfile', {
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${token}`
                    },
                    withCredentials: true
                });
    
                const profileData = response.data;
    
                localStorage.setItem('userProfile', JSON.stringify(profileData));

                setProfile({
                    ...profileData,
                    gender: profileData.gender.toString()
                });
            } catch (error) {
                console.error("Error fetching profile:", error);
            }
        };
    
        fetchProfile();
    }, []);
    
    


    const handleChange = (e) => {
        const { name, value, files } = e.target;

        if(name === "photo" && files.length > 0) {
            const file = files[0];
            const reader = new FileReader();

            reader.onloadend = () => {
                setProfile(prevProfile => ({
                    ...prevProfile,
                    photo: reader.result.split(',')[1],
                    photoPreview: URL.createObjectURL(file)
                }));
            };

            reader.readAsDataURL(file);
        }
        else {
            setProfile({
                ...profile,
                [name]: name === "gender" ? parseInt(value) : value,
            });
        }
    };

    // const validateFields = () => {
    //     const newErrors = {};
    //     const groupPattern = /^[A-Z]{2,5}-\d{3,4}$/;

    //     if (!profile.firstName) newErrors.firstName = "Поле обязательно для заполнения";
    //     if (!profile.lastname) newErrors.lastname = "Поле обязательно для заполнения";
    //     if (!profile.email) newErrors.email = "Поле обязательно для заполнения";
    //     if (!profile.phoneNumber) newErrors.phoneNumber = "Поле обязательно для заполнения";
    //     if (!profile.faculty) newErrors.faculty = "Поле обязательно для заполнения";
    //     if (!profile.specialty) newErrors.specialty = "Поле обязательно для заполнения";
    //     if (!profile.group) {
    //         newErrors.group = "Поле обязательно для заполнения";
    //     } else if (!groupPattern.test(profile.group)) {
    //         newErrors.group = "Группа должна быть записана в формате TI-2210";
    //     }

    //     return newErrors;

    // }

    const handleSubmit = async (e) => {
        e.preventDefault();

        // const newErrors = validateFields();
        // if (Object.keys(newErrors).length > 0) {
        //     setErrors(newErrors);
        //     return;
        // }

        const profileToSend = {
            ...profile,
            gender: parseInt(profile.gender),
            photo: profile.photo ? profile.photo : null
        };

        try{
            if(isEditing) {
                const response = await axios.put(`http://localhost:5077/api/StudentProfile`, profileToSend, {
                    headers: {
                        "Content-Type": "application/json",
                    },
                    withCredentials: true
                });

                console.log("Профиль обновлен:", response.data);

                localStorage.setItem('userProfile', JSON.stringify(profileToSend));
            }
        }
        catch (error) {
            console.error("Ошибка при сохранении товара:", error.response ? error.response.data : error.message);
        }

        setIsEditing(!isEditing);
    };

    

    // const userStatusMap = {
    //     1: "Student",
    //     2: "Moderator",
    //     3: "Admin",
    // };

    const genderMap = {
        0: "Мужской",
        1: "Женский",
    };
    

    return (
    <div className="profile-page">
    <div className="profile-container">
      
      <div className="profile-details">
        {isEditing ? (
          <>
            <InputField
                label="Имя"
                type="text"
                name="firstName"
                value={profile.firstName}
                onChange={handleChange}
                // error={errors.firstName}
            />

            <InputField
                label="Фамилия"
                type="text"
                name="lastname"
                value={profile.lastname}
                onChange={handleChange}
                // error={errors.lastname}
            />

            <SelectField
                label="Пол"
                name="gender"
                value={profile.gender}
                options={[
                    { value: 0, label: "Мужской" },
                    { value: 1, label: "Женский" }
                ]}
                onChange={handleChange}
            />

            <InputField
                label="Email"
                type="email"
                name="email"
                value={profile.email}
                onChange={handleChange}
                // error={errors.email}
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
                // error={errors.faculty}
            />

            <InputField
                label="Специальность"
                type="text"
                name="specialty"
                value={profile.specialty}
                onChange={handleChange}
                // error={errors.specialty}
            />

            <InputField
                label="Группа"
                type="text"
                name="group"
                value={profile.group}
                onChange={handleChange}
                // error={errors.group}
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
            <p><strong>Фамилия:</strong> {profile.lastname}</p>
            <p><strong>Пол:</strong> {genderMap[profile.gender]}</p>
            <p><strong>Email:</strong> {profile.email}</p>
            <p><strong>Телефон:</strong> {profile.phoneNumber}</p>
            <p><strong>Факультет:</strong> {profile.faculty}</p>
            <p><strong>Специальность:</strong> {profile.specialty}</p>
            <p><strong>Группа:</strong> {profile.group}</p>
            <p><strong>Комната:</strong> {profile.roomNumber}</p>
          </>
        )}
        </div>

        <div className="profile-photo">
        <img
            src={profile.photo ? `data:image/jpeg;base64,${profile.photo}` : "default-photo.jpg"}
            alt="Фото профиля"
        />
        {isEditing && (
            <label className="upload-button">
                Загрузить фото
                <input 
                    type="file" 
                    accept="image/*" 
                    name="photo"
                    onChange={handleChange}
                />
            </label>
        )}

        

        <div className="editProfile">
            <Button 
            label={isEditing ? "Сохранить" : "Редактировать"} 
            buttonType="button" 
            onClick={handleSubmit}
        />
        </div>

      </div>
      
    </div>
    </div>
  );
};

export default UserProfile;
