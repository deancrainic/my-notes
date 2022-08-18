using AutoMapper;
using MyNotes.Application.DTOs.UserDTOs;
using MyNotes.Domain.Entities;

namespace MyNotes.Application.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, GetUserDto>()
            .ForMember(
                u => u.UserName, 
                opt => opt.MapFrom(s => s.UserName))
            .ForMember(
                u => u.Email,
                opt => opt.MapFrom(s => s.Email))
            .ReverseMap();
    }
}