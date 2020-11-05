using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmailValidation.Helpers;

namespace EmailValidation.Test
{
    /// <summary>
    /// Classe de test de l'attribut personnalisé : CustomEmailGeolocValidationAttribute
    /// </summary>
    [TestClass]
    public class CustomEmailGeolocTest
    {
        /// <summary>
        /// test d'un domaine valide : renvoie true
        /// </summary>
        [TestMethod]
        public void CustomEmailGeolocValidationAttribute_DomainIsValid()
        {
            // Arrange
            var attrib = new CustomEmailGeolocValidationAttribute();
            var value = "laurent@afnic.fr";
            // Act
            var result = attrib.IsValid(value);
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
            var attrib = new CustomEmailGeolocValidationAttribute();
            var value = "laurent@testinvaliddomain.fr";
            // Act
            var result = attrib.IsValid(value);
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
            var attrib = new CustomEmailGeolocValidationAttribute();
            var value = "laurent@example.com";
            // Act
            var result = attrib.IsValid(value);
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
            var attrib = new CustomEmailGeolocValidationAttribute();
            var value = "laurent@namebay.mc";
            // Act
            var result = attrib.IsValid(value);
            // Assert
            Assert.IsTrue(result);
        }
    }
}
