import Joi from "joi";


export const schema = Joi.object({
  email: Joi.string()
    .email({ tlds: { allow: false } })
    .required()
    .label("Email")
    .messages({
      "string.email": "Введите корректный email-адрес",
      "string.empty": "Поле обязательно для заполнения",
    }),
  password: Joi.string()
    .min(8)
    .pattern(new RegExp(/^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?.&])[A-Za-z\d@$!%*?.&]+$/))
    .required()
    .label("Пароль")
    .messages({
      "string.min": "Пароль должен быть минимум 8 символов",
      "string.pattern.base":
        "Пароль должен содержать одну заглавную букву, одну цифру и один спецсимвол",
      "string.empty": "Поле обязательно для заполнения",
    }),
  confirmPassword: Joi.any()
    .valid(Joi.ref("password"))
    .required()
    .label("Подтвердить пароль")
    .messages({
      "any.only": "Пароли должны совпадать",
      "string.empty": "Поле обязательно для заполнения",
    }),
  firstName: Joi.string()
    .required()
    .label("Имя")
    .messages({
      "string.empty": "Поле обязательно для заполнения",
    }),
  lastName: Joi.string()
    .required()
    .label("Фамилия")
    .messages({
      "string.empty": "Поле обязательно для заполнения",
    }),
  group: Joi.string()
    .pattern(new RegExp(/^[A-Z]{2,5}-\d{3,4}$/))
    .required()
    .label("Группа")
    .messages({
      "string.empty": "Поле обязательно для заполнения",
      "string.pattern.base":
        "Группа должна быть записана в формате TI-2210",
    }),
});


export const validateForm = (data) => {
  const options = { abortEarly: false };
  const { error } = schema.validate(data, options);
  if (!error) return null;

  const errors = {};
  for (let item of error.details) {
    errors[item.path[0]] = item.message;
  }
  return errors;
};
