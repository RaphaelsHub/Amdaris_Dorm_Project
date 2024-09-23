import React from "react";
import './Checkbox.css';

export default function Checkbox({ label, name, checked, onChange }) {
  return (
    <div className="checkbox-container">
      <label className="checkbox-label">
        <input
          type="checkbox"
          name={name}
          checked={checked}
          onChange={onChange}
          className="checkbox-input"
        />
        {label}
      </label>
    </div>
  );
}
