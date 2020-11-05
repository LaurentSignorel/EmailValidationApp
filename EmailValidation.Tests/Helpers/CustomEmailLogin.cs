using EmailValidation.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmailValidation.Test
{
    /// <summary>
    /// Classe de test de l'attribut personnalisé : CustomEmailLoginValidationAttribute
    /// </summary>
    [TestClass]
    public class CustomEmailLoginTest
    {
        /// <summary>
        /// test d'un mail valide : renvoie true
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
        /// test d'un mail dont l'identifiant contient plus de lettres que de chiffres : renvoie true
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
        /// test d'un mail dont l'identifiant contient plus de chiffres que de lettres : renvoie false
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
