using System.Linq;
using System.Security.Claims;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Security
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUsername()
        {
            //in the current http context, if the user exists, and his claims exists,
            //and the type of his claims matches his name - return it
            var username = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(
                x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            
            return username;
        }
    }
}