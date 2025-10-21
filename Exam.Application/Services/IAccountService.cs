using Exam.Application.Models.DTOs.AccountDTOs;

namespace Exam.Application.Services
{
    public interface IAccountService
    {
        public Task<string> Register(RegisterDto model);
        public Task<string> Login(LoginDto login);
    }
}
