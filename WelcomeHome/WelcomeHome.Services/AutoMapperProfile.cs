using AutoMapper;
using WelcomeHome.DAL.Models;
using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services
{
    public class AutoMapperProfile : Profile
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
            CreateMap<VolunteerRegisterDTO, Volunteer>();
            CreateMap<Volunteer, VolunteerOutDTO>();
            CreateMap<VolunteerOutDTO, Volunteer>();
            CreateMap<VolunteerRegisterDTO, UserRegisterDTO>();
            CreateMap<StepInDTO, Step>();
            CreateMap<Step, StepOutDTO>();
            CreateMap<Country, CountryOutDto>();
            CreateMap<City, CityOutDto>();
            CreateMap<UserRegisterDTO, User>();
            CreateMap<User, UserOutDTO>();
        }
    }
}
