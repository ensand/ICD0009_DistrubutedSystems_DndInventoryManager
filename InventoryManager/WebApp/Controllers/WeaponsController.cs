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
    public class WeaponsController : Controller
    {
        private readonly AppDbContext _context;

        public WeaponsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Weapons
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Weapons
                .Include(w => w.AppUser)
                .Include(w => w.DndCharacter)
                .Where(w => w.AppUserId == User.UserGuidId());
            
            return View(await appDbContext.ToListAsync());
        }

        // GET: Weapons/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weapon = await _context.Weapons
                .Include(w => w.AppUser)
                .Include(w => w.DndCharacter)
                .FirstOrDefaultAsync(w => w.Id == id && w.AppUserId == User.UserGuidId());
            
            if (weapon == null)
            {
                return NotFound();
            }

            return View(weapon);
        }

        // GET: Weapons/Create
        public IActionResult Create()
        {
            ViewData["DndCharacterId"] = new SelectList(_context.DndCharacters, "Id", "Name");
            return View();
        }

        // POST: Weapons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Weapon weapon)
        {
            weapon.AppUserId = User.UserGuidId();
            
            if (ModelState.IsValid)
            {
                _context.Add(weapon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["DndCharacterId"] = new SelectList(_context.DndCharacters, "Id", "Name", weapon.DndCharacterId);
            return View(weapon);
        }

        // GET: Weapons/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weapon = await _context.Weapons.FirstOrDefaultAsync(w => w.Id == id && w.AppUserId == User.UserGuidId());
            
            if (weapon == null)
            {
                return NotFound();
            }

            ViewData["DndCharacterId"] = new SelectList(_context.DndCharacters, "Id", "Name", weapon.DndCharacterId);
            return View(weapon);
        }

        // POST: Weapons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Weapon weapon)
        {
            weapon.AppUserId = User.UserGuidId();
            
            if (id != weapon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(weapon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeaponExists(weapon.Id))
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

            ViewData["DndCharacterId"] = new SelectList(_context.DndCharacters, "Id", "Name", weapon.DndCharacterId);
            return View(weapon);
        }

        // GET: Weapons/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weapon = await _context.Weapons
                .Include(w => w.AppUser)
                .Include(w => w.DndCharacter)
                .FirstOrDefaultAsync(w => w.Id == id && w.AppUserId == User.UserGuidId());
            
            if (weapon == null)
            {
                return NotFound();
            }

            return View(weapon);
        }

        // POST: Weapons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var weapon = await _context.Weapons.FirstOrDefaultAsync(w => w.Id == id && w.AppUserId == User.UserGuidId());
          
            _context.Weapons.Remove(weapon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeaponExists(Guid id)
        {
            return _context.Weapons.Any(e => e.Id == id);
        }
    }
}
