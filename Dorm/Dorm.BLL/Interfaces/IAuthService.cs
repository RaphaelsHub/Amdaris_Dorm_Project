using Dorm.Domain.DTO;
using Dorm.Domain.Models;
using Dorm.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dorm.BLL.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginUser(LoginModel loginModel);
        Task<AuthResponse> RegisterUser(RegistrationModel registrationModel);
        Task<ValidationResponse> AuthValidation(object model);
    }
}
