using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RigoRigo.Business.Interfaces;
using RigoRigo.Data.Interfaces;
using RigoRigo.Entities.Entities;

namespace RigoRigo.Business.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<List<Producto>> ObtenerProductosAsync()
        {
            return await _productoRepository.ObtenerProductosAsync();
        }
    }
}
