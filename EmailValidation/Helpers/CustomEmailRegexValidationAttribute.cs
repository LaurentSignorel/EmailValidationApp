using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Text.RegularExpressions;

namespace EmailValidation.Helpers
{
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
                    return new ValidationResult("The email address is required!");
                }

                return Regex.IsMatch((string)value, _pattern)
                    ? null 
                    : new ValidationResult("Invalid Email Address!");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return new ValidationResult("Une erreur interne est survenue, merci de recommencer.");
            }
        }
    }
}