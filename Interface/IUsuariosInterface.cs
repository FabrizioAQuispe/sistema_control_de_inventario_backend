using SistemaControlDeInventario.Models;

namespace SistemaControlDeInventario.Interface
{
    public interface IUsuariosInterface
    {
        Task<List<PerfilDTO>> PerfilUsuario(LoginInputDTO loginInput);
    }
}
