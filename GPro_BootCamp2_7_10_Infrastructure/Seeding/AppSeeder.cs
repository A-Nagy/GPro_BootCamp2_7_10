 

using GPro_BootCamp2_7_10_Domain.Entities;
using GPro_BootCamp2_7_10_Infrastructure.Persistence;
 using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GPro_BootCamp2_7_10_Infrastructure.Seeding { 

public static class AppSeeder
{
     private static readonly string[] Permissions =
    {
        "Product.Read", "Product.Write",
        "Order.Read",   "Order.Write",
        "Category.Read","Category.Write"
    };

    public static async Task SeedAsync(
        ApplicationDbContext ctx,
        UserManager<ApplicationUser> users,
        RoleManager<ApplicationRole> roles)
    {
        await ctx.Database.MigrateAsync();

        // 1) دور Admin
        const string adminRoleName = "Admin";
        if (!await roles.RoleExistsAsync(adminRoleName))
        {
            await roles.CreateAsync(new ApplicationRole { Name = adminRoleName });
        }
        var adminRole = await roles.FindByNameAsync(adminRoleName)!;

        // 2) إسناد الصلاحيات (claims) للدور Admin
        var roleClaims = await ctx.RoleClaims.Where(rc => rc.RoleId == adminRole.Id).ToListAsync();
        foreach (var p in Permissions)
        {
            if (!roleClaims.Any(c => c.ClaimType == "Permission" && c.ClaimValue == p))
                ctx.RoleClaims.Add(new IdentityRoleClaim<int>
                {
                    RoleId = adminRole.Id,
                    ClaimType = "Permission",
                    ClaimValue = p
                });
        }

        // 3) إنشاء مستخدم أدمن
        const string adminUser = "admin";
        const string adminEmail = "admin@ecom.local";
        var user = await users.FindByNameAsync(adminUser);
        if (user == null)
        {
            user = new ApplicationUser
            {
                UserName = adminUser,
                Email = adminEmail,
                EmailConfirmed = true
            };
            await users.CreateAsync(user, "Admin@123");  
        }

        // 4) إسناد الدور للمستخدم
        var inRole = await users.IsInRoleAsync(user, adminRoleName);
        if (!inRole) await users.AddToRoleAsync(user, adminRoleName);

        // 5) بيانات كتالوج مبدئية (Categories/Products)
        if (!await ctx.Categories.AnyAsync())
        {
            var cat1 = new Category { Name = "Electronics" };
            var cat2 = new Category { Name = "Home" };
            ctx.Categories.AddRange(cat1, cat2);

            ctx.Products.AddRange(
                new Product { Name = "Headphones", Price = 199.99m, Currency = "SAR", Qty = 50, Category = cat1 },
                new Product { Name = "Air Fryer", Price = 349.00m, Currency = "SAR", Qty = 20, Category = cat2 }
            );
        }

        await ctx.SaveChangesAsync();
    }
}
}