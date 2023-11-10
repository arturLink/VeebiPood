using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VeebiPood.Data;
using VeebiPood.Models;

namespace VeebiPood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public List<Product> GetProduct()
        {
            var product = _context.Products.ToList();
            return product;
        }
        [HttpPost("add")]
        public List<Product> PostProduct([FromBody] Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return _context.Products.ToList();
        }
        [HttpDelete("delete/{id}")]
        public List<Product> DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return _context.Products.ToList();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            return _context.Products.ToList();
        }
        [HttpGet("select/{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPut("update/{id}")]
        public ActionResult<List<Product>> PutProducts(int id, [FromBody] Product updatedProduct)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.Image = updatedProduct.Image;
            product.Active = updatedProduct.Active;
            product.Stock = updatedProduct.Stock;
            product.CategoryId = updatedProduct.CategoryId;

            _context.Products.Update(product);
            _context.SaveChanges();

            return Ok(_context.Products);
        }
    }
}
