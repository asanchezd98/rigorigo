using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RigoRigo.Entities.Entities
{
    public class Pedido
    {
        public int PedidoID { get; set; }
        public int ClienteID { get; set; }
        public DateTime Fecha { get; set; }
        public decimal ValorTotal { get; set; }
        public Cliente Cliente { get; set; } = new Cliente();
        public List<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();
    }
}
