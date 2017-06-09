using Owin;

using FamousQuoteQuiz.Data;

namespace FamousQuoteQuiz.Web
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            //Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(QuoteQuizContext.Create);
        }
    }
}