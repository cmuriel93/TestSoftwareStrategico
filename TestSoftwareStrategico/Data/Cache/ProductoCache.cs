using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestSoftwareStrategico.Models;

namespace TestSoftwareStrategico.Data.Cache
{ 
    public  class ProductoCache
    {
        private readonly TestSoftwareStrategicoContext _context;
        private readonly IMemoryCache _memoryCache;

        public ProductoCache(TestSoftwareStrategicoContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;

            MemoryCacheEntryOptions cacheEspiracion = new MemoryCacheEntryOptions();
            cacheEspiracion.AbsoluteExpiration = DateTime.Now.AddMinutes(1);
            cacheEspiracion.Priority = CacheItemPriority.Normal;
        }

        public async Task<IEnumerable<Productos>> GetProducts()
        {
            IEnumerable<Productos> productos = await _context.Productos.ToListAsync();

            return _memoryCache.GetOrCreate("data", cacheEntry => {
                return productos;
            });
        }

        public IEnumerable<Productos> GetProrudto()
        {
            var productos = GetProducts();

            return productos.Result;
        }
    }
}
