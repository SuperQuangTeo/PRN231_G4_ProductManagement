using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PRN231_G4_ProductManagement_BE.AutoMapping;
using PRN231_G4_ProductManagement_BE.Models;
using PRN231_G4_ProductManagement_BE.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSession();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.Cookie.Name = "YourCookieName";
            options.LoginPath = "/Login"; // Đường dẫn đến trang đăng nhập
            options.AccessDeniedPath = "/AccessDenied"; // Đường dẫn đến trang từ chối truy cập
        });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy =>
    {
        policy.RequireRole("Admin");
    });
    options.AddPolicy("RequireMemberRole", policy =>
    {
        policy.RequireRole("Member");
    });
});
builder.Services.AddDbContext<PRN231_Product_ManagementContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("NorthwindCS"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
