using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyNotes.Domain.DTOs;
using MyNotes.Domain.Entities;
using MyNotes.Domain.Repositories;
using MyNotes.Domain.Services.Abstractions;

namespace MyNotes.Infrastructure.Services
{
    public class AuthManager : IAuthManager
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _uow;
        private User? _user;
        private UserManager<User> _userManager;
        
        public async Task<bool> ValidateUser(UserLoginDto userDto)
        {
            _user = await _uow.UserQueryRepository.GetUserByUserNameAsync(userDto.UserName);

            return await _userManager.CheckPasswordAsync(_user, userDto.Password);
        }
        
        public AuthManager(IConfiguration configuration, IUnitOfWork uow, UserManager<User> userManager)
        {
            _configuration = configuration;
            _uow = uow;
            _userManager = userManager;
        }

        public string CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = GetClaims();
            var token = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Guid GetUserIdFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);

            var userId = Guid.Parse(jwtSecurityToken.Claims.First(claim => claim.Type == "UserId").Value);

            return userId;
        }
        
        public bool CheckEmail(string email)
        {
            var mail = new MailAddress(email);
            return mail.Host.Contains(".");
        }

        public string GetErrors(IdentityResult result)
        {
            string errors = "";
            
            foreach (var err in result.Errors)
            {
                errors += err.Description + "\n";
            }

            return errors;
        }
        
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("Jwt");

            var token = new JwtSecurityToken(
                issuer: jwtSettings.GetSection("Issuer").Value,
                audience: jwtSettings.GetSection("Audience").Value,
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: signingCredentials
                );

            return token;
        }

        private List<Claim> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user.UserName),
                new Claim("UserId", _user.Id)
            };

            return claims;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = _configuration.GetSection("Jwt:Key").Value;
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
    }
}
