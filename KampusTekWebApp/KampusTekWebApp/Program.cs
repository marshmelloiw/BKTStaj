using KampusTek.Business.Abstract;
using KampusTek.Business.Concrete;
using KampusTek.Data;
using KampusTek.Data.Abstract;
using KampusTek.Data.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<KampusTekDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();
// Authentication
var jwtSection = builder.Configuration.GetSection("Jwt");
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
// Cookie auth for website (MVC)
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/Login";
    options.SlidingExpiration = true;
})
// JWT bearer for API
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = jwtSection["Issuer"],
        ValidAudience = jwtSection["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"]!)),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

// Generic Repository Registration
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Service Registration
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserTypeService, UserTypeService>();
builder.Services.AddScoped<IStationService, StationService>();
builder.Services.AddScoped<IBicycleService, BicycleService>();
builder.Services.AddScoped<IRentalService, RentalService>();
builder.Services.AddScoped<IMaintenanceService, MaintenanceService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Public Account routes (login/register) must be accessible anonymously
app.MapControllerRoute(
    name: "account",
    pattern: "Account/{action=Login}/{id?}",
    defaults: new { controller = "Account" });

// Require auth for other MVC site routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
   .RequireAuthorization();

// Development-only: seed a test user with hashed password if missing
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<KampusTekDbContext>();
    if (!db.Users.Any(u => u.Email == "test@example.com"))
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512();
        var salt = hmac.Key;
        var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("123"));
        db.Users.Add(new KampusTek.Entities.User
        {
            FirstName = "Test",
            LastName = "User",
            Email = "test@example.com",
            CellNumber = "0000000000",
            UserTypeId = 1,
            PasswordHash = hash,
            PasswordSalt = salt
        });
        db.SaveChanges();
    }

}

app.Run();
