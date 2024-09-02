using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.UserFeature.Queries
{
    public class ShowAllUserPermissionsResponse
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Permissions {  get; set; }
    }
}
