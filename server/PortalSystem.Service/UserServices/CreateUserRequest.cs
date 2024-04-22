﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalSystem.Service.UserServices
{
    public record CreateUserRequest(
        string Name,
        string Email,
        string Password
        //,List<string> RoleList
    );
}
