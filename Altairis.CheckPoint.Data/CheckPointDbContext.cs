using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Altairis.CheckPoint.Data;

public class CheckPointDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid> {

    public CheckPointDbContext(DbContextOptions<CheckPointDbContext> options) : base(options) { }

    public DbSet<Event> Events => this.Set<Event>();

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder) {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.Properties<Suid>()
            .HaveConversion<CastingConverter<Suid, string>>()
            .HaveMaxLength(12).AreFixedLength()
            .AreUnicode(false);
    }
}
