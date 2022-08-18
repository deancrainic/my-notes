using System.ComponentModel.DataAnnotations;

namespace MyNotes.Application.DTOs.UserDTOs;

public class UpdateUserDto
{
    [Required]
    public string UserName { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
}