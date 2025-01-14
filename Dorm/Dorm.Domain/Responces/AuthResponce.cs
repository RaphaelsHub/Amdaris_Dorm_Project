﻿using System;

namespace Dorm.Domain.Responses
{
    /// <summary>
    /// Представляет ответ на запрос аутентификации.
    /// </summary>
    /// <param name="Success">
    /// Указывает, был ли запрос аутентификации успешным.
    /// Если <c>true</c>, аутентификация прошла успешно.
    /// Если <c>false</c>, аутентификация не удалась.
    /// </param>
    /// <param name="Error">
    /// Сообщение об ошибке, если запрос аутентификации не удался.
    /// Это поле может быть <c>null</c>, если ошибок не возникло или запрос был успешным.
    /// </param>
    /// <param name="Token">
    /// Токен аутентификации, предоставляемый при успешной аутентификации.
    /// Это поле может быть <c>null</c>, если токен не был предоставлен или запрос не удался.
    /// </param>
    public record AuthResponse(
        bool Success,
        string? Error = null,
        string? Role = null,
        string? Token = null
    );
}
