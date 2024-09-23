import React from 'react';
import classes from './InputField.module.css';

export default function InputField({ label, type, name, value, onChange, error }) {
  return (
    <div className={classes.inputFieldContainer}>
      <label className={classes.label} htmlFor={name}>{label}</label>
      <input
        className={classes.input}
        type={type}
        name={name}
        id={name}
        value={value}
        onChange={onChange}
      />
      {error && <div className={classes.error}>{error}</div>}
    </div>
  );
}