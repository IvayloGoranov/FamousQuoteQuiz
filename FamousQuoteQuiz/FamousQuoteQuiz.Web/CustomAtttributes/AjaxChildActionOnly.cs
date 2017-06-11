using System.Reflection;
using System.Web.Mvc;

namespace FamousQuoteQuiz.Web.CustomAtttributes
{
    public class AjaxChildActionOnlyAttribute : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return controllerContext.RequestContext.HttpContext.Request.IsAjaxRequest() ||
                controllerContext.IsChildAction;
        }
    }
}