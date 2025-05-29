using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaControlDeInventario.Interface;
using SistemaControlDeInventario.Models;

namespace SistemaControlDeInventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MantenimientoController : ControllerBase
    {
        private readonly IMantenimientoInterface _mantenimientoService;
        public MantenimientoController(IMantenimientoInterface mantenimientoService)
        {
            _mantenimientoService = mantenimientoService;
        }
        [HttpGet("buscar_nombre")]
        public async Task<List<ProductosDTO>> ListarNombreProducto(string nombre)
        {
            try
            {
                var productos = await _mantenimientoService.ListarNombreProducto(nombre);
                return productos;
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR ListarNombreProducto: " + ex.Message.ToString());
            }
        }

        [HttpGet("buscar_seguimiento")]
        public async Task<List<MantenimientoTableDTO>> ListarMantenimiento()
        {
            return await _mantenimientoService.ListarMantenimiento();
        }

        [HttpPost("crear_seguimiento")]
        public async Task<bool> CrearMantenimiento(MantenimientoDTO mantenimientoInput)
        {
            return await _mantenimientoService.CrearMantenimiento(mantenimientoInput);
        }
    }
}
