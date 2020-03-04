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
    public class CharactersWeaponsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CharactersWeaponsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CharactersWeapons
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CharactersWeapons.Include(c => c.DndCharacter).Include(c => c.Weapon);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CharactersWeapons/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var charactersWeapons = await _context.CharactersWeapons
                .Include(c => c.DndCharacter)
                .Include(c => c.Weapon)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (charactersWeapons == null)
            {
                return NotFound();
            }

            return View(charactersWeapons);
        }

        // GET: CharactersWeapons/Create
        public IActionResult Create()
        {
            ViewData["DndCharacterId"] = new SelectList(_context.DndCharacters, "Id", "Id");
            ViewData["WeaponId"] = new SelectList(_context.Weapons, "Id", "Id");
            return View();
        }

        // POST: CharactersWeapons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WeaponId,DndCharacterId,Id,Comment")] CharactersWeapons charactersWeapons)
        {
            if (ModelState.IsValid)
            {
                _context.Add(charactersWeapons);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DndCharacterId"] = new SelectList(_context.DndCharacters, "Id", "Id", charactersWeapons.DndCharacterId);
            ViewData["WeaponId"] = new SelectList(_context.Weapons, "Id", "Id", charactersWeapons.WeaponId);
            return View(charactersWeapons);
        }

        // GET: CharactersWeapons/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var charactersWeapons = await _context.CharactersWeapons.FindAsync(id);
            if (charactersWeapons == null)
            {
                return NotFound();
            }
            ViewData["DndCharacterId"] = new SelectList(_context.DndCharacters, "Id", "Id", charactersWeapons.DndCharacterId);
            ViewData["WeaponId"] = new SelectList(_context.Weapons, "Id", "Id", charactersWeapons.WeaponId);
            return View(charactersWeapons);
        }

        // POST: CharactersWeapons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("WeaponId,DndCharacterId,Id,Comment")] CharactersWeapons charactersWeapons)
        {
            if (id != charactersWeapons.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(charactersWeapons);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharactersWeaponsExists(charactersWeapons.Id))
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
            ViewData["DndCharacterId"] = new SelectList(_context.DndCharacters, "Id", "Id", charactersWeapons.DndCharacterId);
            ViewData["WeaponId"] = new SelectList(_context.Weapons, "Id", "Id", charactersWeapons.WeaponId);
            return View(charactersWeapons);
        }

        // GET: CharactersWeapons/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var charactersWeapons = await _context.CharactersWeapons
                .Include(c => c.DndCharacter)
                .Include(c => c.Weapon)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (charactersWeapons == null)
            {
                return NotFound();
            }

            return View(charactersWeapons);
        }

        // POST: CharactersWeapons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var charactersWeapons = await _context.CharactersWeapons.FindAsync(id);
            _context.CharactersWeapons.Remove(charactersWeapons);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CharactersWeaponsExists(string id)
        {
            return _context.CharactersWeapons.Any(e => e.Id == id);
        }
    }
}
