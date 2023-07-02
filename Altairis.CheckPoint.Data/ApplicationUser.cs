using Microsoft.AspNetCore.Identity;

namespace Altairis.CheckPoint.Data;

public class ApplicationUser : IdentityUser<Guid> {

}

public class ApplicationRole : IdentityRole<Guid> {

}