using Dorm.Domain.DTO.Auth;
using Dorm.Domain.Responses;

namespace Dorm.BLL.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginUser(LoginDto loginDto);
        Task<AuthResponse> RegisterUser(RegistrationDto registrationDto);
        Task<ValidationResponse> AuthValidation(object dto);
    }
}
