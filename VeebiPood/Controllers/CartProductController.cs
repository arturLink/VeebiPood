using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VeebiPood.Data;
using VeebiPood.Models;

namespace VeebiPood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CartProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public List<CartProduct> GetCartProduct()
        {
            var cartproduct = _context.CartProducts.ToList();
            return cartproduct;
        }
        [HttpPost]
        public List<CartProduct> PostCartProduct([FromBody] CartProduct cartproduct)
        {
            _context.CartProducts.Add(cartproduct);
            _context.SaveChanges();
            return _context.CartProducts.ToList();
        }
        [HttpDelete("{id}")]
        public List<CartProduct> DeleteCartProduct(int id)
        {
            var cartproduct = _context.CartProducts.Find(id);

            if (cartproduct == null)
            {
                return _context.CartProducts.ToList();
            }

            _context.CartProducts.Remove(cartproduct);
            _context.SaveChanges();
            return _context.CartProducts.ToList();
        }
        [HttpGet("{id}")]
        public ActionResult<CartProduct> GetCartProduct(int id)
        {
            var cartproduct = _context.CartProducts.Find(id);

            if (cartproduct == null)
            {
                return NotFound();
            }

            return cartproduct;
        }

        [HttpPut("{id}")]
        public ActionResult<List<CartProduct>> PutCartProducts(int id, [FromBody] CartProduct updatedCartProduct)
        {
            var cartproduct = _context.CartProducts.Find(id);

            if (cartproduct == null)
            {
                return NotFound();
            }

            cartproduct.ProductId = updatedCartProduct.ProductId;
            cartproduct.Quantity = updatedCartProduct.Quantity;

            _context.CartProducts.Update(cartproduct);
            _context.SaveChanges();

            return Ok(_context.CartProducts);
        }
    }
}
