using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RigoRigo.Data.Context;
using RigoRigo.Data.Interfaces;
using RigoRigo.Entities.Entities;

namespace RigoRigo.Data.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly RigoRigoContext _context;

        public PedidoRepository(RigoRigoContext context)
        {
            _context = context;
        }

        public async Task CrearPedidoConClienteAsync(PedidoConCliente pedidoConCliente)
        {
            var detallesJson = System.Text.Json.JsonSerializer.Serialize(pedidoConCliente.Detalles);
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                EXEC CrearPedidoConCliente
                    @Cedula = {pedidoConCliente.Cedula},
                    @Nombre = {pedidoConCliente.Nombre},
                    @Direccion = {pedidoConCliente.Direccion},
                    @Detalles = {detallesJson}
            ");
        }

        public async Task<List<Pedido>> ObtenerPedidosPorClienteAsync(int clienteId)
        {
            return await _context.Pedidos
                .FromSqlInterpolated($@"
                    EXEC ObtenerPedidosPorCliente 
                        @ClienteID = {clienteId}
                ")
                .Include(p => p.Detalles)
                .ThenInclude(d => d.Producto)
                .ToListAsync();
        }
    }
}
