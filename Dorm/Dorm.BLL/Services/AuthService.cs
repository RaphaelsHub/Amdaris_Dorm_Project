using Dorm.BLL.Interfaces;
using Dorm.DAL.Interfaces;
using Dorm.Domain.DTO;
using Dorm.Domain.Entities.User;
using Dorm.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dorm.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsersRepository<User> _userRepository;

        public AuthService(IUsersRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> RegisterUser(RegistrationModel registrationModel)
        {
            var user = new User()
            {
                Email = registrationModel.Email,
                FirstName = registrationModel.FirstName,
                Surname = registrationModel.Surname,
                Group = registrationModel.Group,
                IpAddress = registrationModel.IpAddress,
                Country = registrationModel.Country,
                City = registrationModel.City,
                DeviceType = registrationModel.DeviceType,
            };

            user.PasswordHash = new PasswordHasher<User>()
                 .HashPassword(user, registrationModel.Password);

            await _userRepository.Create(user);



            return "";

        }
        public async Task<string> LoginUser(LoginModel loginModel)
        {
            var user = await _userRepository.GetByEmail(loginModel.Email); 

            if (user == null)
            {
                return "";
            }

            var result = new PasswordHasher<User>()
                            .VerifyHashedPassword(user, user.PasswordHash, loginModel.Password);

            if (result == PasswordVerificationResult.Failed) 
            {
                return "";
            }



            await _userRepository.Update(user); 
            return "";
        }

    }
}
