using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MyNotes.Domain.Entities
{
    [Index(nameof(UserId), Name = "Index_User", IsUnique = false)]
    public class Note
    {
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(3000)]
        public string Text { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid UserId { get; set; }

        public Note()
        {
            DateCreated = DateTime.Now;
        }
    }
}
