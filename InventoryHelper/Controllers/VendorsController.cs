using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Models;

namespace InventoryHelper.Controllers
{
    [Produces("application/json")]
    [Route("api/Vendors")]
    public class VendorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VendorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Vendors
        [HttpGet]
        public IEnumerable<Vendor> GetVendors()
        {
            return _context.Vendors
                .Include(s => s.Contact);
        }

        // GET: api/Vendors/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetVendor([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vendor = await _context.Vendors.SingleOrDefaultAsync(m => m.Name == id);

            if (vendor == null)
            {
                return NotFound();
            }

            return Ok(vendor);
        }

        // PUT: api/Vendors/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutVendor([FromRoute] string id, [FromBody] VendorInModel vendorInModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vendorInModel.Name)
            {
                return BadRequest();
            }

            Contact contact = await _context.Contacts.FirstOrDefaultAsync(m => m.Email == vendorInModel.ContactEmail);

            Vendor vendor = new Vendor
            {
                Contact = contact,
                Name = vendorInModel.Name
            };

            _context.Entry(vendor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorExists(id))
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

        // POST: api/Vendors
        [HttpPost]
        public async Task<ActionResult> PostVendor([FromBody] VendorInModel vendorModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Contact contact = await _context.Contacts.SingleOrDefaultAsync(m => m.Email == vendorModel.ContactEmail);
            Vendor vendor = new Vendor
            {
                Contact = contact,
                Name = vendorModel.Name
            };

            _context.Vendors.Add(vendor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendor", new { id = vendor.Name }, vendor);
        }

        // DELETE: api/Vendors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVendor([FromBody] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vendor = await _context.Vendors.SingleOrDefaultAsync(m => m.Name == id);
            if (vendor == null)
            {
                return NotFound();
            }

            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();

            return Ok(vendor);
        }

        private bool VendorExists(string id)
        {
            return _context.Vendors.Any(e => e.Name == id);
        }
    }
}