using AutoMapper;
using Dorm.BLL.Interfaces;
using Dorm.DAL.Interfaces;
using Dorm.Domain.DTO.Auth;
using Dorm.Domain.Entities.UserEF;
using Dorm.Domain.Responses;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Dorm.BLL.Services;

/// <summary>
/// Service for handling authentication-related operations.
/// </summary>
public class AuthService : IAuthService
{
    private readonly IMapper _mapper;
    private readonly JwtService _jwtService;
    private readonly IUsersRepository<UserEF> _userRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthService"/> class.
    /// </summary>
    /// <param name="userRepository">The user repository.</param>
    /// <param name="mapper">The mapper.</param>
    /// <param name="jwtService">The JWT service.</param>
    public AuthService(IUsersRepository<UserEF> userRepository, IMapper mapper, JwtService jwtService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _jwtService = jwtService;
    }

    /// <summary>
    /// Logs in a user.
    /// </summary>
    /// <param name="loginDto">The login DTO.</param>
    /// <returns>The authentication response.</returns>
    /// <exception cref="ArgumentException">Thrown when email or password is null.</exception>
    public async Task<AuthResponse> LoginUser(LoginDto loginDto)
    {
        if (loginDto == null)
            return new AuthResponse(
                false,
                "Login Model is null"
            );

        var user = await _userRepository.GetByEmail(
            loginDto.Email ?? throw new ArgumentException(
                "Email cannot be null")
        );

        if (user == null)
            return new AuthResponse(
                false,
                "User with such email, hasn't been found."
            );

        var result = new PasswordHasher<UserEF>().VerifyHashedPassword(
            user,
            user.PasswordHash ?? throw new ArgumentException(
                "PasswordHash from bd is empty"),
            loginDto.Password ?? throw new ArgumentException(
                "Password cant be empty")
        );


        if (result == PasswordVerificationResult.Failed)
            return new AuthResponse(
                false,
                "Password is incorrect."
            );

        var token = _jwtService.GetToken(user);

        return new AuthResponse(true, null, user.UserType.ToString(),  token);
    }

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="registrationDto">The registration DTO.</param>
    /// <returns>The authentication response.</returns>
    /// <exception cref="ArgumentNullException">Thrown when email or password is null.</exception>
    public async Task<AuthResponse> RegisterUser(RegistrationDto registrationDto)
    {
        if (registrationDto == null)
            return new AuthResponse(
                false,
                "Registration Model is null"
            );

        var userFromBd = await _userRepository.GetByEmail(
            registrationDto.Email ?? throw new ArgumentNullException(
                nameof(registrationDto.Email),
                "Email cannot be null"
            )
        );

        if (userFromBd != null)
            return new AuthResponse(
                false,
                "User with such email, hasn't been found."
            );

        var user = _mapper.Map<UserEF>(registrationDto);

        user.PasswordHash = new PasswordHasher<UserEF>().HashPassword(
            user, registrationDto.Password ?? throw new ArgumentNullException(
                nameof(registrationDto.Password),
                "Cant be empty pass"
            )
        );

        user.UserType = Domain.Enum.User.UserType.Student;

        await _userRepository.Create(user);

        var token = _jwtService.GetToken(user);

        return new AuthResponse(true, "", user.UserType.ToString(), token);
    }

    /// <summary>
    /// Validates the authentication model.
    /// </summary>
    /// <param name="model">The model to validate.</param>
    /// <returns>The validation response.</returns>
    public async Task<ValidationResponse> AuthValidation(object model)
    {
        // List of validation results
        var validationResults = new List<ValidationResult>();

        // Context defines the objects of the model to validate
        var context = new ValidationContext(model);

        // Validates the model
        var isValid = ValidateModel(validationResults, model, context);

        // Grouping validation results by property name
        var errors = validationResults
            .GroupBy(validationResult => validationResult.MemberNames.FirstOrDefault() ?? "General")
            .ToDictionary(g => g.Key, g => g.Select(vr => vr.ErrorMessage).ToList());

        return await Task.FromResult(new ValidationResponse(isValid, errors));
    }

    /// <summary>
    /// Validates the model.
    /// </summary>
    /// <param name="validationResults">The list of validation results.</param>
    /// <param name="model">The model to validate.</param>
    /// <param name="context">The validation context.</param>
    /// <returns>True if the model is valid; otherwise, false.</returns>
    /// <exception cref="ArgumentException">Thrown when password is null.</exception>
    private bool ValidateModel(List<ValidationResult> validationResults, object model, ValidationContext context)
    {
        var isValid = Validator.TryValidateObject(model, context, validationResults, true);

        if (model is LoginDto loginDto)
            ValidatePassword(loginDto.Password ?? throw new ArgumentException("Password can't be empty"),
                validationResults);
        else if (model is RegistrationDto registrationDto)
            ValidatePassword(registrationDto.Password ?? throw new ArgumentException("Password can't be empty"),
                validationResults);

        return isValid;
    }

    /// <summary>
    /// Validates the password, checking if it meets the regular requirements.
    /// If the password is invalid, adds an error message to the list of validation results.
    /// </summary>
    /// <param name="password">The password to validate.</param>
    /// <param name="results">The list of validation results.</param>
    private void ValidatePassword(string password, List<ValidationResult> results)
    {
        if (password.Length < 8 || !Regex.IsMatch(password, @"^[A-Za-z\d@$!%*?&]+$"))
            results.Add(new ValidationResult(
                "Password must be at least 8 characters long, contain only English letters, digits, and special characters.",
                new[] { nameof(password) }));

        if (!Regex.IsMatch(password, @"[A-Z]"))
            results.Add(new ValidationResult(
                "Password must contain at least 1 uppercase letter.",
                new[] { nameof(password) }));

        if (!Regex.IsMatch(password, @"\d"))
            results.Add(new ValidationResult(
                "Password must contain at least 1 digit.",
                new[] { nameof(password) }));

        if (!Regex.IsMatch(password, @"[@$!%*?&]"))
            results.Add(new ValidationResult(
                "Password must contain at least 1 special character.",
                new[] { nameof(password) }));
    }
}