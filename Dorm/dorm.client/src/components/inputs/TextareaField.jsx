import React, { useRef, useEffect } from 'react';
import './TextareaField.css';


const TextareaField = ({ label, name, value, onChange, rows, error }) => {
    const textareaRef = useRef(null);

    useEffect(() => {
        if (textareaRef.current) {
            textareaRef.current.style.height = 'auto'; // Сброс высоты
            textareaRef.current.style.height = `${textareaRef.current.scrollHeight}px`; // Установка высоты
        }
    }, [value]);

    return (
        <div className="field-container">
            <label className="label" htmlFor={name}>{label}</label>
            <textarea
                id={name}
                name={name}
                value={value}
                onChange={onChange}
                rows={rows}
                ref={textareaRef}
                className="textarea"
            />
            {error && <div className="error">{error}</div>}
        </div>
    );
}

export default TextareaField;
