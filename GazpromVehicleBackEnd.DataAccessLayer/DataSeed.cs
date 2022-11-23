using GazpromVehicleBackEnd.DataAccessLayer.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GazpromVehicleBackEnd.DataAccessLayer;

public static class DataSeed
{
    public static void AddTestData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var userManager = (UserManager<User>)scope.ServiceProvider.GetService(typeof(UserManager<User>));
        var roleManager =
            (RoleManager<IdentityRole>)scope.ServiceProvider.GetService(typeof(RoleManager<IdentityRole>));
        var identityDbContextOptions =
            (DbContextOptions<ApplicationDbContext>)scope.ServiceProvider.GetService(
                typeof(DbContextOptions<ApplicationDbContext>));

        using var db = new ApplicationDbContext(identityDbContextOptions);
        db.Database.EnsureCreated();
        
        var user = userManager.FindByEmailAsync("user1@example.com").GetAwaiter().GetResult();
        if (user == null)
        {
            var user1 = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = "user1@example.com",
                EmailConfirmed = true,
                UserName = "user1@example.com"
            };
            var res = userManager.CreateAsync(user1, "user1").GetAwaiter().GetResult();
            if (res.Errors.Count() > 0)
            {
                throw new Exception();
            }
            else
                user = userManager.FindByEmailAsync("user1@example.com").GetAwaiter().GetResult();
        }
        
        var admin = userManager.FindByEmailAsync("admin@example.com").GetAwaiter().GetResult();
        if (admin == null)
        {
            var admin1 = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = "admin@example.com",
                EmailConfirmed = true,
                UserName = "admin@example.com"
            };
            var res = userManager.CreateAsync(admin1, "admin123").GetAwaiter().GetResult();
            if (res.Errors.Count() > 0)
            {
                throw new Exception();
            }
            else
                admin = userManager.FindByEmailAsync("admin@example.com").GetAwaiter().GetResult();
        }
        
        if (!roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
        {
            roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
        }

        if (!userManager.IsInRoleAsync(admin, "Admin").GetAwaiter().GetResult())
        {
            userManager.AddToRoleAsync(admin, "Admin").GetAwaiter().GetResult();
        }
        
        if (!roleManager.RoleExistsAsync("User").GetAwaiter().GetResult())
        {
            roleManager.CreateAsync(new IdentityRole("User")).GetAwaiter().GetResult();
        }
        
        if (!userManager.IsInRoleAsync(user, "User").GetAwaiter().GetResult())
        {
            userManager.AddToRoleAsync(user, "User").GetAwaiter().GetResult();
        }

        db.SaveChanges();
    }
}