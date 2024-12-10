using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.Roles;
using Microsoft.AspNetCore.Identity;

namespace backend.Interfaces
{
    public interface IRoleRepository
    {

        Task<bool> CheckExistence(string role);
        Task<IdentityRole> Create(IdentityRole roleDto);
    }
}