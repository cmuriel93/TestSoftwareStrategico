using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestSoftwareStrategico.Data;
using TestSoftwareStrategico.Models;

namespace TestSoftwareStrategico.Controllers
{
    public class HomeController : Controller
    {
        private readonly TestSoftwareStrategicoContext _context;
        private readonly IMemoryCache _memoryCache;

        public HomeController(TestSoftwareStrategicoContext context, IMemoryCache memoryCache)
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

        //[HttpGet]
        //[AllowAnonymous]
        public ViewResult Index(string sortOrder, string buscador)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "precio" ? "price_desc" : "precio";

            var productos = GetProducts().Result;

            if (!String.IsNullOrEmpty(buscador))
            {
                decimal precio = 0;
                try
                {
                    precio = Convert.ToInt32(buscador);
                }
                catch
                {
                    precio = 0;
                }                   

                productos = productos.Where(x => x.ProductoNombre.Contains(buscador)
                || x.Precio == precio);
            }


            switch (sortOrder)
            {
                case "name_desc":
                    productos = productos.OrderByDescending(s => s.ProductoNombre);
                    break;

                case "precio":
                    productos = productos.OrderBy(s => s.Precio);
                    break;

                case "price_desc":
                    productos = productos.OrderByDescending(s => s.Precio);
                    break;
            }
            return View(productos);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
