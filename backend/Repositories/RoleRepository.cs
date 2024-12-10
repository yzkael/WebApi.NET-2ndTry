using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.Roles;
using backend.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace backend.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleRepository(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> CheckExistence(string role)
        {
            return await _roleManager.RoleExistsAsync(role);
        }

        public async Task<IdentityRole> Create(IdentityRole roleDto)
        {
            throw new NotImplementedException();
        }
    }
}