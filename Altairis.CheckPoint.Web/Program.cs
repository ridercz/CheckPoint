using Altairis.CheckPoint.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add CheckPointDbContext
builder.Services.AddDbContext<CheckPointDbContext>(options => {
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"), x => x.UseNetTopologySuite());
});




var app = builder.Build();
app.MapGet("/", () => "Hello World!");

app.Run();
