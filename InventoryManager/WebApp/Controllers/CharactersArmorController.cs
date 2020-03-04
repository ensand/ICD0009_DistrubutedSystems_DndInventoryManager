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
    public class CharactersArmorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CharactersArmorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CharactersArmor
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CharactersArmors.Include(c => c.Armor).Include(c => c.DndCharacter);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CharactersArmor/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var charactersArmor = await _context.CharactersArmors
                .Include(c => c.Armor)
                .Include(c => c.DndCharacter)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (charactersArmor == null)
            {
                return NotFound();
            }

            return View(charactersArmor);
        }

        // GET: CharactersArmor/Create
        public IActionResult Create()
        {
            ViewData["ArmorId"] = new SelectList(_context.Armors, "Id", "Id");
            ViewData["DndCharacterId"] = new SelectList(_context.DndCharacters, "Id", "Id");
            return View();
        }

        // POST: CharactersArmor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArmorId,DndCharacterId,Id,Comment")] CharactersArmor charactersArmor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(charactersArmor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArmorId"] = new SelectList(_context.Armors, "Id", "Id", charactersArmor.ArmorId);
            ViewData["DndCharacterId"] = new SelectList(_context.DndCharacters, "Id", "Id", charactersArmor.DndCharacterId);
            return View(charactersArmor);
        }

        // GET: CharactersArmor/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var charactersArmor = await _context.CharactersArmors.FindAsync(id);
            if (charactersArmor == null)
            {
                return NotFound();
            }
            ViewData["ArmorId"] = new SelectList(_context.Armors, "Id", "Id", charactersArmor.ArmorId);
            ViewData["DndCharacterId"] = new SelectList(_context.DndCharacters, "Id", "Id", charactersArmor.DndCharacterId);
            return View(charactersArmor);
        }

        // POST: CharactersArmor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ArmorId,DndCharacterId,Id,Comment")] CharactersArmor charactersArmor)
        {
            if (id != charactersArmor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(charactersArmor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharactersArmorExists(charactersArmor.Id))
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
            ViewData["ArmorId"] = new SelectList(_context.Armors, "Id", "Id", charactersArmor.ArmorId);
            ViewData["DndCharacterId"] = new SelectList(_context.DndCharacters, "Id", "Id", charactersArmor.DndCharacterId);
            return View(charactersArmor);
        }

        // GET: CharactersArmor/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var charactersArmor = await _context.CharactersArmors
                .Include(c => c.Armor)
                .Include(c => c.DndCharacter)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (charactersArmor == null)
            {
                return NotFound();
            }

            return View(charactersArmor);
        }

        // POST: CharactersArmor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var charactersArmor = await _context.CharactersArmors.FindAsync(id);
            _context.CharactersArmors.Remove(charactersArmor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CharactersArmorExists(string id)
        {
            return _context.CharactersArmors.Any(e => e.Id == id);
        }
    }
}
