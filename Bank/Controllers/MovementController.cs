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
    public class MovementController : Controller
    {
        private readonly BankContext _context;

        public MovementController(BankContext context)
        {
            _context = context;

            if (_context.Movements.Count() == 0)
            {
                var bruno = _context.Users.FirstOrDefault(t => t.Name == "Cambianica");
                var yoann = _context.Users.FirstOrDefault(t => t.Name == "Wampach");

                _context.Movements.Add(new Movement
                {
                    Amount = 50,
                    Message = "Frais d'amitié",
                    DebitID = yoann.ID,
                    CreditID = bruno.ID,
                    UpdatedAt = DateTime.Now,
                    Deleted = false
                });
                _context.SaveChanges();
            }
        }

        // restapi/user 
        [HttpGet]
        //[Authorize("admin")]
        //[AllowAnonymous]
        public IEnumerable<Movement> GetAll()
        {
            return _context.Movements.ToList();
        }

        // restapi/movement/{id}
        [HttpGet("{id}", Name = "GetMovement")]
        //[Authorize("user")]
        public IActionResult GetById(Guid id)
        {
            var item = _context.Movements.FirstOrDefault(t => t.ID == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        
        // restapi/movement/id/{id}
        [HttpGet]
        [Route("id/{id}")]
        //[Authorize("user")]
        public IActionResult GetByAccount(Guid id)
        {
            //var item = _context.Movements.FirstOrDefault(t => t.CreditID == id || t.DebitID == id);
            IEnumerable<Movement> itemslist = _context.Movements.ToList().Where(t => t.CreditID == id || t.DebitID == id);

            if (itemslist == null)
            {
                return NotFound();
            }
            return Json(itemslist);
        }

        // restapi/movement/debit/{id}
        //[HttpGet]
        //[Route("debit/{id}")]
        //[Authorize("read")]
        //public IActionResult GetByAccountDebit(Guid id)
        //{
        //    IEnumerable<Movement> itemslist = _context.Movements.ToList().Where(t => t.DebitID == id);

        //    if (itemslist == null)
        //    {
        //        return NotFound();
        //    }
        //    return Json(itemslist);
        //}

        // restapi/movement/credit/{id}
        //[HttpGet]
        //[Route("credit/{id}")]
        //[Authorize("read")]
        //public IActionResult GetByAccountCredit(Guid id)
        //{
        //    IEnumerable<Movement> itemslist = _context.Movements.ToList().Where(t => t.CreditID == id);

        //    if (itemslist == null)
        //    {
        //        return NotFound();
        //    }
        //    return Json(itemslist);
        //}


        //création mouvement 
        [HttpPost]
        //[Authorize("user")]
        public IActionResult Create([FromBody] Movement item )
        {
            //mouvement
            if (item == null)
            {
                return BadRequest();
            }

            item.UpdatedAt = DateTime.Now;

            _context.Movements.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetMovement", new { id = item.ID }, item);
        }

        [HttpPut("{id}")]
        //[Authorize("admin")]
        public IActionResult Update(Guid id, [FromBody] Movement item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var movement = _context.Movements.FirstOrDefault(t => t.ID == id);
            if (movement == null)
            {
                return NotFound();
            }

            movement.ID = item.ID;
            movement.Amount = item.Amount;
            movement.CreditID = item.CreditID;
            movement.DebitID = item.CreditID;
            movement.DebitID = item.DebitID;
            movement.CreditID = item.CreditID;
            movement.Message = item.Message;
            movement.UpdatedAt = DateTime.Now;
            if (item.Deleted == true)
            {
                movement.Deleted = true;
                movement.DeletedAt = DateTime.Now;
            }

            _context.Movements.Update(movement);
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

    }
}
