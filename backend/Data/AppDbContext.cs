using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
namespace backend.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Persona>().HasData(
                new Persona
                {
                    Id = 1,
                    Nombre = "Ismael",
                    ApellidoPaterno = "Moron",
                    ApellidoMaterno = "Pedraza",
                    Carnet = "12597382",
                    UsuarioId = "1"
                }
            );
            var hasher = new PasswordHasher<Usuario>();
            builder.Entity<Usuario>().HasData(

                new Usuario
                {
                    Id = "1",
                    UserName = "ismael",
                    Email = "ismaelmp997@hotmail.com",
                    PasswordHash = hasher.HashPassword(null, "123456"),
                    PersonaId = 1
                }
            );

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name="Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name="Cliente",
                    NormalizedName = "Cliente"
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);



            base.OnModelCreating(builder);

            // Configure one-to-one relationship between Usuario and Persona
            builder.Entity<Usuario>()
                .HasOne(u => u.Persona)
                .WithOne(p => p.Usuario)
                .HasForeignKey<Usuario>(u => u.PersonaId)
                .OnDelete(DeleteBehavior.Cascade); // Optional: cascade delete if needed
        }

        public DbSet<Persona> Personas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }


    }
}