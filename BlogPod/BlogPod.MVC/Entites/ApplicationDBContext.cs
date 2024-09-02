using Microsoft.EntityFrameworkCore;

namespace BlogPod.MVC.Entites
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<CityEntity> BlogValue { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 3));

            optionsBuilder.UseMySql("server=localhost;database=blog;user=root;password=root", serverVersion);
            base.OnConfiguring(optionsBuilder);
        }



    }
}
