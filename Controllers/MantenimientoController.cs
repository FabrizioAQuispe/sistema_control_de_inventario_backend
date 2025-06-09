using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaControlDeInventario.Interface;
using SistemaControlDeInventario.Models;

namespace SistemaControlDeInventario.Controllers
{
    [Authorize, Route("api/[controller]"), ApiController]
    public class MantenimientoController(IMantenimientoInterface service) : ControllerBase
    {
        [HttpGet("buscar_nombre")]
        public async Task<List<ProductosDTO>> ListarNombreProducto(string nombre) =>
            await service.ListarNombreProducto(nombre);

        [HttpGet("buscar_seguimiento")]
        public async Task<List<MantenimientoTableDTO>> ListarMantenimiento() =>
            await service.ListarMantenimiento();

        [HttpGet("listar_movimientos")]
        public async Task<List<ListaMovimientosDTO>> ListarMovimientos() =>
            await service.ListarMovimientos();

        [HttpGet("filtrar_movimiento")]
        public async Task<List<ListaMovimientosDTO>> BuscarMovimientos(
            [FromQuery] string? nombre, [FromQuery] string? referencia, [FromQuery] DateTime? fecha) =>
            await service.BuscarMovimientos(nombre, referencia, fecha);

        [HttpPost("crear_seguimiento")]
        public async Task<bool> CrearMantenimiento(MantenimientoDTO input) =>
            await service.CrearMantenimiento(input);
    }
}