using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ProductieSysteemBuild2.Models;
using System.Web.Security;
using System.Security.Principal;

namespace ProductieSysteemBuild2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

           
        }
        //protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        //{
        //    HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
        //    if (authCookie != null)
        //    {
        //        FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
        //        string[] roles = authTicket.UserData.Split(',');
        //        GenericPrincipal userPrincipal = new GenericPrincipal(new GenericIdentity(authTicket.Name), roles);
        //        Context.User = userPrincipal;
        //    }
        //}
    }
}
