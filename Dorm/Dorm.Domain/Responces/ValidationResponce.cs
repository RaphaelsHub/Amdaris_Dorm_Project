using System;
using System.Collections.Generic;

namespace Dorm.Domain.Responses
{
    /// <summary>
    /// Представляет результат процесса валидации.
    /// </summary>
    /// <param name="IsValid">
    /// Указывает, была ли валидация успешной.
    /// Если <c>true</c>, модель считается валидной и ошибки не найдены.
    /// Если <c>false</c>, у модели есть ошибки валидации.
    /// </param>
    /// <param name="Errors">
    /// Словарь, где ключом является имя поля или свойства, имеющего ошибки валидации,
    /// а значением — список сообщений об ошибках, связанных с этим полем.
    /// </param>
    public record ValidationResponse(
        bool IsValid,
        Dictionary<string, List<string?>> Errors
    );
}
