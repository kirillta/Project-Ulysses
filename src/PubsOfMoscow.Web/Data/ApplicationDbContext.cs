using Microsoft.EntityFrameworkCore;
using PubsOfMoscow.Web.Models;

namespace PubsOfMoscow.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public DbSet<Congratulation> Congratulations { get; set; }
    }
}
