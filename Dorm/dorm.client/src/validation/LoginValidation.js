import Joi from "joi";

export const loginSchema = Joi.object({
  email: Joi.string()
    .email({ tlds: { allow: false } })
    .required()
    .label("Email")
    .messages({
      "string.email": "Введите корректный email-адрес",
      "string.empty": "Поле обязательно для заполнения",
    }),
  password: Joi.string().required().label("Пароль").messages({
    "string.empty": "Поле обязательно для заполнения",
  }),
  rememberMe: Joi.boolean(),
});

export const validateLoginForm = (data) => {
  const options = { abortEarly: false };
  const { error } = loginSchema.validate(data, options);
  if (!error) return null;

  const errors = {};
  for (let item of error.details) {
    errors[item.path[0]] = item.message;
  }
  return errors;
};
