using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Controllers
{
    public class OtherEquipmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OtherEquipmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OtherEquipments
        public async Task<IActionResult> Index()
        {
            return View(await _context.OtherEquipments.ToListAsync());
        }

        // GET: OtherEquipments/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var otherEquipment = await _context.OtherEquipments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (otherEquipment == null)
            {
                return NotFound();
            }

            return View(otherEquipment);
        }

        // GET: OtherEquipments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OtherEquipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ValueInGp,Quantity,Id,Comment")] OtherEquipment otherEquipment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(otherEquipment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(otherEquipment);
        }

        // GET: OtherEquipments/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var otherEquipment = await _context.OtherEquipments.FindAsync(id);
            if (otherEquipment == null)
            {
                return NotFound();
            }
            return View(otherEquipment);
        }

        // POST: OtherEquipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,ValueInGp,Quantity,Id,Comment")] OtherEquipment otherEquipment)
        {
            if (id != otherEquipment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(otherEquipment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OtherEquipmentExists(otherEquipment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(otherEquipment);
        }

        // GET: OtherEquipments/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var otherEquipment = await _context.OtherEquipments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (otherEquipment == null)
            {
                return NotFound();
            }

            return View(otherEquipment);
        }

        // POST: OtherEquipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var otherEquipment = await _context.OtherEquipments.FindAsync(id);
            _context.OtherEquipments.Remove(otherEquipment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OtherEquipmentExists(string id)
        {
            return _context.OtherEquipments.Any(e => e.Id == id);
        }
    }
}
