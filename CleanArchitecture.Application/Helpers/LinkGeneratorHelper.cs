using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Helpers
{
    public interface ILinkGeneratorHelper
    {
        string GenerateLink(string action, string controller, object values);
    }
    public class LinkGeneratorHelper : ILinkGeneratorHelper
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LinkGeneratorHelper(LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor)
        {
            _linkGenerator = linkGenerator;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GenerateLink(string action, string controller, object values)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var scheme = request.Scheme;
            var host = request.Host;

            // Debugging output
            Console.WriteLine($"Generating link with action: {action}, controller: {controller}, values: {values}, scheme: {scheme}, host: {host}");

            var link = _linkGenerator.GetUriByAction(_httpContextAccessor.HttpContext, action, controller, values, scheme, host);

            // Debugging output
            Console.WriteLine($"Generated link: {link}");

            return link;
        }
    }
}
