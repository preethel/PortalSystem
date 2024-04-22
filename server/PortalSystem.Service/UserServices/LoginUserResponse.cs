using PortalSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalSystem.Service.UserServices
{
    public record LoginUserResponse(string AccessToken, RefreshToken RefreshToken);

}
