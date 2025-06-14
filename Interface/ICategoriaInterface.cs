using SistemaControlDeInventario.Models;

namespace SistemaControlDeInventario.Interface
{
    public interface ICategoriaInterface
    {
        //CRUD PARA EL CONTROL DE INVENTARIOS
        Task<List<CategoriasDTO>> ListarCategorias();
    }
}
