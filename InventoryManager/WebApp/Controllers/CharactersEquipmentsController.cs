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
    public class CharactersEquipmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CharactersEquipmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CharactersEquipments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CharactersEquipments.Include(c => c.DndCharacter).Include(c => c.Equipment);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CharactersEquipments/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var charactersEquipment = await _context.CharactersEquipments
                .Include(c => c.DndCharacter)
                .Include(c => c.Equipment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (charactersEquipment == null)
            {
                return NotFound();
            }

            return View(charactersEquipment);
        }

        // GET: CharactersEquipments/Create
        public IActionResult Create()
        {
            ViewData["DndCharacterId"] = new SelectList(_context.DndCharacters, "Id", "Id");
            ViewData["EquipmentId"] = new SelectList(_context.OtherEquipments, "Id", "Id");
            return View();
        }

        // POST: CharactersEquipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EquipmentId,DndCharacterId,Id,Comment")] CharactersEquipment charactersEquipment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(charactersEquipment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DndCharacterId"] = new SelectList(_context.DndCharacters, "Id", "Id", charactersEquipment.DndCharacterId);
            ViewData["EquipmentId"] = new SelectList(_context.OtherEquipments, "Id", "Id", charactersEquipment.EquipmentId);
            return View(charactersEquipment);
        }

        // GET: CharactersEquipments/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var charactersEquipment = await _context.CharactersEquipments.FindAsync(id);
            if (charactersEquipment == null)
            {
                return NotFound();
            }
            ViewData["DndCharacterId"] = new SelectList(_context.DndCharacters, "Id", "Id", charactersEquipment.DndCharacterId);
            ViewData["EquipmentId"] = new SelectList(_context.OtherEquipments, "Id", "Id", charactersEquipment.EquipmentId);
            return View(charactersEquipment);
        }

        // POST: CharactersEquipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EquipmentId,DndCharacterId,Id,Comment")] CharactersEquipment charactersEquipment)
        {
            if (id != charactersEquipment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(charactersEquipment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharactersEquipmentExists(charactersEquipment.Id))
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
            ViewData["DndCharacterId"] = new SelectList(_context.DndCharacters, "Id", "Id", charactersEquipment.DndCharacterId);
            ViewData["EquipmentId"] = new SelectList(_context.OtherEquipments, "Id", "Id", charactersEquipment.EquipmentId);
            return View(charactersEquipment);
        }

        // GET: CharactersEquipments/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var charactersEquipment = await _context.CharactersEquipments
                .Include(c => c.DndCharacter)
                .Include(c => c.Equipment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (charactersEquipment == null)
            {
                return NotFound();
            }

            return View(charactersEquipment);
        }

        // POST: CharactersEquipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var charactersEquipment = await _context.CharactersEquipments.FindAsync(id);
            _context.CharactersEquipments.Remove(charactersEquipment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CharactersEquipmentExists(string id)
        {
            return _context.CharactersEquipments.Any(e => e.Id == id);
        }
    }
}
