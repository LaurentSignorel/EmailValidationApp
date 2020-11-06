using EmailValidation.Services;
using Ninject;
using Ninject.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Text.RegularExpressions;

namespace EmailValidation.Helpers
{
    /// <summary>
    /// Attribut de validation de l'Email via Expression Régulière.
    /// Afin d'être valide, le mail doit respecter plusieurs critères syntaxiques :
    /// 1) l'email doit répondre à la syntaxe suivante : identifiant@[sous-domaines.]domaine.extension 
    /// 2) l'identifiant de l'email doit commencer par une lettre
    /// 3) il ne doit pas y avoir plus de 3 sous-domaines
    /// </summary>
    public class CustomEmailRegexValidationAttribute : ValidationAttribute
    {
        [Inject]
        public ILogger Logger { get; set; }
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

                return Regex.IsMatch((string)value, ConfigurationService.RegexPattern)
                    ? null 
                    : new ValidationResult("L'email saisi n'est pas valide.");
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