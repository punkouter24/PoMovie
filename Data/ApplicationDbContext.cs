using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PoMovie.Models;

namespace PoMovie.Data
{
    // public class ApplicationDbContext : IdentityDbContext
    // {
    //     public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    //         : base(options)
    //     {
    //     }

    //     public DbSet<Movie> Movies { get; set; }
    // }


    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Movie> Movies { get; set; }
    }
}