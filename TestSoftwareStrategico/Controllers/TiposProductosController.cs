using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestSoftwareStrategico.Data;
using TestSoftwareStrategico.Models;

namespace TestSoftwareStrategico.Controllers
{
    public class TiposProductosController : Controller
    {
        private readonly TestSoftwareStrategicoContext _context;

        public TiposProductosController(TestSoftwareStrategicoContext context)
        {
            _context = context;
        }

        // GET: TiposProductos
        public async Task<IActionResult> Index()
        {
            return View(await _context.TiposProductos.ToListAsync());
        }

        // GET: TiposProductos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiposProductos = await _context.TiposProductos
                .FirstOrDefaultAsync(m => m.TipoProdcutoId == id);
            if (tiposProductos == null)
            {
                return NotFound();
            }

            return View(tiposProductos);
        }

        // GET: TiposProductos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TiposProductos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoProdcutoId,TipoProductoNombre,Activo,InsertTimeaStamp")] TiposProductos tiposProductos)
        {
            if (ModelState.IsValid)
            {
                tiposProductos.Activo = true;
                tiposProductos.InsertTimeStamp = DateTime.Now;

                _context.Add(tiposProductos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tiposProductos);
        }

        // GET: TiposProductos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiposProductos = await _context.TiposProductos.FindAsync(id);
            if (tiposProductos == null)
            {
                return NotFound();
            }
            return View(tiposProductos);
        }

        // POST: TiposProductos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoProdcutoId,TipoProductoNombre,Activo,InsertTimeaStamp")] TiposProductos tiposProductos)
        {
            if (id != tiposProductos.TipoProdcutoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tiposProductos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TiposProductosExists(tiposProductos.TipoProdcutoId))
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
            return View(tiposProductos);
        }

        // GET: TiposProductos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiposProductos = await _context.TiposProductos
                .FirstOrDefaultAsync(m => m.TipoProdcutoId == id);
            if (tiposProductos == null)
            {
                return NotFound();
            }

            return View(tiposProductos);
        }

        // POST: TiposProductos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tiposProductos = await _context.TiposProductos.FindAsync(id);
            _context.TiposProductos.Remove(tiposProductos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TiposProductosExists(int id)
        {
            return _context.TiposProductos.Any(e => e.TipoProdcutoId == id);
        }
    }
}
