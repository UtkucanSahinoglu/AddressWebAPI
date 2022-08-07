using AddressWebApi.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AddressWebApi.Data
{
    public class ApiDbContext : DbContext
    {

        public DbSet<User>? users { get; set; }
        public DbSet<Address>? addresses { get; set; }

        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddDebug().AddConsole());
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=AdresApiDB;Trusted_Connection=True;MultipleActiveResultSets=True;")
                .UseLoggerFactory(loggerFactory);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
        public static void TableBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName);
                entity.Property(e => e.LastName);
                //entity.HasMany(e => e.Addresses).WithOne(e => e.user).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(e => e.Addresses).WithOne(e => e.user).HasForeignKey(e => e.UserId);
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name);
                entity.Property(e => e.OpenAddress);
               // entity.HasOne(e => e.user).WithMany(e =>e.Addresses).OnDelete(DeleteBehavior.ClientCascade);
               // entity.HasOne(e => e.user).WithMany(e => e.Addresses).HasForeignKey(e => e.UserId);
            });
        }
    }
}
