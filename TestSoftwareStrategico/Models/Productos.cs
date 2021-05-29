using System;
using System.ComponentModel.DataAnnotations;

namespace TestSoftwareStrategico.Models
{
    public class Productos
    {
        [Key]
        public int ProductoId { get; set; }          
        public int TipoProdcutoId { get; set; }
        public string ProductoNombre { get; set; }
        public string ProductoDescripcion { get; set; }
        public decimal Precio { get; set; }
        public bool Activo { get; set; }
        public DateTime InsertTimeStamp { get; set; }

        //public virtual TiposProductos tipoProducto { get; set; }
    }
}
