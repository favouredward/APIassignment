namespace APIassignment.Models
{
    public class CountryModel
    {
        public Name Name { get; set; }               // Nested object
        public string[] Capital { get; set; }        // Array of strings
        public string Region { get; set; }           // e.g. Europe, Asia
        public Dictionary<string, Currency> Currencies { get; set; } // Dictionary of currency codes
        public long Population { get; set; }         // Total population
    }


    public class Name
    {
        public string Common { get; set; }           // Common name of the country
        public string Official { get; set; }         // Official name
    }

    public class Currency
    {
        public string Name { get; set; }             // e.g. "Naira"
        public string Symbol { get; set; }           // e.g. "₦"
    }

    // This model represents the JSON payload for a POST request to get country info by name
    public class CountryRequest
    {
        public string Name { get; set; } = string.Empty; // The country name (e.g., "Canada")
    }
}
