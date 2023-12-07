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

            CreateMap<DoctorDto, ApplicationUser>()
                .ForMember(
                    dest => dest.Specialization,
                    opt => opt.MapFrom(src => new Specialization { Title = src.Specialization })
                );
            CreateMap<ApplicationUser, DoctorDto>()
                .ForMember(
                    dest => dest.Specialization,
                    opt => opt.MapFrom(src => src.Specialization.Title)
                );
        }
    }
}
