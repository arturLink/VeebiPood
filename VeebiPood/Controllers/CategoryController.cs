using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VeebiPood.Data;
using VeebiPood.Models;

namespace VeebiPood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public List<Category> GetCategories()
        {
            var cat = _context.Categories.ToList();
            return cat;
        }
        [HttpPost]
        public List<Category> PostCategory([FromBody] Category cat)
        {
            _context.Categories.Add(cat);
            _context.SaveChanges();
            return _context.Categories.ToList();
        }
        [HttpDelete("{id}")]
        public List<Category> DeleteCategory(int id)
        {
            var cat = _context.Categories.Find(id);

            if (cat == null)
            {
                return _context.Categories.ToList();
            }

            _context.Categories.Remove(cat);
            _context.SaveChanges();
            return _context.Categories.ToList();
        }
        [HttpGet("{id}")]
        public ActionResult<Category> GetCategory(int id)
        {
            var cat = _context.Categories.Find(id);

            if (cat == null)
            {
                return NotFound();
            }

            return cat;
        }

        [HttpPut("{id}")]
        public ActionResult<List<Category>> PutCategory(int id, [FromBody] Category updatedCat)
        {
            var cat = _context.Categories.Find(id);

            if (cat == null)
            {
                return NotFound();
            }

            cat.Name = updatedCat.Name;

            _context.Categories.Update(cat);
            _context.SaveChanges();

            return Ok(_context.Categories);
        }
    }
}
