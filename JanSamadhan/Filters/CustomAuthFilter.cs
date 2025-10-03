using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace JanSamadhan.Filters
{
    public class CustomAuthAttribute : ActionFilterAttribute
    {
        public string UserType { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (UserType == "User")
            {
                if (context.HttpContext.Session.GetInt32("UserId") == null)
                {
                    context.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            { "controller", "User" },
                            { "action", "Login" }
                        });
                }
            }
            else if (UserType == "Officer")
            {
                if (context.HttpContext.Session.GetInt32("OfficerId") == null)
                {
                    context.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            { "controller", "McpOfficer" },
                            { "action", "Login" }
                        });
                }
            }

            base.OnActionExecuting(context);
        }
    }
}
