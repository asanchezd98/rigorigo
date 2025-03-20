using Microsoft.AspNetCore.Mvc;
using RigoRigo.Business.Interfaces;
using RigoRigo.Entities.Entities;

namespace RigoRigo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidosController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearPedido([FromBody] PedidoConCliente pedido)
        {
            if (pedido == null)
            {
                return BadRequest("El pedido no puede ser nulo.");
            }

            try
            {
                await _pedidoService.CrearPedidoConClienteAsync(pedido);
                return Ok("Pedido guardado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("{clienteId}")]
        public async Task<IActionResult> ObtenerPedidosPorCliente(int clienteId)
        {
            if (clienteId <= 0)
            {
                return BadRequest("El ID del cliente no es válido.");
            }

            try
            {
                var pedidos = await _pedidoService.ObtenerPedidosPorClienteAsync(clienteId);
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
