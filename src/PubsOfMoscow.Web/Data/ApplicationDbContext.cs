using Microsoft.EntityFrameworkCore;
using PubsOfMoscow.Web.Models;

namespace PubsOfMoscow.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Pub>()
                .HasOne(p => p.Round)
                .WithMany(r => r.Pubs);
        }


        public DbSet<Congratulation> Congratulations { get; set; }
        public DbSet<Pub> Pubs { get; set; }
        public DbSet<Round> Rounds { get; set; }
    }
}
