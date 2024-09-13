﻿using AutoMapper;
using Dorm.BLL.Interfaces;
using Dorm.DAL.Interfaces;
using Dorm.Domain.DTO.Auth;
using Dorm.Domain.Entities.UserEF;
using Dorm.Domain.Responses;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Dorm.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsersRepository<UserEF> _userRepository;
        private readonly IMapper _mapper;
        private readonly JwtService _jwtService;
        public AuthService(IUsersRepository<UserEF> userRepository, IMapper mapper, JwtService jwtService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        public async Task<AuthResponse> LoginUser(LoginDto loginDto)
        {
            if (loginDto == null) return new AuthResponse(false,
                            "Login Model is null", null);

            var user = await _userRepository.GetByEmail(loginDto.Email ?? throw new ArgumentException("Email cannot be null"));

            if (user == null) return new AuthResponse(false,
                            "User with such email, hasn't been found.", null);

            var result = new PasswordHasher<UserEF>()
                            .VerifyHashedPassword(user, user.PasswordHash ?? 
                                    throw new ArgumentException("PasswordHash from bd is empty"), 
                            loginDto.Password ?? throw new ArgumentException("Password cant be empty"));


            if (result == PasswordVerificationResult.Failed)
            {
                return new AuthResponse(false, "Password is incorrect.", null);
            }
            //await _userRepository.Update(user);

            string token = _jwtService.GetToken(user);

            return new AuthResponse(true, null, token);
        }

        public async Task<AuthResponse> RegisterUser(RegistrationDto registrationDto)
        {
            if (registrationDto == null) return new AuthResponse(false,
                "Registration Model is null", null);

            var userFromBd = await _userRepository.GetByEmail(registrationDto.Email ??
                            throw new ArgumentNullException("Email", "Email cannot be null"));

            if (userFromBd == null) return new AuthResponse(false,
                            "User with such email, hasn't been found.", null);

            var user = _mapper.Map<UserEF>(registrationDto);

            user.PasswordHash = new PasswordHasher<UserEF>().HashPassword(user, registrationDto.Password ?? throw new ArgumentException("Cant be empty pass"));

            await _userRepository.Create(user);

            string token = _jwtService.GetToken(user);

            return (new AuthResponse(true, "", "token"));
        }

        public async Task<ValidationResponse> AuthValidation(object model)
        {
            // Создание списка ошибок
            var validationResults = new List<ValidationResult>();
            // Контекст определяет поля, которые подлежат проверке
            var context = new ValidationContext(model);

            // Валидация
            bool isValid = ValidateModel(validationResults, model, context);

            // Группировка ошибок
            var errors = validationResults
                .GroupBy(validationResult => validationResult.MemberNames.FirstOrDefault() ?? "General")
                .ToDictionary(g => g.Key, g => g.Select(vr => vr.ErrorMessage).ToList());

            return await Task.FromResult(new ValidationResponse(isValid, errors));
        }

        private bool ValidateModel(List<ValidationResult> validationResults, object model, ValidationContext context)
        {
            //Валидация
            bool isValid = Validator.TryValidateObject(model, context, validationResults, true);

            // Дополнительная валидация
            if (model is LoginDto loginDto)
            {
                ValidatePassword(loginDto.Password?? throw new ArgumentException("Password cant be empty"), validationResults);
            }
            else if (model is RegistrationDto registrationDto)
            {
                ValidatePassword(registrationDto.Password ?? throw new ArgumentException("Password cant be empty"), validationResults);
            }

            return isValid;
        }

        private void ValidatePassword(string password, List<ValidationResult> results)
        {
            // Проверка длины пароля и допустимых символов
            if (password.Length < 8 || !Regex.IsMatch(password, @"^[A-Za-z\d@$!%*?&]+$"))
            {
                results.Add(new ValidationResult("Password must be at least 8 characters long and contain only English letters, digits, and special characters.", new[] { nameof(password) }));
            }

            // Проверка наличия прописных букв
            if (!Regex.IsMatch(password, @"[A-Z]"))
            {
                results.Add(new ValidationResult("Password must contain at least 1 uppercase letter.", new[] { nameof(password) }));
            }

            // Проверка наличия цифр
            if (!Regex.IsMatch(password, @"\d"))
            {
                results.Add(new ValidationResult("Password must contain at least 1 digit.", new[] { nameof(password) }));
            }

            // Проверка наличия специальных символов
            if (!Regex.IsMatch(password, @"[@$!%*?&]"))
            {
                results.Add(new ValidationResult("Password must contain at least 1 special character.", new[] { nameof(password) }));
            }
        }
    }
}