using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VeebiPood.Data;
using VeebiPood.Models;

namespace VeebiPood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public List<Order> GetOrders()
        {
            var orders = _context.Orders.ToList();
            return orders;
        }
        [HttpPost]
        public List<Order> PostOrder([FromBody] Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return _context.Orders.ToList();
        }
        [HttpDelete("{id}")]
        public List<Order> DeleteOrder(int id)
        {
            var order = _context.Orders.Find(id);

            if (order == null)
            {
                return _context.Orders.ToList();
            }

            _context.Orders.Remove(order);
            _context.SaveChanges();
            return _context.Orders.ToList();
        }
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            var order = _context.Orders.Find(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        [HttpPut("{id}")]
        public ActionResult<List<Order>> PutOrder(int id, [FromBody] Order updatedOrder)
        {
            var order = _context.Orders.Find(id);

            if (order == null)
            {
                return NotFound();
            }

            order.created = updatedOrder.created;
            order.TotalSum = updatedOrder.TotalSum;
            order.Paid = updatedOrder.Paid;
            order.CartProduct = updatedOrder.CartProduct;
            order.Person = updatedOrder.Person;



            _context.Orders.Update(order);
            _context.SaveChanges();

            return Ok(_context.Orders);
        }
    }
}
