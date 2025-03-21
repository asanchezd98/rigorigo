using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RigoRigo.Data.Models;
using RigoRigo.Entities.Entities;

namespace RigoRigo.Business.Interfaces
{
    public interface IPedidoService
    {
        Task CrearPedidoConClienteAsync(PedidosModel pedidoConCliente);
        Task<List<Pedido>> ObtenerPedidosPorClienteAsync(int clienteId);
    }
}
