using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RigoRigo.Data.Models
{
    public class PedidosModel
    {
        public string cedula { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public List<DetallePedidoModel> detalles { get; set; } = new List<DetallePedidoModel>();
    }
}
