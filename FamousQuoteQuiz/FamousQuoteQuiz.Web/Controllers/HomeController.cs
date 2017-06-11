using System.Web.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

using FamousQuoteQuiz.Services;
using FamousQuoteQuiz.Models;
using FamousQuoteQuiz.Web.ViewModels;
using FamousQuoteQuiz.Services.DTOs;
using FamousQuoteQuiz.Utils;
using FamousQuoteQuiz.Web.CustomAtttributes;
using FamousQuoteQuiz.Data;

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
        [HandleError(View = "UnpopulatedDbError", ExceptionType = typeof(UnpopulatedDbException))]
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
                    if (authorAnswers.Contains(randomAuthor.Name))
                    {
                        continue;
                    }

                    authorAnswers.Add(randomAuthor.Name);
                }

                if (!authorAnswers.Contains(randomQuote.Author))
                {
                    int randomPosition =
                        StaticRandomizer.RandomNumber(0, GlobalConstants.MultipleChoiceModeDefaultAuthorsCount);
                    authorAnswers[randomPosition] = randomQuote.Author;
                }
            }

            viewModel.AuthorAnswers = authorAnswers;

            return this.View(viewModel);
        }

        [AjaxChildActionOnly]
        [HandleError(View = "UnpopulatedDbError", ExceptionType = typeof(UnpopulatedDbException))]
        public async Task<ActionResult> ProcessAnswer(int id, string author, string answer)
        {
            var quote = await this.quotesService.GetQuoteById(id);
            if (!string.IsNullOrEmpty(answer))
            {
                if ((answer.Equals("yes") && quote.Author.Equals(author)) ||
                (answer.Equals("no") && !quote.Author.Equals(author)))
                {
                    return this.PartialView("_ProcessAnswer", 
                        GlobalConstants.DefaultCorrectAnswerResponse + quote.Author);
                }
                else
                {
                    return this.PartialView("_ProcessAnswer", 
                        GlobalConstants.DefaultWrongAnswerResponse + quote.Author);
                }
            }

            if (quote.Author.Equals(author))
            {
                return this.PartialView("_ProcessAnswer", 
                    GlobalConstants.DefaultCorrectAnswerResponse + quote.Author);
            }

            return this.PartialView("_ProcessAnswer", 
                GlobalConstants.DefaultWrongAnswerResponse + quote.Author);
        }
    }
}