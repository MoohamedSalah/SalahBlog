using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SalahBlog.Models;

namespace SalahBlog.Data
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) { }

        public DbSet<ApplicationUser>? ApplicationUsers { get; set; }
        public DbSet<Post>? Post { get; set; }
        public DbSet<Page>? Page { get; set; }
    }
}
