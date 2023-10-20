using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VeebiPood.Data;
using VeebiPood.Models;

namespace VeebiPood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public List<Person> GetPersons()
        {
            var persons = _context.Persons.ToList();
            return persons;
        }
        [HttpPost]
        public List<Person> PostPerson([FromBody] Person person)
        {
            _context.Persons.Add(person);
            _context.SaveChanges();
            return _context.Persons.ToList();
        }
        [HttpDelete("{id}")]
        public List<Person> DeletePerson(int id)
        {
            var person = _context.Persons.Find(id);

            if (person == null)
            {
                return _context.Persons.ToList();
            }

            _context.Persons.Remove(person);
            _context.SaveChanges();
            return _context.Persons.ToList();
        }
        [HttpGet("{id}")]
        public ActionResult<Person> GetPerson(int id)
        {
            var persons = _context.Persons.Find(id);

            if (persons == null)
            {
                return NotFound();
            }

            return persons;
        }

        [HttpPut("{id}")]
        public ActionResult<List<Person>> PutPerson(int id, [FromBody] Person updatedPerson)
        {
            var persons = _context.Persons.Find(id);

            if (persons == null)
            {
                return NotFound();
            }

            persons.FirstName = updatedPerson.FirstName;
            persons.LastName = updatedPerson.LastName;
            persons.Address = updatedPerson.Address;
            persons.Phone = updatedPerson.Phone;
            persons.Password = updatedPerson.Password;
            persons.PersonCode = updatedPerson.PersonCode;
            persons.Admin = updatedPerson.Admin;

            _context.Persons.Update(persons);
            _context.SaveChanges();

            return Ok(_context.Persons);
        }
    }
}
