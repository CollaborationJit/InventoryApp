using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Models;
using Library.Models;

namespace InventoryHelper.Controllers
{
    [Produces("application/json")]
    [Route("api/Contacts")]
    public class ContactsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Contacts
        [HttpGet]
        public IEnumerable<Contact> GetContacts()
        {
            return _context.Contacts;
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetContact([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contact = await _context.Contacts.SingleOrDefaultAsync(m => m.Email == id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        // PUT: api/Contacts/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutContact([FromBody] string email, [FromBody] ContactInModel contactModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Contact contact = new Contact
            {
                Email = contactModel.Email,
                FirstName = contactModel.FirstName,
                LastName = contactModel.LastName,
                PhoneNumber = contactModel.PhoneNumber
            };

            _context.Entry(contact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(email))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Contacts
        [HttpPost]
        public async Task<ActionResult<ContactOutModel>> PostContact([FromBody] ContactInModel contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Contact postVal = new Contact
            {
                PhoneNumber = contact.PhoneNumber,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email
            };

            _context.Contacts.Add(postVal);
            await _context.SaveChangesAsync();

            ContactOutModel outputContact = new ContactOutModel(postVal);

            return CreatedAtAction("GetContact", new { id = contact.Email }, outputContact);
        }

        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteContact([FromBody] string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Contact contact = await _context.Contacts.SingleOrDefaultAsync(m => m.Email == email);

            if (contact == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return Ok(contact);
        }

        private bool ContactExists(string id)
        {
            return _context.Contacts.Any(e => e.Email == id);
        }
    }
}