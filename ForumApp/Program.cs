using ForumApp.Data;
using ForumApp.Data.Models;
using ForumApp.Services.Core;
using ForumApp.Services.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder
    .Configuration
    .GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.
    Services
    .AddDbContext<ForumAppDbContext>(options =>
        options
        .UseSqlServer(connectionString));
builder
    .Services
    .AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.Password.RequireDigit = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 5;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    })
    .AddEntityFrameworkStores<ForumAppDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

builder
    .Services
    .AddControllersWithViews();

builder
    .Services
    .AddRazorPages();

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
    name: "areas",
    pattern: "{area:exists}/{controller=Board}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
