using System.Collections.Generic;

namespace EmailValidation.Services
{
    public interface IConfigurationService
    {
        string RegexPattern { get; set; }
        string GeoLocApiUrl { get; set; }
        List<string> AllowedCountries { get; set; }
    }
}