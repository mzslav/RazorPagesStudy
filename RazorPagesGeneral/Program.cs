using Microsoft.EntityFrameworkCore;
using RazorPagesStudy.Services;

var builder = WebApplication.CreateBuilder(args);

// Доступ до конфігурації через builder.Configuration
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorPages();


builder.Services.AddScoped<IBookRepository, SQLBookRepository>();

// Додати конфігурацію для DbContextPool
builder.Services.AddDbContextPool<AppDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("BookDBConnection"));
});

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
    options.AppendTrailingSlash = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
