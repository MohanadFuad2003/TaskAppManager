using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TaskManagerApp.Data;

var builder = WebApplication.CreateBuilder(args);

// ===== MVC =====
builder.Services.AddControllersWithViews();

// ===== SQLite in App_Data =====
// نبني مسار قاعدة البيانات داخل جذر المشروع (وليس bin) لضمان ثبات المسار
var dataDir = Path.Combine(builder.Environment.ContentRootPath, "App_Data");
Directory.CreateDirectory(dataDir); // يتأكد من وجود المجلد
var dbPath = Path.Combine(dataDir, "tasks.db");
var sqliteConn = $"Data Source={dbPath}";

// تسجيل الـ DbContext على SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(sqliteConn));

// ===== Identity =====
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    // Password rules
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;

    // Lockout rules
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// ===== Cookies (Login/Logout paths) =====
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromDays(7); // Remember Me duration
    options.SlidingExpiration = true;
});

var app = builder.Build();

// ===== Apply migrations & create DB if missing =====
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate(); // ينشئ tasks.db ويطبق جميع الـ migrations
}

// ===== Pipeline =====
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // يجب قبل Authorization
app.UseAuthorization();

// ===== Redirect "/" according to auth state =====
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            context.Response.Redirect("/Home/Index");
            return;
        }
        else
        {
            context.Response.Redirect("/Account/Login");
            return;
        }
    }

    await next();
});

// ===== Routing =====
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Index}/{action=Index}/{id?}");

app.Run();
