using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            builder.HasData(
            new ApplicationUser
            {
                Id = "d735fa86-1e8c-11ed-861d-0242ac120002",
                Email = "admin@localhost.com",
                NormalizedEmail = "admin@localhost.com",
                Nombre = "Admin",
                Apellidos = "Admin",
                UserName = "AdminAdmin",
                NormalizedUserName = "AdminAdmin",
                PasswordHash = hasher.HashPassword(null,"papero546"),
                EmailConfirmed = true,
            },
            new ApplicationUser
            {
                Id = "865ce880-1e8d-11ed-861d-0242ac120002",
                Email = "papero@localhost.com",
                NormalizedEmail = "papero@localhost.com",
                Nombre = "Papero",
                Apellidos = "Papero",
                UserName = "PaperoPapero",
                NormalizedUserName = "PaperoPapero",
                PasswordHash = hasher.HashPassword(null, "chochera546"),
                EmailConfirmed = true,
            });
        }
    }
}
