using EmailValidation.Models;
using Newtonsoft.Json;
using Ninject.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmailValidation.Services
{
    public class GeoLocService : IGeoLocService
    {
        public ILogger Logger { get; set; }
        public IConfigurationService Config { get; set; }

        public GeoLocService(ILogger logger, IConfigurationService config)
        {
            Logger = logger;
            Config = config;
        }

        public bool CheckCountryCode(string fqdn)
        {
            Task<bool> t = Task.Run(() => CheckCountryCodeAsync(fqdn));
            t.Wait();
            return t.Result;
        }

        private async Task<bool> CheckCountryCodeAsync(string fqdn)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(Config.GeoLocApiUrl + fqdn);
                Logger.Info($"HTTP response from freegeoip : {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    string message = await response.Content.ReadAsStringAsync();
                    GeoIpModel geoIp = JsonConvert.DeserializeObject<GeoIpModel>(message);
                    if (!string.IsNullOrEmpty(Config.AllowedCountries.Find(c => c.Equals(geoIp.CountryCode))))
                    {
                        return true;
                    }
                }
                else
                {
                    Logger.Error($"{response.RequestMessage} - {response.ReasonPhrase}");
                }
            }
            return false;
        }
    }
}