using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.DAL.Models;
using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Establishment, EstablishmentOutDTO>();
            CreateMap<EstablishmentInDTO, Establishment>();
            CreateMap<EstablishmentOutDTO, Establishment>();
			CreateMap<EstablishmentType, EstablishmentTypeOutDTO>();
            CreateMap<Document, DocumentOutDTO>();
            CreateMap<DocumentInDTO, Document>();
            CreateMap<DocumentOutDTO, Document>();
			CreateMap<EventInDTO, Event>();
            CreateMap<Event, EventOutDTO>();
            CreateMap<EventOutDTO, Event>();
            CreateMap<UserInDTO, User>();
            CreateMap<User, UserOutDTO>();
            CreateMap<UserOutDTO, User>();
            CreateMap<VolunteerInDTO, Volunteer>();
            CreateMap<Volunteer, VolunteerOutDTO>();
            CreateMap<VolunteerOutDTO, Volunteer>();
        }
    }
}
