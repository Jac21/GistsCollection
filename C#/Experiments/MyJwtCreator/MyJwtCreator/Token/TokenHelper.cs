using MyJwtCreator.Models;
using MyJwtCreator.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;

namespace MyJwtCreator.Token
{
    public class TokenHelper
    {
        public static async Task<string> CreateJwtAsync(
            User user,
            UserData userData,
            string issuer,
            string authority,
            string symSec,
            int daysValid)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = await ClaimsHelper.CreateClaimsIdentityAsync(user, userData);

            // Crate JWToken
            // User Sha256 to digitally sign signature
            var token = tokenHandler.CreateJwtSecurityToken(issuer: issuer,
                audience: authority,
                subject: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(daysValid),
                signingCredentials:
                new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.Default.GetBytes(symSec)),
                    SecurityAlgorithms.HmacSha256Signature));

            return tokenHandler.WriteToken(token);
        }
    }
}