using System.ComponentModel.DataAnnotations;

namespace MyNotes.Application.DTOs.NoteDTOs
{
    public class CreateNoteDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
