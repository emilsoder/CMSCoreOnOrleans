using CMSCore.Shared.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CMSCore.Identity.Extensions
{
    public class JwtTokenUtility
    {
        public static string CreateJwtToken(string normalizedUserName, string email, string[] roles)
        {
            var stringifiedRoles = roles != null ? string.Join(',', roles?.Select(x => x)) : "";
            return CreateUserSecurityToken(normalizedUserName, email, stringifiedRoles);
        }

        private static string CreateUserSecurityToken(string userId, string email, string roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(IdentityConst.JwtSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, email),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, roles)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}