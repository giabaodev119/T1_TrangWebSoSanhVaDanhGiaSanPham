using DACS.DataAccess;
using DACS.Interface;
using DACS.Models;
using DACS.Models.EF;
using DACS.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Configuration;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Hosting;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddDefaultTokenProviders()
        .AddDefaultUI()
        .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Thi?t l?p Lockout.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.AllowedForNewUsers = true;

});
var configuration = builder.Configuration;

builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
	IConfigurationSection googleAuthNSection = builder.Configuration.GetSection("Authentication:Google");
	googleOptions.ClientId = googleAuthNSection["ClientId"];

	googleOptions.ClientSecret = googleAuthNSection["ClientSecret"];

	// Cấu hình Url callback lại từ Google (không thiết lập thì mặc định là /signin-google)
	googleOptions.CallbackPath = "/dang-nhap-bang-google";
});


builder.Services.AddRazorPages();
               
builder.Services.AddScoped<ICategory, EFCategory>();
builder.Services.AddScoped<INews, EFNews>();
builder.Services.AddScoped<IPost, EFPost>();
builder.Services.AddScoped<IProductCategory, EFProductCategory>();
builder.Services.AddScoped<IProduct, EFProduct>();
builder.Services.AddScoped<ICheckerRepository, EFCheckerRepository>();
builder.Services.AddScoped<IUserRepository, EFUserRepository>();
builder.Services.AddScoped<IProductComment, EFProductComment>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseSession();
app.UseRouting();
app.UseAuthentication(); ;

app.UseAuthorization();

app.MapRazorPages();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(name: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(name: "Checker",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(name: "User",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
