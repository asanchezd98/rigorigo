using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RigoRigo.Business.Interfaces;
using RigoRigo.Data.Interfaces;
using RigoRigo.Data.Models;
using RigoRigo.Entities.Entities;

namespace RigoRigo.Business.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoService(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task CrearPedidoConClienteAsync(PedidosModel pedidoConCliente)
        {
            await _pedidoRepository.CrearPedidoConClienteAsync(pedidoConCliente);
        }

        public async Task<List<Pedido>> ObtenerPedidosPorClienteAsync(int clienteId)
        {
            if (clienteId <= 0)
                throw new ArgumentException("El ID del cliente no es válido.");

            return await _pedidoRepository.ObtenerPedidosPorClienteAsync(clienteId);
        }
    }
}
