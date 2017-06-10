using System.Web.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

using FamousQuoteQuiz.Services;
using FamousQuoteQuiz.Models;
using FamousQuoteQuiz.Web.ViewModels;
using FamousQuoteQuiz.Services.DTOs;
using FamousQuoteQuiz.Utils;

namespace FamousQuoteQuiz.Web.Controllers
{
    public class HomeController : Controller
    {
        private IQuotesService quotesService;
        private IAuthorsService authorsService;
        private IModesService modesService;

        public HomeController(IQuotesService quotesService, IAuthorsService authorsService,
            IModesService modesService)
        {
            this.quotesService = quotesService;
            this.authorsService = authorsService;
            this.modesService = modesService;
        }

        // GET: Index
        public async Task<ActionResult> Index()
        {
            var viewModel = new QuoteAndAuthorAnswersDTO();
            QuoteDTO randomQuote = await this.quotesService.GetRandomQuote();
            viewModel.Quote = randomQuote;
            var authorAnswers = new List<string>();

            viewModel.QuizModeType = await this.modesService.GetSelectedMode();
            if (viewModel.QuizModeType == QuizModeType.Binary)
            {
                var randomAuthor = await this.authorsService.GetRandomAuthor();
                authorAnswers.Add(randomAuthor.Name);
            }
            else
            {
                while (authorAnswers.Count < GlobalConstants.MultipleChoiceModeDefaultAuthorsCount)
                {
                    var randomAuthor = await this.authorsService.GetRandomAuthor();
                    var duplicateAuthor = authorAnswers.
                        FirstOrDefault(x => x.Equals(randomAuthor.Name));
                    var matchingAuthor = authorAnswers.
                        FirstOrDefault(x => x.Equals(randomQuote.Author));
                    if (duplicateAuthor != null || matchingAuthor != null)
                    {
                        continue;
                    }

                    authorAnswers.Add(randomAuthor.Name);
                }

                int randomPosition =
                    StaticRandomizer.RandomNumber(0, GlobalConstants.MultipleChoiceModeDefaultAuthorsCount);
                authorAnswers[randomPosition] = randomQuote.Author;
            }

            viewModel.AuthorAnswers = authorAnswers;

            return this.View(viewModel);
        }
    }
}