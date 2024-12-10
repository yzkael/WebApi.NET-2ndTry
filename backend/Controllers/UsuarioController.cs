using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Dtos.Usuarios;
using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("/api/user")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly ITokenServices _tokenServices;
        public UsuarioController(AppDbContext context, ITokenServices tokenServices, UserManager<Usuario> userManager)
        {
            _userManager = userManager;
            _context = context;
            _tokenServices = tokenServices;
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                // Fetch all users
                var allUsers = await _userManager.Users.ToListAsync();

                // Return a success response
                return Ok(allUsers);
            }
            catch (Exception ex)
            {
                // Log the exception (use a logging framework like Serilog or NLog in real applications)
                Console.Error.WriteLine($"An error occurred while fetching users: {ex.Message}");

                // Return a meaningful error response
                return Problem("An error occurred while fetching the user data.", statusCode: 500);
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUsuarioDto model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var foundUser = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == model.Username);
                if (foundUser == null) return BadRequest("Invalid Username or Password");
                var correctPassword = await _userManager.CheckPasswordAsync(foundUser, model.Password);
                if (!correctPassword) return BadRequest("Invalid Username or Password");

                var token = await _tokenServices.CreateToken(foundUser);

                Response.Cookies.Append("auth_token", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddHours(1)
                });

                return Ok(new { message = "Logged In succesfully!" });

            }
            catch (System.Exception)
            {
                return StatusCode(500, "Internal Server Error 500");
            }
        }
    }
}