using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RigoRigo.Entities.Entities;

namespace RigoRigo.Data.Interfaces
{
    public interface IProductoRepository
    {
        Task<List<Producto>> ObtenerProductosAsync();
    }
}
