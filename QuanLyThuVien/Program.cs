using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuanLyThuVien.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<QuanLyThuVienContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QuanLyThuVienContext") ?? throw new InvalidOperationException("Connection string 'QuanLyThuVienContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
