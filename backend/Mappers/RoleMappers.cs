using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.Roles;
using Microsoft.AspNetCore.Identity;

namespace backend.Mappers
{
    public static class RoleMappers
    {
        public static IdentityRole FromCreateRequestToIdentityRole(this CreateRolesRequestDto model)
        {
            return new IdentityRole(model.Role);
        }
    }
}