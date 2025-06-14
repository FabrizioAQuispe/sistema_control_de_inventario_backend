using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaControlDeInventario.Interface;
using SistemaControlDeInventario.Models;

namespace SistemaControlDeInventario.Controllers
{
    [Route("api/[controller]"), ApiController]
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
            [FromQuery] string? nombre, [FromQuery] DateTime? fecha_inicial, [FromQuery] DateTime? fecha_final) =>
            await service.BuscarMovimientos(nombre, fecha_inicial, fecha_final);

        [HttpPost("crear_seguimiento")]
        public async Task<bool> CrearMantenimiento(MantenimientoDTO input) =>
            await service.CrearMantenimiento(input);

        [HttpPut("actualizar_mantenimiento")]
        public async Task<MantenimientoDTO> UpdaterManenimiento([FromBody] MantenimientoDTO mantenimientoInput) =>
            await service.UpdaterManenimiento(mantenimientoInput);

        [HttpDelete("eliminar_seguimiento/{id_mantenimiento}")]
        public async Task<bool> DeleteMantenimiento(int id_mantenimiento) => 
            await service.DeleteMantenimiento(id_mantenimiento);

    }
}