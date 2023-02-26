using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RRHH.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RRHHContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RRHHContext")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope()) {
    var services = scope.ServiceProvider;
    try {
        var context = services.GetRequiredService<RRHHContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex) {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Un error ha ocurrido mientras se inicializaba la base de datos.");
    }
}

app.Run();
