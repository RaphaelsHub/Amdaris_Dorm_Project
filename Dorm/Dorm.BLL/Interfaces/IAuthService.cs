using Dorm.Domain.DTO;
using Dorm.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dorm.BLL.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterUser(RegistrationModel registrationModel);
        Task<string> LoginUser(LoginModel loginModel);
    }
}
