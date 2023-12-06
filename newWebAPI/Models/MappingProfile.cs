using newWebAPI;
using newWebAPI.Models;
using newWebAPI.Models.DTOs;
using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Book, BookUpdateDTO>().ReverseMap();
    }
}