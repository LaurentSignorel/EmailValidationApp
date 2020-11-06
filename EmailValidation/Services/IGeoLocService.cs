using Ninject.Extensions.Logging;

namespace EmailValidation.Services
{
    public interface IGeoLocService
    {
        ILogger Logger { get; set; }
        IConfigurationService Config { get; set; }
        bool CheckCountryCode(string fqdn);
    }
}
