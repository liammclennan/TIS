using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace TodayIShall.Web.Infrastructure
{
    public class IdentityAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var splitUrl = httpContext.Request.Path.Split('/');
            var nameInUrl = splitUrl[2];
            var correctUser = System.Threading.Thread.CurrentPrincipal.Identity.Name.Equals(nameInUrl, StringComparison.InvariantCultureIgnoreCase);
            if (!correctUser) FormsAuthentication.SignOut();
            return correctUser;
        }
    }
}