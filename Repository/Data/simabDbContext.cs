using Domain.Entities;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Repository.Data
{
    public partial class simabDbContext : DbContext
    {
        public simabDbContext()
        {
        }

        public simabDbContext(DbContextOptions<simabDbContext> options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("tUser");
                
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .IsUnicode(true);
                

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
