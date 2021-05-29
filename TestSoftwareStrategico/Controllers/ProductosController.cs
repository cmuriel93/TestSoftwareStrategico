using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TestSoftwareStrategico.Data;
using TestSoftwareStrategico.Models;

namespace TestSoftwareStrategico.Controllers
{
    public class ProductosController : Controller
    {
        private readonly TestSoftwareStrategicoContext _context;
        private readonly IMemoryCache _memoryCache;

        public ProductosController(TestSoftwareStrategicoContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;

            MemoryCacheEntryOptions cacheEspiracion = new MemoryCacheEntryOptions();
            cacheEspiracion.AbsoluteExpiration = DateTime.Now.AddMinutes(1);
            cacheEspiracion.Priority = CacheItemPriority.High;
        }

        [HttpGet]
        // GET: Productos
        public async Task<IEnumerable<Productos>> GetProducts()
        {
            IEnumerable<Productos> productos = await _context.Productos.ToListAsync();
                       
            return _memoryCache.GetOrCreate("data", cacheEntry => {
                return productos;
            });                 
        }

        // GET: Productos
        public IActionResult Index()
        { 

            var productos = GetProducts();
            return View(productos.Result);
        }

        // GET: Productos/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            IEnumerable<Productos> productos = GetProducts().Result;

            var producto = productos.Where(m => m.ProductoId == id).FirstOrDefault();
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductoId,TipoProdcutoId,ProductoNombre,ProductoDescripcion,Precio,Activo,InsertTimeStamp")] Productos productos)
        {
            if (ModelState.IsValid)
            {
                productos.Activo = true;
                productos.InsertTimeStamp = DateTime.Now;

                _context.Add(productos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productos);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos.FindAsync(id);
            if (productos == null)
            {
                return NotFound();
            }
            return View(productos);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductoId,TipoProdcutoId,ProductoNombre,ProductoDescripcion, Precio, Activo, InsertTimeStamp")] Productos productos)
        {
            if (id != productos.ProductoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductosExists(productos.ProductoId))
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
            return View(productos);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos
                .FirstOrDefaultAsync(m => m.ProductoId == id);
            if (productos == null)
            {
                return NotFound();
            }

            return View(productos);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productos = await _context.Productos.FindAsync(id);
            _context.Productos.Remove(productos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductosExists(int id)
        {
            return _context.Productos.Any(e => e.ProductoId == id);
        }
    }
}
