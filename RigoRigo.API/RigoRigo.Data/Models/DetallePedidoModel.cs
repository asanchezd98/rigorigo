using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RigoRigo.Data.Models
{
    public class DetallePedidoModel
    {
        public int productoID { get; set; }
        public int cantidad { get; set; }
        public decimal precioUnitario { get; set; }
    }
}
