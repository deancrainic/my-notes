using Microsoft.AspNetCore.Identity;
using MyNotes.Domain.DTOs;
using MyNotes.Domain.Entities;

namespace MyNotes.Domain.Services.Abstractions
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(UserLoginDto userDto);
        string CreateToken();
        Guid GetUserIdFromToken(string token);
        bool CheckEmail(string email);
        string GetErrors(IdentityResult result);
    }
}
