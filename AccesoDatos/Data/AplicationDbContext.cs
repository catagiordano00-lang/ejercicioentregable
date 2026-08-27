using AccesoDatos.Models;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Data
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(
            DbContextOptions<AplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Autor> Autores { get; set; }

        public DbSet<Libro> Libros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Libro>()
                .HasOne(l => l.Autor)
                .WithMany(a => a.Libros)
                .HasForeignKey(l => l.AutorId);
        }
    }
}