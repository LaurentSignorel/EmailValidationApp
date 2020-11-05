using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmailValidation.Helpers;

namespace EmailValidation.Test
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class CustomEmailGeolocTest
    {
        /// <summary>
        /// 
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
        /// 
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
        /// 
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
    }
}
