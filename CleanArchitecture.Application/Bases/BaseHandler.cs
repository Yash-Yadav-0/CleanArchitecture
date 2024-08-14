using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Application.Interfaces.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CleanArchitecture.Application.Bases
{
    public class BaseHandler
    {
        public IUnitOfWork UnitOfWork { get; }
        public IMapper Mapper { get; }
        public IHttpContextAccessor HttpContextAccessor { get; }
        public string UserId { get; set; }

        public BaseHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            HttpContextAccessor = httpContextAccessor;
            UserId = HttpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
