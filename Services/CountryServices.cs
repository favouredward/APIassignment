using APIassignment.Models;
using System.Text.Json;

namespace APIassignment.Services
{
    // Service class that communicates with the external RESTCountries API
    public class CountryService : ICountryService
    {
        //private variable to send and receive HTTP Requests to APIs
        private readonly HttpClient _httpClient;

        // HttpClient is injected through constructor
        public CountryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CountryModel>> GetAllCountriesAsync()
        {
            var response = await _httpClient.GetAsync("https://restcountries.com/v3.1/all?fields=name,region,currencies,capital,population");

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException("Failed to fetch countries");

            var stream = await response.Content.ReadAsStreamAsync();
            var countries = await JsonSerializer.DeserializeAsync<List<CountryModel>>(stream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return countries ?? new List<CountryModel>();
        }

        public async Task<List<CountryModel>> GetByRegionAsync(string region)
        {
            var response = await _httpClient.GetAsync($"https://restcountries.com/v3.1/region/{region}");

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException("Region not found");

            var content = await response.Content.ReadAsStringAsync();
            var countries = JsonSerializer.Deserialize<List<CountryModel>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return countries ?? new List<CountryModel>();
        }

        public async Task<List<CountryModel>> GetByCapitalAsync(string capital)
        {
            var response = await _httpClient.GetAsync($"https://restcountries.com/v3.1/capital/{capital}");

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException("Capital not found");

            var content = await response.Content.ReadAsStringAsync();
            var countries = JsonSerializer.Deserialize<List<CountryModel>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return countries ?? new List<CountryModel>();
        }
         
        public async Task<List<CountryModel>> GetByNameAsync(string name)
        {
            var response = await _httpClient.GetAsync($"https://restcountries.com/v3.1/name/{name}");

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException("Country not found");

            var content = await response.Content.ReadAsStringAsync();
            var countries = JsonSerializer.Deserialize<List<CountryModel>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return countries ?? new List<CountryModel>();
        }

        //public async Task<List<CountryModel>?> GetCountryByNameAsync(string name)
        //{
        //    var response = await _httpClient.GetAsync($"https://restcountries.com/v3.1/name/{name}");
        //    if (!response.IsSuccessStatusCode)
        //        return null;
        //    var content = await response.Content.ReadAsStringAsync();
        //    var countries = JsonSerializer.Deserialize<List<CountryModel>>(content, new JsonSerializerOptions
        //    {
        //        PropertyNameCaseInsensitive = true
        //    });
        //    return countries;
        //}

        // Method to get countries by name using external API
        public async Task<List<CountryModel>?> GetCountryByNameAsync(string name)
        {
            // Form the endpoint URL using the provided country name
            var response = await _httpClient.GetAsync($"https://restcountries.com/v3.1/name/{name}");

            // Return null if not successful
            if (!response.IsSuccessStatusCode)
                return null;

            var stream = await response.Content.ReadAsStreamAsync();

            // Deserialize the response JSON into a list of CountryModel objects
            var countries = await JsonSerializer.DeserializeAsync<List<CountryModel>>(stream,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return countries;
        }
    }
}
