using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("/api/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost("create-new")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateRolesRequestDto model)
        {
            if (await _roleManager.RoleExistsAsync(model.Role)) return BadRequest("That role already exists");

            var newRole = await _roleManager.CreateAsync(new IdentityRole(model.Role.ToUpper()));
            if (newRole == null) return BadRequest("Invalid Input Data");

            return Ok(newRole);
        }

        [HttpPost("create-role")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> CreateNew([FromBody] CreateRolesRequestDto model)
        {
            if (await _roleManager.RoleExistsAsync(model.Role)) return BadRequest("That role already exists");

            var newRole = await _roleManager.CreateAsync(new IdentityRole(model.Role.ToUpper()));
            if (newRole == null) return BadRequest("Invalid Input Data");

            return Ok(newRole);
        }
    }
}