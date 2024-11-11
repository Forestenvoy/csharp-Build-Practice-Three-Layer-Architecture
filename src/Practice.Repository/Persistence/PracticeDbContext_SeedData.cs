using Microsoft.EntityFrameworkCore;
using Practice.Repository.Entities;

namespace Practice.Repository.Persistence
{
    public partial class PracticeDbContext : DbContext
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    Id = 1,
                    Account = "admin",
                    Password = "test123"
                }
            );
        }
    }
}
