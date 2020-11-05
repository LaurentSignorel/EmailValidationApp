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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string _pattern;

        public CustomEmailRegexValidationAttribute()
        {
            _pattern = ConfigurationManager.AppSettings.Get("regexPattern");
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                if (value == null)
                {
                    return new ValidationResult("Veuillez saisir un email à valider.");
                }

                return Regex.IsMatch((string)value, _pattern)
                    ? null 
                    : new ValidationResult("L'email saisi n'est pas valide.");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return new ValidationResult("Une erreur interne est survenue, merci de recommencer.");
            }
        }
    }
}