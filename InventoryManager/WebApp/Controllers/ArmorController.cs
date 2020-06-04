using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using Extensions;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize(Roles = "user")]
    public class ArmorController : Controller
    {
        private readonly AppDbContext _context;

        public ArmorController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Armor
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Armors
                .Include(a => a.AppUser)
                .Include(a => a.DndCharacter)
                .Where(a => a.AppUserId == User.UserGuidId());
            
            return View(await appDbContext.ToListAsync());
        }

        // GET: Armor/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var armor = await _context.Armors
                .Include(a => a.AppUser)
                .Include(a => a.DndCharacter)
                .FirstOrDefaultAsync(a => a.Id == id && a.AppUserId == User.UserGuidId());
            
            if (armor == null)
            {
                return NotFound();
            }

            return View(armor);
        }

        // GET: Armor/Create
        public IActionResult Create()
        {
            ViewData["DndCharacterId"] = new SelectList(_context.DndCharacters, "Id", "Name");
            return View();
        }

        // POST: Armor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Armor armor)
        {
            armor.AppUserId = User.UserGuidId();
            
            if (ModelState.IsValid)
            {
                _context.Add(armor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["DndCharacterId"] = new SelectList(_context.DndCharacters, "Id", "Name", armor.DndCharacterId);
            return View(armor);
        }

        // GET: Armor/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var armor = await _context.Armors.FirstOrDefaultAsync(a => a.Id == id && a.AppUserId == User.UserGuidId());
            
            if (armor == null)
            {
                return NotFound();
            }
            
            ViewData["DndCharacterId"] = new SelectList(_context.DndCharacters, "Id", "Name", armor.DndCharacterId);
            return View(armor);
        }

        // POST: Armor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Armor armor)
        {
            armor.AppUserId = User.UserGuidId();
            
            if (id != armor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(armor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArmorExists(armor.Id))
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
            
            ViewData["DndCharacterId"] = new SelectList(_context.DndCharacters, "Id", "Name", armor.DndCharacterId);
            return View(armor);
        }

        // GET: Armor/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var armor = await _context.Armors
                .Include(a => a.AppUser)
                .Include(a => a.DndCharacter)
                .FirstOrDefaultAsync(a => a.Id == id && a.AppUserId == User.UserGuidId());
            
            if (armor == null)
            {
                return NotFound();
            }

            return View(armor);
        }

        // POST: Armor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var armor = await _context.Armors.FirstOrDefaultAsync(a => a.Id == id && a.AppUserId == User.UserGuidId());
            
            _context.Armors.Remove(armor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArmorExists(Guid id)
        {
            return _context.Armors.Any(e => e.Id == id);
        }
    }
}
