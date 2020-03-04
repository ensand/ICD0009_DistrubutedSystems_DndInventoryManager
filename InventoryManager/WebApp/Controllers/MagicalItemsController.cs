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
    public class MagicalItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MagicalItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MagicalItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.MagicalItems.ToListAsync());
        }

        // GET: MagicalItems/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magicalItem = await _context.MagicalItems
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
            return View();
        }

        // POST: MagicalItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Spell,MaxCharges,CurrentCharges,Quantity,Id,Comment")] MagicalItem magicalItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(magicalItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(magicalItem);
        }

        // GET: MagicalItems/Edit/5
        public async Task<IActionResult> Edit(string id)
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
            return View(magicalItem);
        }

        // POST: MagicalItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Spell,MaxCharges,CurrentCharges,Quantity,Id,Comment")] MagicalItem magicalItem)
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
            return View(magicalItem);
        }

        // GET: MagicalItems/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magicalItem = await _context.MagicalItems
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
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var magicalItem = await _context.MagicalItems.FindAsync(id);
            _context.MagicalItems.Remove(magicalItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MagicalItemExists(string id)
        {
            return _context.MagicalItems.Any(e => e.Id == id);
        }
    }
}
