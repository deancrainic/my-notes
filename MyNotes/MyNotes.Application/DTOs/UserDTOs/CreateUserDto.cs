using System.ComponentModel.DataAnnotations;

namespace MyNotes.Application.DTOs.UserDTOs
{
    public class CreateUserDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
