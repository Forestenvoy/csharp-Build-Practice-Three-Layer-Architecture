using Practice.Repository.Entities;
using Practice.Repository.Interfaces;

namespace Practice.Repository.Implements
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(PracticeDbContext context) : base(context)
        {
        }



    }
}
