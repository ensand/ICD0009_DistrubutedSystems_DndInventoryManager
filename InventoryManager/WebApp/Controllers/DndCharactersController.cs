using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize(Roles = "user")]
    public class DndCharactersController : Controller
    {
        private readonly AppDbContext _context;

        public DndCharactersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: DndCharacters
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.DndCharacters
                .Include(d => d.AppUser)
                .Where(d => d.AppUserId == User.UserGuidId());
            
            return View(await appDbContext.ToListAsync());
        }

        // GET: DndCharacters/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dndCharacter = await _context.DndCharacters
                .Include(d => d.AppUser)
                .FirstOrDefaultAsync(d => d.Id == id && d.AppUserId == User.UserGuidId());
            
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DndCharacter dndCharacter)
        {
            dndCharacter.AppUserId = User.UserGuidId();
            
            if (ModelState.IsValid)
            {
                _context.Add(dndCharacter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(dndCharacter);
        }

        // GET: DndCharacters/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dndCharacter = await _context.DndCharacters.FirstOrDefaultAsync(d => d.Id == id && d.AppUserId == User.UserGuidId());
            
            if (dndCharacter == null)
            {
                return NotFound();
            }
            
            return View(dndCharacter);
        }

        // POST: DndCharacters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, DndCharacter dndCharacter)
        {
            dndCharacter.AppUserId = User.UserGuidId();
            
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
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dndCharacter = await _context.DndCharacters
                .FirstOrDefaultAsync(d => d.Id == id && d.AppUserId == User.UserGuidId());
            
            if (dndCharacter == null)
            {
                return NotFound();
            }

            return View(dndCharacter);
        }

        // POST: DndCharacters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var dndCharacter = await _context.DndCharacters.FirstOrDefaultAsync(d => d.Id == id && d.AppUserId == User.UserGuidId());
            
            _context.DndCharacters.Remove(dndCharacter);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DndCharacterExists(Guid id)
        {
            return _context.DndCharacters.Any(e => e.Id == id);
        }
    }
}
