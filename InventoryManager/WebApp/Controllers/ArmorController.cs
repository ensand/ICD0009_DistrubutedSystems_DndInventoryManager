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
    public class ArmorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArmorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Armor
        public async Task<IActionResult> Index()
        {
            return View(await _context.Armors.ToListAsync());
        }

        // GET: Armor/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var armor = await _context.Armors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (armor == null)
            {
                return NotFound();
            }

            return View(armor);
        }

        // GET: Armor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Armor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Type,BaseAc,Weight,ValueInGp,Quantity,StealthDisadvantage,Id,Comment")] Armor armor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(armor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(armor);
        }

        // GET: Armor/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var armor = await _context.Armors.FindAsync(id);
            if (armor == null)
            {
                return NotFound();
            }
            return View(armor);
        }

        // POST: Armor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Type,BaseAc,Weight,ValueInGp,Quantity,StealthDisadvantage,Id,Comment")] Armor armor)
        {
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
            return View(armor);
        }

        // GET: Armor/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var armor = await _context.Armors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (armor == null)
            {
                return NotFound();
            }

            return View(armor);
        }

        // POST: Armor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var armor = await _context.Armors.FindAsync(id);
            _context.Armors.Remove(armor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArmorExists(string id)
        {
            return _context.Armors.Any(e => e.Id == id);
        }
    }
}
