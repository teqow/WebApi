using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Models
{
    public class ProductRepository : IProductRepostiory
    {
        private readonly AppDbContext _appDbContext;

        public ProductRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Product> Products => _appDbContext.Products;

        public Product AddProduct(Product product)
        {
            if(product.Id == 0)
            {
                _appDbContext.Products.Add(product);
                _appDbContext.SaveChanges();
            }

            return product;
        }

        public void DeleteProduct(int id)
        {
            Product dbProduct = _appDbContext.Products.FirstOrDefault(p => p.Id == id);
            if (dbProduct != null)
            {
                _appDbContext.Remove(dbProduct);
                _appDbContext.SaveChanges();
            }
        }

        public Product UpdateProduct(Product product) =>
            AddProduct(product);
    }
}
