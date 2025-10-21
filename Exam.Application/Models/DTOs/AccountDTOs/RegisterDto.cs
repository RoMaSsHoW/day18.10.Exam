namespace Exam.Application.Models.DTOs.AccountDTOs
{
    public class RegisterDto
    {
        public string UserName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
    }
}
