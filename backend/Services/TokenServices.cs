using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace backend.Services
{
    public class TokenServices : ITokenServices
    {

        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        private readonly UserManager<Usuario> _userManager;

        public TokenServices(IConfiguration config, UserManager<Usuario> userManager)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecretKey"]!));
            _userManager = userManager;
        }


        public async Task<string> CreateToken(Usuario usuario)
        {

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti,usuario.Id),
                new Claim(JwtRegisteredClaimNames.Sub, usuario.UserName!),
            };
            var userRoles = await _userManager.GetRolesAsync(usuario);
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var creds = new SigningCredentials(_key, SecurityAlgorithms.Aes256CbcHmacSha512);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}