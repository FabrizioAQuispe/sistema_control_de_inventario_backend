using SistemaControlDeInventario.Models;

namespace SistemaControlDeInventario.Interface
{
    public interface IMantenimientoInterface
    {
        Task<List<ProductosDTO>> ListarNombreProducto(string nombre);
        Task<bool> CrearMantenimiento(MantenimientoDTO mantenimientoInput);
        Task<List<MantenimientoTableDTO>> ListarMantenimiento();

        //-----------------------------INTERFACES PARA LOS MOVIMIENTOS------------------------------------//
        Task<List<ListaMovimientosDTO>> ListarMovimientos();
        Task<List<ListaMovimientosDTO>> BuscarMovimientos(string? nombre,string? referencia,DateTime? fecha);
    }
}
