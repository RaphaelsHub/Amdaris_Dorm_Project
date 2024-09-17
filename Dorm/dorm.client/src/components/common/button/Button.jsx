import classes from "./Button.module.css"

export default function Button ({label, buttonType}) {
    return (
    <button className={classes.button} type={buttonType}>{label}</button>
    );
};
