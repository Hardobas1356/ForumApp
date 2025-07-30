using ForumApp.Data;
using ForumApp.Data.Models;
using ForumApp.Services.Core;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.Infrastructure.Middleware;
using ForumApp.Web.Seeders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using static ForumApp.GCommon.ValidationConstants.ApplicationUserConstants;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder
    .Configuration
    .GetConnectionString("DefaultConnection") 
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.
    Services
    .AddDbContext<ForumAppDbContext>(options =>
    {
        options.UseSqlServer(connectionString);
    });

builder
    .Services
    .AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
    {
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = PasswordMinLength;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    })
    .AddEntityFrameworkStores<ForumAppDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Error/403";
});

builder
    .Services
    .AddControllersWithViews();

builder
    .Services
    .AddRazorPages();

builder
    .Services
    .AddTransient<AdminSeeder>();

builder
    .Services
    .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder
    .Services
    .AddScoped<IBoardService, BoardService>();
builder
    .Services
    .AddScoped<IPostService, PostService>();
builder
    .Services
    .AddScoped<IReplyService, ReplyService>();
builder
    .Services
    .AddScoped<ICategoryService, CategoryService>();
builder
    .Services
    .AddScoped<ITagService, TagService>();
builder
    .Services
    .AddScoped<IPermissionService, PermissionService>();
builder
    .Services
    .AddScoped<IApplicationUserService, ApplicationUserService>();

WebApplication app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<AdminSeeder>();
    await seeder.SeedAsync();
}

app.UseExceptionHandler("/Error/500");
app.UseStatusCodePagesWithReExecute("/Error/{0}");
app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<VerifyUserNotDeleted>();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Board}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
