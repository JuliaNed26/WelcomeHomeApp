using AutoMapper;
using WelcomeHome.DAL.Dto;
using WelcomeHome.DAL.Models;
using WelcomeHome.Services.DTO;
using WelcomeHome.Services.DTO.EstablishmentDTO;
using WelcomeHome.Services.DTO.EventDto;
using WelcomeHome.Services.DTO.VacancyDTO;

namespace WelcomeHome.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Establishment, EstablishmentFullInfoDTO>();
            CreateMap<EstablishmentInDTO, Establishment>();
            CreateMap<EstablishmentFullInfoDTO, Establishment>();
            CreateMap<EstablishmentVolunteerInDTO, Establishment>();
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
            CreateMap<EstablishmentVolunteerInDTO, Establishment>();

            CreateMap<PaginationOptionsDTO, PaginationOptionsDto>();

            CreateMap<VacancyWithTotalPagesCount, VacancyDTO>()
            .ForMember(vacancy => vacancy.FromRobotaUa, opt => opt.MapFrom(_ => false))
            .ForMember(vacancy => vacancy.SalaryFrom, opt => opt.Ignore())
            .ForMember(vacancy => vacancy.SalaryTo, opt => opt.Ignore())
            .ForMember(vacancy => vacancy.MetroName, opt => opt.Ignore())
            .ForMember(vacancy => vacancy.DistrictName, opt => opt.Ignore());

            CreateMap<Vacancy, VacancyDTO>()
            .ForMember(vacancy => vacancy.CityName, opt => opt.MapFrom(src => src.City.Name))
            .ForMember(vacancy => vacancy.FromRobotaUa, opt => opt.MapFrom(_ => false))
            .ForMember(vacancy => vacancy.SalaryFrom, opt => opt.Ignore())
            .ForMember(vacancy => vacancy.SalaryTo, opt => opt.Ignore())
            .ForMember(vacancy => vacancy.MetroName, opt => opt.Ignore())
            .ForMember(vacancy => vacancy.DistrictName, opt => opt.Ignore());

            CreateMap<VacancyAddUpdateDTO, Vacancy>();
        }
    }
}
