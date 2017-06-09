using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FamousQuoteQuiz.Web.Startup))]
namespace FamousQuoteQuiz.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
