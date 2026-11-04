using MedicalDeviceSafetyPlatform.Data;
using MedicalDeviceSafetyPlatform.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=findings-demo.db"));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/Account/Login");
var app = builder.Build();
if (!app.Environment.IsDevelopment()) { app.UseExceptionHandler("/Home/Error"); app.UseHsts(); }
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // Local demonstration database only; production/team integration must use migrations.
    await db.Database.EnsureCreatedAsync();
    if (!await db.SafetyNotices.AnyAsync())
    {
        db.SafetyNotices.AddRange(
            new SafetyNotice { Reference = "DEMO-2026-001", DeviceName = "Infusion Pump" },
            new SafetyNotice { Reference = "DEMO-2026-002", DeviceName = "Patient Monitor" });
        await db.SaveChangesAsync();
    }
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    if (!await roleManager.RoleExistsAsync("Employee"))
        await roleManager.CreateAsync(new IdentityRole("Employee"));
}
await app.RunAsync();
