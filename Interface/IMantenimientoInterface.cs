using SistemaControlDeInventario.Models;

namespace SistemaControlDeInventario.Interface
{
    public interface IMantenimientoInterface
    {
        Task<List<MantenimientoTableDTO>> ListarMantenimiento();
        Task<List<ProductosDTO>> ListarNombreProducto(string nombre);
        Task<bool> CrearMantenimiento(MantenimientoDTO mantenimientoInput);
        Task<MantenimientoDTO> UpdaterManenimiento(MantenimientoDTO mantenimientoInput);
        Task<bool> DeleteMantenimiento(int id_mantenimiento);

        
        //-----------------------------INTERFACES PARA LOS MOVIMIENTOS------------------------------------//
        Task<List<ListaMovimientosDTO>> ListarMovimientos();
        Task<List<ListaMovimientosDTO>> BuscarMovimientos(string? nombre,DateTime?fecha_final,DateTime? fecha_inicial);
    }
}
