using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestSoftwareStrategico.Models;

namespace TestSoftwareStrategico.Helpers
{
    public static class QueryableExtensions
    {

        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable, Paginacion paginacion)
        {
            return queryable
                .Skip((paginacion.pagina - 1) * paginacion.cantidadMostrar)
                .Take(paginacion.cantidadMostrar);
        }
    }
}
