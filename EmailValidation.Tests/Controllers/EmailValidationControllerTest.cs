using System.Web.Mvc;
using EmailValidation.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmailValidation.Tests.Controllers
{
    [TestClass]
    public class EmailValidationControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            EmailValidationController controller = new EmailValidationController();
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }
    }
}
