using System.Web.Mvc;
using System.Threading.Tasks;

using System.Web.Mvc.Expressions;

using FamousQuoteQuiz.Web.BindingModels;
using FamousQuoteQuiz.Services;
using FamousQuoteQuiz.Models;
using FamousQuoteQuiz.Data;

namespace FamousQuoteQuiz.Web.Controllers
{
    public class SettingsController : Controller
    {
        private IModesService modesService;

        public SettingsController(IModesService modesService)
        {
            this.modesService = modesService;
        }
        
        // GET: Settings
        [HttpGet]
        [HandleError(View = "UnpopulatedDbError", ExceptionType = typeof(UnpopulatedDbException))]
        public async Task<ActionResult> Index()
        {
            QuizModeType selectedModeType = await this.modesService.GetSelectedMode();
            var bindingModel = new QuizModeBindingModel { Type = selectedModeType };

            return this.View(bindingModel);
        }

        // POST: Settings
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(QuizModeBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                await this.modesService.UpdateMode(model.Type);

                this.TempData["Message"] = "Quiz mode changed";

                return this.RedirectToAction<HomeController>(c => c.Index());
            }

            return this.View(model);
        }
    }
}