using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RigoRigo.Entities.Entities;

namespace RigoRigo.Business.Interfaces
{
    public interface IProductoService
    {
        Task<List<Producto>> ObtenerProductosAsync();
    }
}
