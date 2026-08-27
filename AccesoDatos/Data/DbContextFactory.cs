using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;
using System.Linq;

namespace AccesoDatos.Data
{
    public class AplicationDbContextFactory
        : IDesignTimeDbContextFactory<AplicationDbContext>
    {
        public AplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AplicationDbContext>();
            optionsBuilder.UseSqlite("Data Source=" + GetDbPath());
            return new AplicationDbContext(optionsBuilder.Options);
        }

        private static string GetDbPath()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            var dir = new DirectoryInfo(basePath);

            while (dir != null && !dir.GetDirectories("Migrations").Any())
            {
                dir = dir.Parent;
            }

            if (dir == null)
                return Path.Combine(basePath, "biblioteca.db");

            return Path.Combine(dir.FullName, "biblioteca.db");
        }
    }
}
