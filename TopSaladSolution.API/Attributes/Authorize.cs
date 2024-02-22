using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TopSaladSolution.API.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class Authorize : AuthorizeAttribute, IAuthorizationFilter
    {
        private AuthorizationFilterContext _context;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _context = context;
            if(!context.HttpContext.User.Identity.IsAuthenticated) return;
        }
    }
}
