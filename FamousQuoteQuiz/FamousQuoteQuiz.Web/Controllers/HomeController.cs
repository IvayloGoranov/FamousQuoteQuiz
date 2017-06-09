using System.Web.Mvc;

namespace FamousQuoteQuiz.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}