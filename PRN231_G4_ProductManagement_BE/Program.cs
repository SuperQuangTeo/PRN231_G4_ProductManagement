using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PRN231_G4_ProductManagement_BE.AutoMapping;
using PRN231_G4_ProductManagement_BE.Models;
using PRN231_G4_ProductManagement_BE.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
//==============================================================
builder.Services.AddDbContext<PRN231_Product_ManagementContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("NorthwindCS"));
});
//==============================================================

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(1800);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
//==============================================================
builder.Services.AddAutoMapper(typeof(MappingProfile));
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new UserMapping());
    mc.AddProfile(new RoleMapping());
    mc.AddProfile(new StoreMapping());
    mc.AddProfile(new SupplierMapping());
    mc.AddProfile(new CategoryMapping());
    mc.AddProfile(new UnitMapping());
    mc.AddProfile(new ProductMapping());

});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
//==============================================================

builder.Services.AddCors();

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
app.UseCors(o =>
o.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.MapControllers();

app.Run();
