using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practical_15Test2.Filters
{
    public class CustomAuthorizeAttribute: AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                // User is logged in but not authorized -  show custom message
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/Shared/Unauthorized.cshtml"
                };
            }
            else
            {
                // User is not logged in - redirect to Login
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}