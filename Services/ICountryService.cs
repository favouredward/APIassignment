using APIassignment.Models;

namespace APIassignment.Services
{
    // Interface defining the methods to interact with the country API
    public interface ICountryService
    {
        Task<List<CountryModel>> GetAllCountriesAsync();
        Task<List<CountryModel>> GetByRegionAsync(string region);
        Task<List<CountryModel>> GetByCapitalAsync(string capital);
        Task<List<CountryModel>> GetByNameAsync(string name);
        Task<List<CountryModel>?> GetCountryByNameAsync(string name);
    }
}
