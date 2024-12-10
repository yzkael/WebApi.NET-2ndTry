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
            builder.Entity<Persona>(entity =>
            {
                entity.HasIndex(p => p.Carnet).IsUnique();
            });

            builder.Entity<Persona>().HasData(
                new Persona
                {
                    Id = 1,
                    Nombre = "Ismael",
                    ApellidoPaterno = "Moron",
                    ApellidoMaterno = "Pedraza",
                    Carnet = "12597382",
                    Telefono = "75526864",
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
                    Id= "1",
                    Name="Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id= "2",
                    Name="User",
                    NormalizedName = "User"
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            // Assign Admin Role to the User
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = "1", // The Id of the Usuario
                    RoleId = "1"  // The Id of the Admin role
                }
            );

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