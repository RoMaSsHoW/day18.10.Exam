using Exam.Application.Models.DTOs.AccountDTOs;
using Exam.Domain.Entities;
using Exam.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Exam.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;

        public AccountService(UserManager<User> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<string> Register(RegisterDto model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser is not null)
                throw new Exception("Пользователь с таким email уже существует");

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                Role = UserRole.Client,
                RegistrationDate = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new Exception($"Ошибка при создании пользователя: {errors}");
            }

            try
            {
                var accessToken = _tokenService.GenerateAccessToken(user);
                return accessToken;
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при генерации токена доступа: " + ex.Message);
            }
        }

        public async Task<string> Login(LoginDto login)
        {
            var existingUser = await _userManager.FindByEmailAsync(login.Email);
            if (existingUser is null)
                throw new Exception("Пользователь с таким email не найден");

            if (!await _userManager.CheckPasswordAsync(existingUser, login.Password))
                throw new Exception("Неверный пароль");

            try
            {
                var accessToken = _tokenService.GenerateAccessToken(existingUser);
                return accessToken;
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при генерации токена доступа: " + ex.Message);
            }
        }
    }
}
