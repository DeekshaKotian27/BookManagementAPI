using BookManagementAPI.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookManagementAPI.API.Helpers
{
    public static class TokenGenerator
    {
        public static string GetToken(Users userData, IConfigurationSection jwtSettings)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = jwtSettings["Key"];
            var key = Encoding.UTF8.GetBytes(secretKey);
            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name,userData.UserName),
                    new Claim(ClaimTypes.Email,userData.EmailID)
                }),
                Expires = DateTime.UtcNow.AddMinutes(100),
                Audience = jwtSettings["Audience"],
                Issuer = jwtSettings["Issuer"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescription);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}
