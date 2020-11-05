using EmailValidation.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmailValidation.Test
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class CustomEmailRegexTest
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void CustomEmailRegexValidationAttribute_EmailIsValid()
        {
            // Arrange
            var attrib = new CustomEmailRegexValidationAttribute();
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
        public void CustomEmailRegexValidationAttribute_EmailDomainHasThreeOrLessSubDomains()
        {
            // Arrange
            var attrib = new CustomEmailRegexValidationAttribute();
            var value = "laurent@example.microsoft.verisign.mondomaine.fr";
            // Act
            var result = attrib.IsValid(value);
            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void CustomEmailRegexValidationAttribute_EmailDomainHasMoreThanThreeSubDomains()
        {
            // Arrange
            var attrib = new CustomEmailRegexValidationAttribute();
            var value = "laurent@example.microsoft.afnic.verisign.mondomaine.fr";
            // Act
            var result = attrib.IsValid(value);
            // Assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void CustomEmailRegexValidationAttribute_EmailBeginWithNumber()
        {
            // Arrange
            var attrib = new CustomEmailRegexValidationAttribute();
            var value = "6laurent@afnic.fr";
            // Act
            var result = attrib.IsValid(value);
            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CustomEmailRegexValidationAttribute_EmailIsInAuthorizedCountry()
        {
            // Arrange
            var attrib = new CustomEmailRegexValidationAttribute();
            var value = "laurent@namebay.mc";
            // Act
            var result = attrib.IsValid(value);
            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CustomEmailRegexValidationAttribute_EmailIsNotInAuthorizedCountry()
        {
            // Arrange
            var attrib = new CustomEmailRegexValidationAttribute();
            var value = "laurent.signorel@outlook.com";
            // Act
            var result = attrib.IsValid(value);
            // Assert
            Assert.IsFalse(result);
        }
    }
}
