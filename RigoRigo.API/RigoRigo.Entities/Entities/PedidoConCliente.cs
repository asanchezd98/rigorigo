﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RigoRigo.Entities.Entities
{
    public class PedidoConCliente
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public List<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();
    }
}
