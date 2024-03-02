using Newtonsoft.Json;
using System.Text.Json.Serialization;
using WelcomeHome.Services.DTO.VacancyDTO;
using WelcomeHome.Services.Exceptions;

namespace WelcomeHome.Services.ServiceClients.RobotaUa;

public sealed class RobotaUaServiceClient : IRobotaUaServiceClient, IDisposable
{
    private readonly HttpClient _httpClient;
    private bool _disposed;

    public RobotaUaServiceClient()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://api.rabota.ua");
    }

    public async Task<IEnumerable<VacancyDTO>> GetAllVacanciesAsync(PaginationOptionsDTO paginationOptions)
    {
        var getResponse = await _httpClient.GetAsync($"/vacancy/search?page={paginationOptions.PageNumber}" +
                                                              $"&count={paginationOptions.CountOnPage}&ukrainian=true&cityId=1")
                                           .ConfigureAwait(false);
        if (!getResponse.IsSuccessStatusCode)
        {
            throw new BusinessException("Robota ua service do not work properly");
        }

        var jsonResponse = await getResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
        dynamic deserializedResponse = JsonConvert.DeserializeObject(jsonResponse)!;

        List<VacancyDTO> vacancies = new List<VacancyDTO>();
        foreach (dynamic document in deserializedResponse.documents)
        {
            vacancies.Add(new VacancyDTO()
            {
                Id = document.id,
                Name = document.name,
                CompanyName = document.companyName,
                Description = document.shortDescription,
                Salary = document.salary,
                SalaryFrom = document.salaryFrom,
                SalaryTo = document.salaryTo,
                PageURL = $"https://robota.ua/company0/vacancy{document.id}",
                CityId = document.cityId,
                CityName = document.cityName,
                MetroName = document.metroName,
                DistrictName = document.districtName,
                FromRobotaUa = true,
            });
        }
        
        return vacancies;
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            Dispose(true);
            _disposed = true;
        }
    }

    private void Dispose(bool dispose)
    {
        if (dispose == true)
        {
            _httpClient.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
