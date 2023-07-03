using Microsoft.AspNetCore.Identity;

namespace Altairis.CheckPoint.Data;

public class ApplicationUser : IdentityUser<Suid> {

    [Key]
    public override Suid Id { get; set; } = Suid.NewSuid();

}

public class ApplicationRole : IdentityRole<Suid> {

    [Key]
    public override Suid Id { get; set; } = Suid.NewSuid();

}