using System.Web.Mvc;

using FamousQuoteQuiz.Web.CustomAtttributes;

namespace FamousQuoteQuiz.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
