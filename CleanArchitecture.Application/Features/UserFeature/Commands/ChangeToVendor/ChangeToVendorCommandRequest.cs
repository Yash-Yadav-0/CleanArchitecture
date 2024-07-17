using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.UserFeature.Commands.ChangeToVendor
{
    public class ChangeToVendorCommandRequest : IRequest<ChangeToVendorCommandResponse>
    {
        [DefaultValue("Enter User Email for (User to Vendor)")]
        public string Email { get; set; }
    }
}
