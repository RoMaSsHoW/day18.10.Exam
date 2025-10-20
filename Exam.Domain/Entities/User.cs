using Exam.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Exam.Domain.Entities
{
    public class User : IdentityUser<int>
    {
        public string Address { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; }
        public UserRole Role { get; set; }
    }
}
