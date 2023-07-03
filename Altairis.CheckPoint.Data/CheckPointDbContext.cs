global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;
global using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Altairis.CheckPoint.Data;

public class CheckPointDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Suid> {

    public CheckPointDbContext(DbContextOptions<CheckPointDbContext> options) : base(options) { }

    public DbSet<Event> Events => this.Set<Event>();

    public DbSet<Competitor> Competitors => this.Set<Competitor>();

    public DbSet<Checkpoint> Checkpoints => this.Set<Checkpoint>();

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder) {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.Properties<Suid>()
            .HaveConversion<CastingConverter<Suid, string>>()
            .HaveMaxLength(Suid.Length).AreFixedLength()
            .AreUnicode(false);
    }
}
