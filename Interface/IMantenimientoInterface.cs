using SistemaControlDeInventario.Models;

namespace SistemaControlDeInventario.Interface
{
    public interface IMantenimientoInterface
    {
        Task<List<ProductosDTO>> ListarNombreProducto(string nombre);
        Task<bool> CrearMantenimiento(MantenimientoDTO mantenimientoInput);
        Task<List<MantenimientoTableDTO>> ListarMantenimiento();
    }
}
