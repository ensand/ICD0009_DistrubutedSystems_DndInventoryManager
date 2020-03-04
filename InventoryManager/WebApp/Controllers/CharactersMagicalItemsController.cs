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
    public class CharactersMagicalItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CharactersMagicalItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CharactersMagicalItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CharactersMagicalItems.Include(c => c.DndCharacter).Include(c => c.MagicalItem);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CharactersMagicalItems/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var charactersMagicalItems = await _context.CharactersMagicalItems
                .Include(c => c.DndCharacter)
                .Include(c => c.MagicalItem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (charactersMagicalItems == null)
            {
                return NotFound();
            }

            return View(charactersMagicalItems);
        }

        // GET: CharactersMagicalItems/Create
        public IActionResult Create()
        {
            ViewData["DndCharacterId"] = new SelectList(_context.DndCharacters, "Id", "Id");
            ViewData["MagicalItemId"] = new SelectList(_context.MagicalItems, "Id", "Id");
            return View();
        }

        // POST: CharactersMagicalItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MagicalItemId,DndCharacterId,Id,Comment")] CharactersMagicalItems charactersMagicalItems)
        {
            if (ModelState.IsValid)
            {
                _context.Add(charactersMagicalItems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DndCharacterId"] = new SelectList(_context.DndCharacters, "Id", "Id", charactersMagicalItems.DndCharacterId);
            ViewData["MagicalItemId"] = new SelectList(_context.MagicalItems, "Id", "Id", charactersMagicalItems.MagicalItemId);
            return View(charactersMagicalItems);
        }

        // GET: CharactersMagicalItems/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var charactersMagicalItems = await _context.CharactersMagicalItems.FindAsync(id);
            if (charactersMagicalItems == null)
            {
                return NotFound();
            }
            ViewData["DndCharacterId"] = new SelectList(_context.DndCharacters, "Id", "Id", charactersMagicalItems.DndCharacterId);
            ViewData["MagicalItemId"] = new SelectList(_context.MagicalItems, "Id", "Id", charactersMagicalItems.MagicalItemId);
            return View(charactersMagicalItems);
        }

        // POST: CharactersMagicalItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MagicalItemId,DndCharacterId,Id,Comment")] CharactersMagicalItems charactersMagicalItems)
        {
            if (id != charactersMagicalItems.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(charactersMagicalItems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharactersMagicalItemsExists(charactersMagicalItems.Id))
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
            ViewData["DndCharacterId"] = new SelectList(_context.DndCharacters, "Id", "Id", charactersMagicalItems.DndCharacterId);
            ViewData["MagicalItemId"] = new SelectList(_context.MagicalItems, "Id", "Id", charactersMagicalItems.MagicalItemId);
            return View(charactersMagicalItems);
        }

        // GET: CharactersMagicalItems/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var charactersMagicalItems = await _context.CharactersMagicalItems
                .Include(c => c.DndCharacter)
                .Include(c => c.MagicalItem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (charactersMagicalItems == null)
            {
                return NotFound();
            }

            return View(charactersMagicalItems);
        }

        // POST: CharactersMagicalItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var charactersMagicalItems = await _context.CharactersMagicalItems.FindAsync(id);
            _context.CharactersMagicalItems.Remove(charactersMagicalItems);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CharactersMagicalItemsExists(string id)
        {
            return _context.CharactersMagicalItems.Any(e => e.Id == id);
        }
    }
}
