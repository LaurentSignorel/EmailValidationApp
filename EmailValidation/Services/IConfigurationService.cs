using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailValidation.Services
{
    public interface IConfigurationService
    {
        string RegexPattern { get; set; }
        string GeoLocApiUrl { get; set; }
        List<string> AllowedCountries { get; set; }
    }
}