using Exam.Domain.Entities;

namespace Exam.Application.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
    }
}
