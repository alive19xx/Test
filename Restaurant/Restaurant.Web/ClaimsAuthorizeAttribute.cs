using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Restaurant.Web
{
    public class ClaimsAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string _claimType;
        private readonly string _claimValue;

        public ClaimsAuthorizeAttribute(string claimType, string claimValue)
        {
            _claimType = claimType;
            _claimValue = claimValue;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = filterContext.HttpContext.User as ClaimsPrincipal;
            if (user.HasClaim(_claimType, _claimValue))
                base.OnAuthorization(filterContext);
            else
            {
                
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
            }
        }

        
    }
}