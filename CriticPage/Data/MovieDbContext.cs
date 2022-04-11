using CriticPage.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CriticPage.Data
{
    public class MovieDbContext : IdentityDbContext<IdentityUser>
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDbFunction(typeof(MovieDbContext).GetMethod(nameof(GetMode), new[] { typeof(int) }));
            modelBuilder.HasDbFunction(typeof(MovieDbContext).GetMethod(nameof(GetStDev), new[] { typeof(int) }));


        }
        [DbFunction(Schema = "dbo", Name = "GetMode")]
        public static int GetMode(int MovieId)
        {
            throw new NotImplementedException();
        }
        [DbFunction(Schema = "dbo", Name = "GetStDev")]
        public static double GetStDev(int MovieId)
        {
            throw new NotImplementedException();
        }

        public DbSet<Movie> Movie { get; set; }
        public DbSet<MovieScore> MovieScore { get; set; }
        public DbSet<Admin> Admin { get; set; }
    }
}
