using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;
using VehicleTrackingSystem.Core.Constants;
using VehicleTrackingSystem.Dal.Entities;

namespace VehicleTrackingSystem.Bal.TokenProvider
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (User)context.HttpContext.Items["User"];
            if (user == null)
            {
                context.Result = null;
                //throw new Exception(message: Messages.Unauthorized);
            }
        }
    }
}
