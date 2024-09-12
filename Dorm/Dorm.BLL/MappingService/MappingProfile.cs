using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dorm.Domain.DTO;
using Dorm.Domain.Entities.Ad;
using Dorm.Domain.Entities.User;
using Dorm.Domain.Models;

namespace Dorm.BLL.MappingService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegistrationModel, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.UserStatus, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<LoginModel, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.UserStatus, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Ad, AdDto>();

            CreateMap<AdDto, Ad>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
