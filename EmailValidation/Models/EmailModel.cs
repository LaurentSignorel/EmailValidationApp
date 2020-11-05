using EmailValidation.Helpers;
using System.ComponentModel.DataAnnotations;

namespace EmailValidation.Models
{
    public class EmailModel
    {
        [Display(Name = "Adresse Email")]
        [Required(ErrorMessage = "Veuillez saisir un email à valider.")]
        [EmailAddress(ErrorMessage = "L'email saisi n'est pas valide.")]
        [CustomEmailLoginValidation()]
        [CustomEmailRegexValidation()]
        [CustomEmailGeolocValidation()]
        public string Email { get; set; }
    }
}