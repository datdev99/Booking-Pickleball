using AutoMapper;
using BookingApi.Migrations.AuthDb;
using BookingApi.Models.Domain;
using BookingApi.Models.DTO.NewDto;
using BookingApi.Models.DTO.UserDto;

namespace BookingApi.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<News, GetNewDto>().ReverseMap();
            CreateMap<AddNewRequestDto, News>().ReverseMap();
            CreateMap<UpdateNewRequestDto, News>().ReverseMap();

            CreateMap<GetUserDto, RegisterRequestDto>().ReverseMap();
        }
    }
}
