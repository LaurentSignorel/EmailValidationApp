﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EmailValidation.Models;
using Newtonsoft.Json;

namespace EmailValidation.Helpers
{
    public class CustomEmailGeolocValidationAttribute : ValidationAttribute
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string _pattern;
        private readonly string _url;
        private readonly List<string> _validCountries;

        public CustomEmailGeolocValidationAttribute()
        {
            _pattern = ConfigurationManager.AppSettings.Get("regexPattern");
            _validCountries = ConfigurationManager.AppSettings.Get("validCountries").Split(';').ToList();
            _url = ConfigurationManager.AppSettings.Get("freegeoip");
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                if (value == null)
                {
                    return new ValidationResult("Veuillez saisir un email à valider.");
                }

                Regex reg = new Regex(_pattern);

                Match match = Regex.Match((string)value, _pattern);

                Group domain = match.Groups["domain"];
                Group extension = match.Groups["tld"];

                Task<bool> t = Task.Run(() => ShowThreadInfoAsync(domain.Value + extension.Value));
                t.Wait();

                if (!t.Result)
                {
                    return new ValidationResult("Le domaine n'est pas d'un pays limitrophe à la france ou n'est pas un domaine valide.");
                }

                return null;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return new ValidationResult("Une erreur interne est survenue, merci de recommencer.");
            }
        }

        private async Task<bool> ShowThreadInfoAsync(string fqdn)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(_url + fqdn);
                log.Info($"HTTP response from freegeoip : {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    string message = await response.Content.ReadAsStringAsync();
                    GeoIpModel geoIp = JsonConvert.DeserializeObject<GeoIpModel>(message);
                    string test = _validCountries.Find(c => c.Equals(geoIp.CountryCode));
                    if (!string.IsNullOrEmpty(test))
                        return true;
                }
                else
                {
                    log.Error($"{response.RequestMessage} - {response.ReasonPhrase}");
                }
            }
            return false;
        }
    }
}