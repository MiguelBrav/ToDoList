using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Domain.Interfaces;
using ToDoList.DTO.ApiResponse;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace ToDoList.Domain.Helper
{
    public class TokenService : ITokenService
    {
        public async Task<ResponseAuth> GenerateToken(IdentityUser user, string email, IList<Claim> claimsBD,string key)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", email),
                new Claim("userId", user.Id)
            };

            claims.AddRange(claimsBD);

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddMinutes(30);

            var securityToken = new JwtSecurityToken(issuer: null,
                audience: null, claims: claims, expires: expiracion,
                signingCredentials: creds);

            return new ResponseAuth()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                ExpirationDate = expiracion
            };
        }
    }
}
