using EmailValidation.Helpers;
using EmailValidation.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace EmailValidation.Test
{
    /// <summary>
    /// Classe de test de l'attribut personnalisé : CustomEmailRegexValidationAttribute
    /// </summary>
    [TestClass]
    public class CustomEmailRegexTest
    {
        CustomEmailRegexValidationAttribute Attr { get; set; }

        [TestInitialize]
        public void MyTestInitialize()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IConfigurationService>().To<ConfigurationService>();
            Attr = kernel.Get<CustomEmailRegexValidationAttribute>();
        }
        /// <summary>
        /// test d'un mail valide : renvoie true
        /// </summary>
        [TestMethod]
        public void CustomEmailRegexValidationAttribute_EmailIsValid()
        {
            // Arrange
            var value = "laurent@afnic.fr";
            // Act
            var result = Attr.IsValid(value);
            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// test d'un email dont le domaine contient au maximum 3 sous-domaines : renvoie true
        /// </summary>
        [TestMethod]
        public void CustomEmailRegexValidationAttribute_EmailDomainHasThreeOrLessSubDomains()
        {
            // Arrange
            var value = "laurent@example.microsoft.verisign.mondomaine.fr";
            // Act
            var result = Attr.IsValid(value);
            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// test d'un email dont le domaine contient plus de 3 sous-domaines : renvoie false
        /// </summary>
        [TestMethod]
        public void CustomEmailRegexValidationAttribute_EmailDomainHasMoreThanThreeSubDomains()
        {
            // Arrange
            var value = "laurent@example.microsoft.afnic.verisign.mondomaine.fr";
            // Act
            var result = Attr.IsValid(value);
            // Assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// test d'un email dont l'identifiant commence par un chiffre : renvoie false
        /// </summary>
        [TestMethod]
        public void CustomEmailRegexValidationAttribute_EmailBeginWithNumber()
        {
            // Arrange
            var value = "6laurent@afnic.fr";
            // Act
            var result = Attr.IsValid(value);
            // Assert
            Assert.IsFalse(result);
        }
    }
}
