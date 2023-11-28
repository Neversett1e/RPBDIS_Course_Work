using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using WebApplication3;
using WebApplication3.Data;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
/*builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer("Data Source=COLORBLIND\\SQLEXPRESS; Database=LibraryDB;Integrated Security=True;Encrypt=False;"));*/
builder.Services.AddDbContext<LibraryDBContext>(options => options.UseSqlServer("Data Source=COLORBLIND\\SQLEXPRESS; Database=LibraryDB;Integrated Security=True;Encrypt=False;"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<LibraryDBContext>().AddDefaultUI()
                .AddDefaultTokenProviders(); ;

builder.Services.AddControllersWithViews(options => {
    options.CacheProfiles.Add("ModelCache",
        new CacheProfile()
        {
            Location = ResponseCacheLocation.Any,
            Duration = 2 * 14 + 240
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
