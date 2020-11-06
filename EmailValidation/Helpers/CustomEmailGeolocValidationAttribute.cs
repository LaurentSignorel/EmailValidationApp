using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using EmailValidation.Models;
using EmailValidation.Services;
using Newtonsoft.Json;
using Ninject;
using Ninject.Extensions.Logging;

namespace EmailValidation.Helpers
{
    /// <summary>
    /// Attribut de validation du domaine d'un email.
    /// Afin d'être valide, le domaine du mail doit être un domaine valide et être hébergé en france ou dans un des pays limitrophes de la france.
    /// </summary>
    public class CustomEmailGeolocValidationAttribute : ValidationAttribute
    {
        [Inject]
        public ILogger Logger { get; set; }
        [Inject]
        public IGeoLocService GeoLocService { get; set; }
        [Inject]
        public IConfigurationService ConfigurationService { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {            
            try
            {
                if (value == null)
                {
                    return new ValidationResult("Veuillez saisir un email à valider.");
                }
                // utilisation de la regex de validation, afin de découper par groupes le domaine et l'extension
                Match match = Regex.Match((string)value, ConfigurationService.RegexPattern);
                Group domain = match.Groups["domain"];
                Group extension = match.Groups["tld"];
                bool result = GeoLocService.CheckCountryCode(domain.Value + extension.Value);

                if (!result)
                {
                    return new ValidationResult("Le domaine n'est pas d'un pays limitrophe à la france ou n'est pas un domaine valide.");
                }
                return null;
            }
            catch (Exception ex)
            {
                if (Logger != null)
                {
                    Logger.ErrorException(ex.Message, ex);
                }
                return new ValidationResult("Une erreur interne est survenue, merci de recommencer.");
            }
        }
    }
}