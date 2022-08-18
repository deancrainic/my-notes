using AutoMapper;
using MyNotes.Application.DTOs.NoteDTOs;
using MyNotes.Domain.Entities;

namespace MyNotes.Application.Profiles;

public class NoteProfile : Profile
{
    public NoteProfile()
    {
        CreateMap<Note, GetNoteDto>().ReverseMap();
    }
}