using Microsoft.EntityFrameworkCore;
using Practice.Repository.Entity;

namespace Practice.Repository
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

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Product 1",
                    Price = 100,
                    Description = "Product 1 Description"
                },
                new Product
                {
                    Id = 2,
                    Name = "Product 2",
                    Price = 200,
                    Description = "Product 2 Description"
                }
            );
        }
    }
}
