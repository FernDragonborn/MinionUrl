using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MinionUrl.Data;
using MinionUrl.Models;

namespace MinionUrl.Controllers
{
    public class BackendController : Controller
    {
        private readonly MinionUrlContext _context;

        public BackendController(MinionUrlContext context)
        {
            _context = context;
        }

        // GET: UrlDatas
        public async Task<IActionResult> Index()
        {
            return _context.UrlData != null ?
                        View(await _context.UrlData.ToListAsync()) :
                        Problem("Entity set 'MinionUrlContext.UrlData'  is null.");
        }

        // GET: UrlDatas/Details/5
        public virtual async Task<IActionResult> GetUrl(Guid? id)
        {
            if (id == null || _context.UrlData == null)
            {
                return NotFound();
            }

            var urlData = await _context.UrlData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (urlData == null)
            {
                return NotFound();
            }

            return View(urlData);
        }

        // GET: UrlDatas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UrlDatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullUrl,ShortUrl,CreatorId,CreationDateTime")] UrlData urlData)
        {
            if (ModelState.IsValid)
            {
                urlData.Id = Guid.NewGuid();
                _context.Add(urlData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(urlData);
        }

        // GET: UrlDatas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.UrlData == null)
            {
                return NotFound();
            }

            var urlData = await _context.UrlData.FindAsync(id);
            if (urlData == null)
            {
                return NotFound();
            }
            return View(urlData);
        }

        // POST: UrlDatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,FullUrl,ShortUrl,CreatorId,CreationDateTime")] UrlData urlData)
        {
            if (id != urlData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(urlData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UrlDataExists(urlData.Id))
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
            return View(urlData);
        }

        // GET: UrlDatas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.UrlData == null)
            {
                return NotFound();
            }

            var urlData = await _context.UrlData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (urlData == null)
            {
                return NotFound();
            }

            return View(urlData);
        }

        // POST: UrlDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.UrlData == null)
            {
                return Problem("Entity set 'MinionUrlContext.UrlData'  is null.");
            }
            var urlData = await _context.UrlData.FindAsync(id);
            if (urlData != null)
            {
                _context.UrlData.Remove(urlData);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UrlDataExists(Guid id)
        {
            return (_context.UrlData?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
