using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Application.Helpers
{
    public class UrlFactoryHelper
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IActionContextAccessor actionContextAccessor;
        private readonly IUrlHelperFactory urlHelperFactory;
        public UrlFactoryHelper(IHttpContextAccessor httpContextAccessor, IActionContextAccessor actionContextAccessor, IUrlHelperFactory urlHelperFactory)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.actionContextAccessor = actionContextAccessor;
            this.urlHelperFactory = urlHelperFactory;
        }
        public IUrlHelper CreateUrlHelper()
        {
            var actionContext = actionContextAccessor.ActionContext;
            if (actionContext == null)
            {
                throw new InvalidOperationException("ActionContext cannot be null. Ensure that the IActionContextAccessor is correctly configured.");
            }
            return urlHelperFactory.GetUrlHelper(actionContext);
        }
    }
}
