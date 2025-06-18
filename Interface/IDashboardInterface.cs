using SistemaControlDeInventario.Models;

namespace SistemaControlDeInventario.Interface
{
    public interface IDashboardInterface
    {
        Task<List<CantidadIngresosDTO>> ListarCantidadIngresos();
        Task<List<CantidadSalidas>> ListarCantidadSalidas();
        Task<List<CantidadStockDTO>> ListarStockTotal();
    }
}
