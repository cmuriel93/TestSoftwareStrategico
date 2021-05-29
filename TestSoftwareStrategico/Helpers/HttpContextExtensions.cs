using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestSoftwareStrategico.Helpers
{
    public static class HttpContextExtensions
    {
        public static async Task PaginacionEnRespuesta<T>(this HttpContext context, IQueryable<T> queryable, int cantidadRegistros)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            double conteo = 1;
            double totalPaginas = Math.Ceiling(conteo / cantidadRegistros);
            context.Response.Headers.Add("totalPaginas", totalPaginas.ToString());
        }
    }
}
