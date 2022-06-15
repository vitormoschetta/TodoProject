using Microsoft.EntityFrameworkCore;
using Todo.Api;
using Todo.Infrastructure.Database.Context;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

// Apply Migrations
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (dataContext.Database.IsRelational() && dataContext.Database.GetPendingMigrations().Any())
        dataContext.Database.Migrate();
}

app.Run();
