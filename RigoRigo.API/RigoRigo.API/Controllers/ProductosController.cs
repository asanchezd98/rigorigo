using Microsoft.AspNetCore.Mvc;
using RigoRigo.Business.Interfaces;
using RigoRigo.Entities.Entities;

namespace RigoRigo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Producto>>> ObtenerProductos()
        {
            var productos = await _productoService.ObtenerProductosAsync();
            return Ok(productos);
        }
    }
}
