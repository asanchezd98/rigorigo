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
    public class ProductoRepository : IProductoRepository
    {
        private readonly RigoRigoContext _context;

        public ProductoRepository(RigoRigoContext context)
        {
            _context = context;
        }

        // Obtener todos los productos de manera asíncrona
        public async Task<List<Producto>> ObtenerProductosAsync()
        {
            return await _context.Productos
                .FromSqlInterpolated($@"EXEC ObtenerProductos")
                .ToListAsync();
        }
    }
}
