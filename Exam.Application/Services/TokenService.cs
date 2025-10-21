using Exam.Application.Models;
using Exam.Application.Permissions;
using Exam.Domain.Entities;
using Exam.Domain.Enums;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Exam.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly JWTSettings _jwtSettings;
        public TokenService(IOptions<JWTSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public string GenerateAccessToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
            };

            var permissionClaims = GetPermissions(user)
                .Select(permission => new Claim("Permissions", permission));

            claims.AddRange(permissionClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_jwtSettings.ExpireTime),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private IEnumerable<string> GetPermissions(User user)
        {
            var permissions = new List<string>();

            if (user.Role == UserRole.Admin)
            {
                permissions.Add(PermissionConstants.Restaurants.View);
                permissions.Add(PermissionConstants.Restaurants.Manage);
                permissions.Add(PermissionConstants.Menu.View);
                permissions.Add(PermissionConstants.Menu.Manage);
                permissions.Add(PermissionConstants.Orders.View);
                permissions.Add(PermissionConstants.Orders.Create);
                permissions.Add(PermissionConstants.Orders.Manage);
                permissions.Add(PermissionConstants.Couriers.View);
                permissions.Add(PermissionConstants.Couriers.Manage);
            }
            else if (user.Role == UserRole.Client)
            {
                permissions.Add(PermissionConstants.Restaurants.View);
                permissions.Add(PermissionConstants.Menu.View);
                permissions.Add(PermissionConstants.Orders.View);
                permissions.Add(PermissionConstants.Orders.Create);
            }
            else if (user.Role == UserRole.Courier)
            {
                permissions.Add(PermissionConstants.Orders.View);
                permissions.Add(PermissionConstants.Orders.Manage);
                permissions.Add(PermissionConstants.Couriers.View);
            }
            else
            {
                throw new InvalidOperationException("Unknown user role.");
            }

            return permissions;
        }
    }
}
