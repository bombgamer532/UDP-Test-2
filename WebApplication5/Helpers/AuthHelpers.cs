using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication5.Models;

namespace WebApplication5.Helpers
{
    public static class AuthHelpers
    {
        public static string GenerateJWTToken(string login, string password)
        {
            var claims = new List<Claim> {
                new Claim("login", login),
                new Claim("password", password),
            };
            var jwtToken = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                       Encoding.ASCII.GetBytes(
                           new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Jwt")["Key"]
                           )
                    ),
                    SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
