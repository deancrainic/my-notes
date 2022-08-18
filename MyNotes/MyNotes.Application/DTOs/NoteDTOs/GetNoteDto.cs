namespace MyNotes.Application.DTOs.NoteDTOs;

public class GetNoteDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public DateTime LastEdited { get; set; }
}