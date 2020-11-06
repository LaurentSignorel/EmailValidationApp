using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace EmailValidation.Services
{
    public class ConfigurationService : IConfigurationService
    {
        public string RegexPattern { get; set; }
        public string GeoLocApiUrl { get; set; }
        public List<string> AllowedCountries { get; set; }

        public ConfigurationService()
        {
            RegexPattern = ConfigurationManager.AppSettings.Get("regexPattern");
            GeoLocApiUrl = ConfigurationManager.AppSettings.Get("freegeoip");
            AllowedCountries = ConfigurationManager.AppSettings.Get("validCountries").Split(';').ToList();
        }
    }
}