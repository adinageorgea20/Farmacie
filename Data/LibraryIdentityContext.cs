using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Farmacie.Data
{
    public class LibraryIdentityContext : IdentityDbContext<IdentityUser>
    {
        public LibraryIdentityContext(DbContextOptions<LibraryIdentityContext> options)
            : base(options)
        {
        }
    }
}
