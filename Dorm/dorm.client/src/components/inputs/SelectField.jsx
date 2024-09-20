import React from "react";
import classes from './SelectField.module.css'

export default function SelectField({ label, name, value, options, onChange, error }) {
  return (
    <div className={classes.selectField}>
      <label className={classes.label}>{label}</label>
      <select className={classes.select} name={name} value={value} onChange={onChange}>
        {options.map((option) => (
          <option key={option.value} value={option.value}>
            {option.label}
          </option>
        ))}
      </select>
      {error && <div className={classes.error}>{error}</div>}
    </div>
  );
}
