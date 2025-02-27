﻿using AutoMapper;
using Dorm.Domain.DTO;
using Dorm.Domain.DTO.Auth;
using Dorm.Domain.DTO.Chat;
using Dorm.Domain.Entities.Ad;
using Dorm.Domain.Entities.Chat;
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
                .ForMember(dest => dest.UserType, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UserProfileDto, UserEF>()
                .ForMember(dest => dest.Email, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UserEF, UserProfileDto>();

            CreateMap<Ad, AdDto>();

            CreateMap<AdDto, Ad>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Chat, ChatDto>();

            CreateMap<ChatDto, Chat>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<ChatMessage, ChatMessageDto>();

            CreateMap<ChatMessageDto, ChatMessage>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Ticket, TicketDto>();

            CreateMap<TicketDto, Ticket>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Reservation, ReservationDto>();

            CreateMap<ReservationDto, Reservation>();
        }
    }
}
