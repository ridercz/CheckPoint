using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Altairis.CheckPoint.Data;

public class CheckPointDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid> {

    public CheckPointDbContext(DbContextOptions<CheckPointDbContext> options) : base(options) { }

}
