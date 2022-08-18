using System.ComponentModel.DataAnnotations;

namespace MyNotes.Application.DTOs.UserDTOs;

public class ChangeUserPasswordDto
{
    [Required]
    public string Password { get; set; }
}