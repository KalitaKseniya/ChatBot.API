
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatBot.Core.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        private const string adminRoleId = "2301D884-221A-4E7D-B509-0113DCC043E1";
        private const string superAdminRoleId = "1231D884-221A-4E7D-B509-0113DCC043E1";
        private const string managerRoleId = "7D9B7113-A8F8-4035-99A7-A20DD400F6A3";

        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = managerRoleId,
                    Name = "Manager",
                },
            new IdentityRole
            {
                Id = adminRoleId,
                Name = "Admin",
            },
            new IdentityRole
            {
                Id = superAdminRoleId,
                Name = "SuperAdmin"
            });
        }
    }
}
