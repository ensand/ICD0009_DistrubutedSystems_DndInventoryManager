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
    public class DndCharactersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DndCharactersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DndCharacters
        public async Task<IActionResult> Index()
        {
            return View(await _context.DndCharacters.ToListAsync());
        }

        // GET: DndCharacters/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dndCharacter = await _context.DndCharacters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dndCharacter == null)
            {
                return NotFound();
            }

            return View(dndCharacter);
        }

        // GET: DndCharacters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DndCharacters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Level,GoldPieces,PlatinumPieces,SilverPieces,CopperPieces,Id,Comment")] DndCharacter dndCharacter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dndCharacter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dndCharacter);
        }

        // GET: DndCharacters/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dndCharacter = await _context.DndCharacters.FindAsync(id);
            if (dndCharacter == null)
            {
                return NotFound();
            }
            return View(dndCharacter);
        }

        // POST: DndCharacters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Level,GoldPieces,PlatinumPieces,SilverPieces,CopperPieces,Id,Comment")] DndCharacter dndCharacter)
        {
            if (id != dndCharacter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dndCharacter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DndCharacterExists(dndCharacter.Id))
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
            return View(dndCharacter);
        }

        // GET: DndCharacters/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dndCharacter = await _context.DndCharacters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dndCharacter == null)
            {
                return NotFound();
            }

            return View(dndCharacter);
        }

        // POST: DndCharacters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var dndCharacter = await _context.DndCharacters.FindAsync(id);
            _context.DndCharacters.Remove(dndCharacter);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DndCharacterExists(string id)
        {
            return _context.DndCharacters.Any(e => e.Id == id);
        }
    }
}
