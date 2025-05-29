using SistemaControlDeInventario.Models;

namespace SistemaControlDeInventario.Interface
{
    public interface IProductosInterface
    {
        Task<bool> CrearProductos(ProductosDTO productosInput);
        Task<List<ProductosDTO>> ListProductos();
        Task<bool> DeleteProductos(int id_prod);
        Task<bool> EditarProductos(ProductosDTO productosInput);
    }
}
