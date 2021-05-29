using System;
using System.ComponentModel.DataAnnotations;

namespace TestSoftwareStrategico.Models
{
    public class TiposProductos
    {
        [Key]
        public int TipoProdcutoId { get; set; }
        public string TipoProductoNombre { get; set; }
        public bool Activo { get; set; }
        public DateTime InsertTimeStamp { get; set; }
    }
}
