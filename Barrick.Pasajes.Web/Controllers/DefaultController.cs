using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
namespace Barrick.Pasajes.Web.Controllers
{
    public class DefaultController : Controller
    {

        protected override void OnActionExecuting ( ActionExecutingContext filterContext )
        {
            var session = filterContext.HttpContext.Session;
            if (session == null || (!session.IsNewSession && Session["LoginUsuario"] != null)) return;

            if (Request.IsAjaxRequest() && (!Request.IsAuthenticated || User == null))
            {
                filterContext.RequestContext.HttpContext.Response.StatusCode = 401;
                filterContext.HttpContext.Response.End();
                throw new Exception("Session Expirada, redireccionar al Login.");
            }

            FormsAuthentication.SignOut();
            filterContext.Result =
                new RedirectToRouteResult(new RouteValueDictionary
                    {
                        {"action", "login"},
                        {"controller", "Home"},
                        {"returnUrl", filterContext.HttpContext.Request.RawUrl}
                    });
            return;
        }

    }
}
