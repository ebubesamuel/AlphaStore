using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AlphaStore.Infrastructure.Database.Configuration;
using AlphaStore.Web.Middlewares;
using AlphaStore.Application.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<DatabaseContext>(options =>
//options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStr")));
builder.Services.AddDatabase(builder.Configuration.GetSection("DatabaseSettings").GetValue<string>("ConnectionString"), typeof(AlphaStore.Infrastructure.Database.Configuration.ApplicationConfiguration).Assembly).AddControllersWithViews();
builder.Services.AddServices();
builder.Services.AddMiddlewares();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});



app.UseAuthorization();
app.UseCustomMiddleware();
app.UseMigrations();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

