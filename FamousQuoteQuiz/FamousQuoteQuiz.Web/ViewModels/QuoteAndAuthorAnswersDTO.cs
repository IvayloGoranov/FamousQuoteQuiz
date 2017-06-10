using System.Collections.Generic;

using FamousQuoteQuiz.Services.DTOs;
using FamousQuoteQuiz.Models;

namespace FamousQuoteQuiz.Web.ViewModels
{
    public class QuoteAndAuthorAnswersDTO
    {
        public QuoteDTO Quote { get; set; }

        public IList<string> AuthorAnswers { get; set; }

        public QuizModeType QuizModeType { get; set; }
    }
}