using System.Security.Claims;
using System.Web.Mvc;
using System.Web.Routing;

namespace Restaurant.Web.Attributes
{
    public class ClaimsAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string _claimType;
        private readonly string[] _claimValues;

        public ClaimsAuthorizeAttribute(string claimType, string[] claimValues)
        {
            _claimType = claimType;
            _claimValues = claimValues;
        }


        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = filterContext.HttpContext.User as ClaimsPrincipal;
            var allowAccess = false;
            foreach (var claimValue in _claimValues)
            {
                if (user!=null && user.HasClaim(_claimType, claimValue))
                    allowAccess = true;
            }

            if (allowAccess)
                base.OnAuthorization(filterContext);
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
            }
        }

        
    }
}