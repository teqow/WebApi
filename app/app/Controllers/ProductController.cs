using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductRepostiory _productRepostiory;

        public ProductController(IProductRepostiory productRepostiory) =>
            _productRepostiory = productRepostiory;

        [HttpGet]
        public IEnumerable<Product> Get() => _productRepostiory.Products;

        [HttpGet("{id}")]
        public Product Get(int id)
        {
            var product = _productRepostiory.Products.FirstOrDefault(p => p.Id == id);
            return product;
        }

        [HttpPost]
        public Product Post([FromBody] Product product)
        {
            _productRepostiory.AddProduct(
                new Product
                {
                    Name = product.Name,
                    Price = product.Price
                });
            return product;
        }

        [HttpPut]
        public Product Put([FromBody] Product product)
        {
            _productRepostiory.UpdateProduct(product);
            return product;
        }

        [HttpPatch("{id}")]
        public StatusCodeResult Patch(int id, [FromBody] JsonPatchDocument<Product> patch)
        {
            Product product = Get(id);
            if (product != null)
            {
                patch.ApplyTo(product);
                return Ok();
            }

            return NotFound();
        }

        [HttpDelete]
        public void Delete(int id) => _productRepostiory.DeleteProduct(id);
    }
}