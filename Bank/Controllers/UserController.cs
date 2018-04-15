using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bank.BankDbContext;
using BankModels;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bank.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly BankContext _context;

        public UserController(BankContext context)
        {
            _context = context;

            if (_context.Users.Count() == 0)
            {
                _context.Users.Add(new User
                {
                    Name = "cambianica",
                    Firstname = "bruno",
                    Mail = "bruno.cambianica@gmail.com",
                    Password = "bruno",
                    Balance = 50000,
                    RIB = "50387530367",
                    UpdatedAt = DateTime.Now,
                    Deleted = false

                });
                _context.Users.Add(new User
                {
                    Name = "wampach",
                    Firstname = "yoann",
                    Mail = "yoann@gmail.com",
                    Password = "yoann",
                    Balance = 50000,
                    RIB = "50387530368",
                    UpdatedAt = DateTime.Now,
                    Deleted = false
                });
                _context.SaveChanges();
            }
        }

        // restapi/user 
        [HttpGet]
        //[Authorize("admin")]
        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        // restapi/user/{id}
        [HttpGet("{id}", Name = "GetUser")]
        //[Authorize("admin")]
        public IActionResult GetById(Guid id)
        {
            var item = _context.Users.FirstOrDefault(t => t.ID == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        //[Authorize("admin")]
        public IActionResult Create([FromBody] User item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.UpdatedAt = DateTime.Now;

            _context.Users.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetUser", new { id = item.ID }, item);
        }

        //update
        [HttpPut("{id}")]
        //[Authorize("admin")]
        public IActionResult Update(Guid id, [FromBody] User item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var user = _context.Users.FirstOrDefault(t => t.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            user.ID = item.ID;
            user.Name = item.Name;
            user.Firstname = item.Firstname;
            user.Mail = item.Mail;
            user.Password = item.Password;
            user.RIB = item.RIB;
            user.BirthDate = item.BirthDate;
            user.Balance = item.Balance;
            user.UpdatedAt = DateTime.Now;
            if (item.Deleted == true)
            {
                user.Deleted = true;
                user.DeletedAt = DateTime.Now;
            }
            else
            {
                user.Deleted = false;
                user.DeletedAt = null;
            }
            _context.SaveChanges();
            return new NoContentResult();

        }

        [HttpDelete("{id}")]
        [Authorize("admin")]
        public IActionResult Delete(Guid id)
        {
            var user = _context.Users.FirstOrDefault(t => t.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
            return new NoContentResult();
        }

        //view all the claims of the token
        //[HttpGet("claims")]
        //public IActionResult Claims()
        //{
        //    return Json(User.Claims.Select(c =>
        //        new
        //        {
        //            c.Type,
        //            c.Value
        //        }));
        //}
    }
}
