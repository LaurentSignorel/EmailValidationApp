using EmailValidation.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmailValidation.Test
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class CustomEmailLoginTest
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void CustomEmailLoginValidationAttribute_EmailIsValid()
        {
            // Arrange
            var attrib = new CustomEmailLoginValidationAttribute();
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
        public void CustomEmailLoginValidationAttribute_EmailIdentifierHasLessNumbersThanLetters()
        {
            // Arrange
            var attrib = new CustomEmailLoginValidationAttribute();
            var value = "laurent-123@afnic.fr";
            // Act
            var result = attrib.IsValid(value);
            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void CustomEmailLoginValidationAttribute_EmailIdentifierHasMoreNumbersThanLetters()
        {
            // Arrange
            var attrib = new CustomEmailLoginValidationAttribute();
            var value = "laurent-123456789@afnic.fr";
            // Act
            var result = attrib.IsValid(value);
            // Assert
            Assert.IsFalse(result);
        }
    }
}
