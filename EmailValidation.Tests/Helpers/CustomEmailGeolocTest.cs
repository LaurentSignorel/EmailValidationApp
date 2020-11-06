using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmailValidation.Helpers;
using Ninject;
using EmailValidation.Services;

namespace EmailValidation.Test
{
    /// <summary>
    /// Classe de test de l'attribut personnalisé : CustomEmailGeolocValidationAttribute
    /// </summary>
    [TestClass]
    public class CustomEmailGeolocTest
    {
        CustomEmailGeolocValidationAttribute Attr { get; set; }
        [TestInitialize]
        public void MyTestInitialize()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IConfigurationService>().To<ConfigurationService>();
            kernel.Bind<IGeoLocService>().To<GeoLocService>();
            Attr = kernel.Get<CustomEmailGeolocValidationAttribute>();
        }
        /// <summary>
        /// test d'un domaine valide : renvoie true
        /// </summary>
        [TestMethod]
        public void CustomEmailGeolocValidationAttribute_DomainIsValid()
        {
            // Arrange
            var value = "laurent@afnic.fr";
            // Act
            var result = Attr.IsValid(value);
            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// test d'un domaine invalide : renvoie false
        /// </summary>
        [TestMethod]
        public void CustomEmailGeolocValidationAttribute_DomainIsNotValid()
        {
            // Arrange
            var value = "laurent@testinvaliddomain.fr";
            // Act
            var result = Attr.IsValid(value);
            // Assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// test d'un domaine valide dont le code pays n'est pas dans la liste autorisée : renvoie false
        /// </summary>
        [TestMethod]
        public void CustomEmailGeolocValidationAttribute_DomainIsValidButNotInAllowedCountries()
        {
            // Arrange
            var value = "laurent@example.com";
            // Act
            var result = Attr.IsValid(value);
            // Assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// test d'un domaine valide et dont le code pays est dans la liste autorisée : renvoie true
        /// </summary>
        [TestMethod]
        public void CustomEmailGeolocValidationAttribute_EmailIsInAuthorizedCountry()
        {
            // Arrange
            var value = "laurent@namebay.mc";
            // Act
            var result = Attr.IsValid(value);
            // Assert
            Assert.IsTrue(result);
        }
    }
}
