using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.Controllers
{
    public class MagicalItemsController : Controller
    {
        private readonly AppDbContext _context;

        public MagicalItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: MagicalItems
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.MagicalItems.Include(m => m.DndCharacter);
            return View(await appDbContext.ToListAsync());
        }

        // GET: MagicalItems/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magicalItem = await _context.MagicalItems
                .Include(m => m.DndCharacter)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (magicalItem == null)
            {
                return NotFound();
            }

            return View(magicalItem);
        }

        // GET: MagicalItems/Create
        public IActionResult Create()
        {
            ViewData["DndCharacterId"] = new SelectList(_context.DndCharacters, "Id", "Name");
            return View();
        }

        // POST: MagicalItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DndCharacterId,BaseItem,Name,Spell,MaxCharges,CurrentCharges,Quantity,Id,Comment")] MagicalItem magicalItem)
        {
            if (ModelState.IsValid)
            {
                magicalItem.Id = Guid.NewGuid();
                _context.Add(magicalItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DndCharacterId"] = new SelectList(_context.DndCharacters, "Id", "Name", magicalItem.DndCharacterId);
            return View(magicalItem);
        }

        // GET: MagicalItems/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magicalItem = await _context.MagicalItems.FindAsync(id);
            if (magicalItem == null)
            {
                return NotFound();
            }
            ViewData["DndCharacterId"] = new SelectList(_context.DndCharacters, "Id", "Name", magicalItem.DndCharacterId);
            return View(magicalItem);
        }

        // POST: MagicalItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DndCharacterId,BaseItem,Name,Spell,MaxCharges,CurrentCharges,Quantity,Id,Comment")] MagicalItem magicalItem)
        {
            if (id != magicalItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(magicalItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MagicalItemExists(magicalItem.Id))
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
            ViewData["DndCharacterId"] = new SelectList(_context.DndCharacters, "Id", "Name", magicalItem.DndCharacterId);
            return View(magicalItem);
        }

        // GET: MagicalItems/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magicalItem = await _context.MagicalItems
                .Include(m => m.DndCharacter)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (magicalItem == null)
            {
                return NotFound();
            }

            return View(magicalItem);
        }

        // POST: MagicalItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var magicalItem = await _context.MagicalItems.FindAsync(id);
            _context.MagicalItems.Remove(magicalItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MagicalItemExists(Guid id)
        {
            return _context.MagicalItems.Any(e => e.Id == id);
        }
    }
}
