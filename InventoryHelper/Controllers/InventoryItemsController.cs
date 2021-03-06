﻿using System;
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
    [Route("api/InventoryItems")]
    public class InventoryItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InventoryItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/InventoryItems
        [HttpGet]
        public IEnumerable<InventoryItem> GetItems()
        {
            return _context.Items
                .Include(s => s.Vendor)
                .Include(s => s.Vendor.Contact);
        }

        // GET: api/InventoryItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetInventoryItem([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Guid guid = new Guid(id);
            InventoryItem inventoryItem = await _context.Items
                .Where(s => s.Guid == guid)
                .Include(s => s.Vendor)
                .SingleOrDefaultAsync();

            if (inventoryItem == null)
            {
                return NotFound();
            }

            return Ok(inventoryItem);
        }

        // PUT: api/InventoryItems/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutInventoryItem([FromBody] Guid id, [FromBody] InventoryInModel inventoryModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Vendor vendor = await _context.Vendors.SingleOrDefaultAsync(s => s.Name == inventoryModel.VendorName);

            InventoryItem inventoryItem = new InventoryItem
            {
                Guid = new Guid(inventoryModel.Guid),
                SerialNo = inventoryModel.SerialNo,
                Quantity = inventoryModel.Quantity,
                QuantityAvailable = inventoryModel.QuantityAvailable,
                Title = inventoryModel.Title,
                Type = InventoryItem.GetItemType(inventoryModel.Type),
                Vendor = vendor
            };

            if (id != inventoryItem.Guid)
            {
                return BadRequest();
            }

            _context.Entry(inventoryItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryItemExists(id))
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

        // POST: api/InventoryItems
        [HttpPost]
        public async Task<ActionResult> PostInventoryItem([FromBody] InventoryInModel inventoryModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Vendor vendor = await _context.Vendors.SingleOrDefaultAsync(s => s.Name == inventoryModel.VendorName);

            if(vendor == null)
            {
                return BadRequest();
            }

            InventoryItem inventoryItem = new InventoryItem
            {
                Guid = new Guid(inventoryModel.Guid),
                SerialNo = inventoryModel.SerialNo,
                Quantity = inventoryModel.Quantity,
                QuantityAvailable = inventoryModel.QuantityAvailable,
                Title = inventoryModel.Title,
                Type = InventoryItem.GetItemType(inventoryModel.Type),
                Vendor = vendor
            };

            _context.Items.Add(inventoryItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInventoryItem", new { id = inventoryItem.Guid }, inventoryItem);
        }

        // DELETE: api/InventoryItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteInventoryItem([FromBody] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Guid guid = new Guid(id);
            var inventoryItem = await _context.Items.SingleOrDefaultAsync(m => m.Guid == guid);

            if (inventoryItem == null)
            {
                return NotFound();
            }

            _context.Items.Remove(inventoryItem);
            await _context.SaveChangesAsync();

            return Ok(inventoryItem);
        }

        private bool InventoryItemExists(Guid id)
        {
            return _context.Items.Any(e => e.Guid == id);
        }
    }
}