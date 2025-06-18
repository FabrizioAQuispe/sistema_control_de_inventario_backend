using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaControlDeInventario.Interface;
using SistemaControlDeInventario.Models;

namespace SistemaControlDeInventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : Controller
    {
        private readonly IDashboardInterface _dashboardService;
        public DashboardController(IDashboardInterface dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("Listar_Ingresos")]
        public async Task<IActionResult> ListarIngresos()
        {
            try
            {
                var ingresos = await _dashboardService.ListarCantidadIngresos();
                return Ok(ingresos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al obtener los ingresos: {ex.Message}");
            }
        }

        [HttpGet("Listar_Salidas")]
        public async Task<IActionResult> ListarCantidadSalidas()
        {
            try
            {
                var salidas = await _dashboardService.ListarCantidadSalidas();
                return Ok(salidas);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener las salidas: {ex.Message}");
            }
        }

        [HttpGet("Listar_Stock_Total")]
        public async Task<List<CantidadStockDTO>> ListarStockTotal()
        {
            return await _dashboardService.ListarStockTotal();
        }
    }
}
