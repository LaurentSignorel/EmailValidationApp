using EmailValidation.Models;
using System.Web.Mvc;

namespace EmailValidation.Controllers
{
    public class EmailValidationController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // GET: EmailValidation
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(EmailModel model)
        {
            if (ModelState.IsValid)
            {
                log.Debug(model.Email);
                return View(model);
            }
            return View();
        }
    }
}