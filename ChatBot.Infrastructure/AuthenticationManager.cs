
using ChatBot.Core.Dtos;
using ChatBot.Core.Interfaces;
using ChatBot.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot.Infrastructure
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private  User _user;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthenticationManager(UserManager<User> userManager,
                                     IConfiguration configuration,
                                     RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigniningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        public async Task<bool> ValidateUser(UserForAuthenticationDto userDto)
        {
            _user = await _userManager.FindByNameAsync(userDto.UserName);
            
            return _user != null && await _userManager.CheckPasswordAsync(_user, userDto.Password);
        }

        private SigningCredentials GetSigniningCredentials()
        {
            var key = (Environment.GetEnvironmentVariable("SECRET"));
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key + key));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<IEnumerable<Claim>> GetClaims()
        {
            var roles = await _userManager.GetRolesAsync(_user);
            var userRoles = roles.Select(r => new Claim(ClaimTypes.Role, r)).ToArray();
            var userClaims = await _userManager.GetClaimsAsync(_user).ConfigureAwait(false);
            
            IList<Claim> roleClaims = new List<Claim>();

            foreach (var userRole in roles)
            {
                var role = await _roleManager.FindByNameAsync(userRole);
                IList<Claim> claims_ = await _roleManager.GetClaimsAsync(role).ConfigureAwait(false);
                foreach(var claim_ in claims_)
                {
                    roleClaims.Add(claim_);
                }
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, _user.Id.ToString()),
                new Claim(ClaimTypes.Email, _user.Email),
                new Claim(ClaimTypes.Name, _user.UserName)
            }.Union(userClaims).Union(roleClaims).Union(userRoles);
            
            
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials,
                                                      IEnumerable<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var tokenOptions = new JwtSecurityToken
            (
                issuer: jwtSettings.GetSection("validIssuer").Value,
                audience: jwtSettings.GetSection("validAudience").Value,
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(jwtSettings.GetSection("expires").Value))
            );

            return tokenOptions;
        }

    }
}
