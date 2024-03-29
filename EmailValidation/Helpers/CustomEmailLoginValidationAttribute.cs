﻿using Ninject;
using Ninject.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EmailValidation.Helpers
{
    /// <summary>
    /// Attribut de validation de l'identifiant d'un email.
    /// Afin d'être valide, le l'identifiant du mail doit contenir plus de lettres que de chiffres.
    /// Les caractères spéciaux ne sont pas pris en compte.
    /// </summary>
    public class CustomEmailLoginValidationAttribute : ValidationAttribute
    {
        [Inject]
        public ILogger Logger { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                if (value == null)
                {
                    return new ValidationResult("Veuillez saisir un email à valider.");
                }

                string mailId = ((string)value).Substring(0, ((string)value).IndexOf('@'));

                int countLetters = mailId.Count(c => char.IsLetter(c));

                int countNumbers = mailId.Count(c => char.IsNumber(c));

                if (Logger != null)
                {
                    Logger.Info($"CustomEmailLoginValidation : Email = {value} /Identifier = {mailId} / Letters = {countLetters} / Numbers = {countNumbers}"); 
                }

                return countLetters < countNumbers
                    ? new ValidationResult("L'identifiant contient plus de lettres que de chiffres.")
                    : null;
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