using Application.Dtos;
using AutoMapper;
using Core.Models;

namespace Application.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterDto, ApplicationUser>();
            CreateMap<ApplicationUser, RegisterDto>();
        }
    }
}
