import classes from "./Button.module.css"

export default function Button ({label, buttonType, onClick}) {
    return (
    <button className={classes.button} type={buttonType} onClick={onClick}>{label}</button>
    );
};
