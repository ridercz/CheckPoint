using Altairis.CheckPoint.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

// Create builder
var builder = WebApplication.CreateBuilder(args);

// Add CheckPointDbContext
builder.Services.AddDbContext<CheckPointDbContext>(options => {
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 12;
}).AddEntityFrameworkStores<CheckPointDbContext>();

// Build app
var app = builder.Build();

// Initialize database
using (var scope = app.Services.CreateScope()) {
    // Migrate database
    var dc = scope.ServiceProvider.GetRequiredService<CheckPointDbContext>();
    dc.Database.Migrate();

    // Create admin user if none exists
    if (!dc.Users.Any()) {
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var adminUser = new ApplicationUser { UserName = "admin" };
        // Using fixed password is a security risk, we are doing it only for demo purposes
        var result = await userManager.CreateAsync(adminUser, "pass.word123");
        if (result.Succeeded) {
            app.Logger.LogInformation("Created admin user with ID {userid}.", adminUser.Id);
        } else {
            app.Logger.LogError("Cannot create admin user: {errors}", string.Join(", ", result.Errors.Select(x => x.Description)));
        }
    }

    // Create some sample data
    var newEvent = new Event { Name = "Test event", DateStart = DateTime.Now };
    newEvent.Competitors.Add(new Competitor { Name = "John Doe", DateCreated = DateTime.Now });
    newEvent.Competitors.Add(new Competitor { Name = "Jane Roe", DateCreated = DateTime.Now });
    newEvent.Checkpoints.Add(new Checkpoint { Name = "Start", SequenceNumber = 1, Type = CheckpointType.Start });
    newEvent.Checkpoints.Add(new Checkpoint { Name = "Checkpoint 1", SequenceNumber = 2, Type = CheckpointType.Mandatory });
    newEvent.Checkpoints.Add(new Checkpoint { Name = "Checkpoint 2", SequenceNumber = 3, Type = CheckpointType.Optional });
    newEvent.Checkpoints.Add(new Checkpoint { Name = "Finish", SequenceNumber = 4, Type = CheckpointType.Finish });
    await dc.Events.AddAsync(newEvent);
    await dc.SaveChangesAsync();
}

app.Run();
