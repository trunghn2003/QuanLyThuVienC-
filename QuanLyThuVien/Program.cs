
using System.Reflection;
using Microsoft.EntityFrameworkCore;

using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;
using QuanLyThuVien.Repositories;
using QuanLyThuVien.Services;
using System.Text;
using QuanLyThuVien.Profiles;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<QuanLyThuVienContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QuanLyThuVienContext") ?? throw new InvalidOperationException("Connection string 'QuanLyThuVienContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAuthentication()
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
            ValidAudience = builder.Configuration["JwtConfig:Audience"],
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes((builder.Configuration["JwtConfig:SecretKey"]))),
            ClockSkew = TimeSpan.Zero
        };

    });
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("admin", p =>
    {
        p.RequireRole("admin");
    });
builder.Services.AddScoped<UnitOfWork>();

builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddScoped<IAuthorService, AuthorService>();


builder.Services.AddScoped<IGenreService, GenreService>();

builder.Services.AddScoped<IBorrowingService, BorrowingService>();

builder.Services.AddScoped<IBorrowedBookService, BorrowedBookService>();

builder.Services.AddScoped<IStatisticsBorrowedBookService, StatisticsBorrowedBookService>();


//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddAutoMapper(typeof(Program).Assembly);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:3000") 
           .AllowAnyHeader()
           .AllowAnyMethod();
});
app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
