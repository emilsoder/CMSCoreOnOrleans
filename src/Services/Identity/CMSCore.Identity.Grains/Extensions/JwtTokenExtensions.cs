using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CMSCore.Identity.Models;
using CMSCore.Shared.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CMSCore.Identity.Grains.Extensions
{
    public static class JwtTokenExtensions
    {
        public static string CreateJwtToken(this ApplicationUser user, IList<string> roles)
        {
            var stringifiedRoles = roles.ArrayToCommaSeparatedString();
            return user.CreateJwtToken(stringifiedRoles);
        }

        public static string CreateJwtToken(this ApplicationUser user, string roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(IdentityConst.JwtSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.NormalizedUserName),
                    new Claim(ClaimTypes.Email, user.Email),
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