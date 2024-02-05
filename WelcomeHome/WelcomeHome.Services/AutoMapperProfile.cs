using AutoMapper;
using WelcomeHome.DAL.Dto;
using WelcomeHome.DAL.Models;
using WelcomeHome.Services.DTO;
using WelcomeHome.Services.DTO.EstablishmentDTO;
using WelcomeHome.Services.DTO.EventDto;

namespace WelcomeHome.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Establishment, EstablishmentFullInfoDTO>();
            CreateMap<EstablishmentInDTO, Establishment>();
            CreateMap<EstablishmentFullInfoDTO, Establishment>();
            CreateMap<EstablishmentType, EstablishmentTypeOutDTO>();
            CreateMap<Document, DocumentOutDTO>();
            CreateMap<DocumentInDTO, Document>();
            CreateMap<DocumentOutDTO, Document>();
            CreateMap<EventInDTO, Event>();
            CreateMap<Event, EventFullInfoDTO>();
            CreateMap<EventFullInfoDTO, Event>();
            CreateMap<VolunteerRegisterDTO, Volunteer>();
            CreateMap<Volunteer, VolunteerOutDTO>();
            CreateMap<VolunteerOutDTO, Volunteer>();
            CreateMap<VolunteerRegisterDTO, UserRegisterDTO>();
            CreateMap<StepInDTO, Step>();
            CreateMap<Step, StepOutDTO>();
            CreateMap<Country, CountryOutDTO>();
            CreateMap<City, CityOutDTO>();
            CreateMap<UserRegisterDTO, User>();
            CreateMap<User, UserOutDTO>();
            CreateMap<UserCategory, UserCategoryOutDTO>();
            CreateMap<SocialPayout, SocialPayoutListItemDTO>();
            CreateMap<SocialPayout, SocialPayoutOutDTO>();
            CreateMap<EstablishmentFiltersDto, EstablishmentRetrievalFiltersDto>();
        }
    }
}
