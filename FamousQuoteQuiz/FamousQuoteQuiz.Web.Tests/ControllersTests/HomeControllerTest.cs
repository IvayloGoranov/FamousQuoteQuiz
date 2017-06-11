using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;
using TestStack.FluentMVCTesting;

using FamousQuoteQuiz.Services;
using FamousQuoteQuiz.Services.DTOs;
using FamousQuoteQuiz.Models;
using FamousQuoteQuiz.Web.Controllers;
using FamousQuoteQuiz.Web.ViewModels;

namespace FamousQuoteQuiz.Web.Tests.ControllersTests
{
    [TestClass]
    public class HomeControllerTest
    {
        private Mock<IQuotesService> quotesServiceMock;
        private Mock<IAuthorsService> authorsServiceMock;
        private Mock<IModesService> modesServiceMock;

        [TestInitialize]
        public void Init()
        {
            this.quotesServiceMock = new Mock<IQuotesService>();
            var quoteDTO = new QuoteDTO { Id = 1, Author = "Pesho", Content = "I am cool" };
            this.quotesServiceMock.Setup(x => x.GetRandomQuote()).ReturnsAsync(quoteDTO);

            this.authorsServiceMock = new Mock<IAuthorsService>();
            var authorDTO = new AuthorDTO { Name = "Pesho" };
            this.authorsServiceMock.Setup(x => x.GetRandomAuthor()).ReturnsAsync(authorDTO);

            this.modesServiceMock = new Mock<IModesService>();
            this.modesServiceMock.Setup(x => x.GetSelectedMode()).ReturnsAsync(QuizModeType.Binary);
        }

        [TestMethod]
        public void TestIndex_ShouldWorkCorrectly()
        {
            var controller = new HomeController(this.quotesServiceMock.Object, this.authorsServiceMock.Object,
                this.modesServiceMock.Object);
            controller.WithCallTo(x => x.Index())
                .ShouldRenderView("Index")
                .WithModel<QuoteAndAuthorAnswersDTO>();
        }
    }
}
