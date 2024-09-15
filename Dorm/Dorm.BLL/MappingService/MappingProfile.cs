using AutoMapper;
using Dorm.Domain.DTO;
using Dorm.Domain.DTO.Auth;
using Dorm.Domain.DTO.Laundry;
using Dorm.Domain.Entities.Ad;
using Dorm.Domain.Entities.Laundry;
using Dorm.Domain.Entities.Ticket;
using Dorm.Domain.Entities.UserEF;

namespace Dorm.BLL.MappingService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegistrationDto, UserEF>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.UserStatus, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<LoginDto, UserEF>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.UserStatus, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());


            CreateMap<Ad, AdDto>();

            CreateMap<AdDto, Ad>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());


            CreateMap<Ticket, TicketDto>();

            CreateMap<TicketDto, Ticket>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Reservation, ReservationDto>();

            CreateMap<ReservationDto, Reservation>();
        }
    }
}
