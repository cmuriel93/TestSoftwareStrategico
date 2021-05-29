using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestSoftwareStrategico.Models;

namespace TestSoftwareStrategico.Data
{
    public class TestSoftwareStrategicoContext : DbContext
    {
        public TestSoftwareStrategicoContext (DbContextOptions<TestSoftwareStrategicoContext> options)
            : base(options)
        {
        }

        public DbSet<Productos> Productos { get; set; }

        public DbSet<TestSoftwareStrategico.Models.TiposProductos> TiposProductos { get; set; }
    }
}
