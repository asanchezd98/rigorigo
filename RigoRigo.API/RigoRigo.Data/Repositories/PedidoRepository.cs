using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RigoRigo.Data.Context;
using RigoRigo.Data.Interfaces;
using RigoRigo.Data.Models;
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

        public async Task CrearPedidoConClienteAsync(PedidosModel pedido)
        {
            var detallesJson = System.Text.Json.JsonSerializer.Serialize(pedido.detalles);
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                EXEC CrearPedidoConCliente
                    @Cedula = {pedido.cedula},
                    @Nombre = {pedido.nombre},
                    @Direccion = {pedido.direccion},
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
