using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options): base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // seed roles user,admin, superadmin
            var adminRoleId = "95816cc4 - e5d9 - 4eb5 - bfef - 59909a91100f";
            var superAdminRoleId = "0b034830 - 29d8 - 4eef - a8e1 - ae5e69903112";
            var userRoleId = "b24acfcf-924a-4f11-8258-b487a2e4d18d";
            var roles = new List<IdentityRole>
            { 
               new IdentityRole
               {
                   Name="Admin",
                   NormalizedName="Admin",
                   Id = adminRoleId,
                   ConcurrencyStamp =adminRoleId
               },
               new IdentityRole
               {
                   Name="SuperAdmin",
                   NormalizedName="SuperAdmin",
                   Id = superAdminRoleId,
                   ConcurrencyStamp =superAdminRoleId
               },
               new IdentityRole
               {
                   Name="User",
                   NormalizedName="User",
                   Id = userRoleId,
                   ConcurrencyStamp =userRoleId
               }

            };
            builder.Entity<IdentityRole>().HasData(roles);
            //seed superadmin user
            var superAdminId = "a3fc0df1-bbaa-4b30-aa97-5899e03cc3ee";
            var superAdminUser = new IdentityUser
            { 
              UserName="superadmin@bloggie.com",
              Email="superadmin@bloggie.com",
              NormalizedEmail= "superadmin@bloggie.com".ToUpper(),
              NormalizedUserName= "superadmin@bloggie.com".ToUpper(),
              Id= superAdminId

            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "Superadmin@123");
            builder.Entity<IdentityUser>().HasData(superAdminUser);
            //add all the roles to super admin user

            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId=adminRoleId,
                    UserId=superAdminId
                },
                 new IdentityUserRole<string>
                {
                    RoleId=superAdminRoleId,
                    UserId=superAdminId
                },
                  new IdentityUserRole<string>
                {
                    RoleId=userRoleId,
                    UserId=superAdminId
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}
