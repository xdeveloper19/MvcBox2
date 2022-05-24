using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Entities.Context
{
    public class ApplicationUserContext: IdentityDbContext<User>
    {
        public ApplicationUserContext(DbContextOptions<ApplicationUserContext> options) : base(options)
        {
           //Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
