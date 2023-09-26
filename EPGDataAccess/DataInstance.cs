using Microsoft.EntityFrameworkCore;
using EPGDomain;
using Microsoft.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EPGDataAccess
{
    public class DataInstance : IdentityDbContext<ServiceUser, Role, string>
    {
        public DataInstance(DbContextOptions<DataInstance> options) : base(options) { }
        public DataInstance() { }
        public DbSet<Work> Works { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ServiceUser> ServiceUsers { get; set; }
        public override DbSet<Role> Roles { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=EPGProjectDatabase;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True").LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Role>().HasData(new List<Role>()
            {
                new Role
                {
                    Id = Guid.NewGuid().ToString(), Name = "User", NormalizedName = "USER"
                },
                new Role
                {
                    Id = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "ADMIN"
                }
            });
            
        }
    }
}