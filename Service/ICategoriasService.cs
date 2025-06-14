using Microsoft.Data.SqlClient;
using SistemaControlDeInventario.Interface;
using SistemaControlDeInventario.Models;

namespace SistemaControlDeInventario.Service
{
    public class ICategoriasService : ICategoriaInterface
    {
        private readonly IConfiguration _configuration;
        public ICategoriasService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<List<CategoriasDTO>> ListarCategorias()
        {
            try
            {
                List<CategoriasDTO> listaCategorias = new List<CategoriasDTO>();
                string query = "SELECT nombre,descripcion FROM Categoria";
                string conex = _configuration.GetConnectionString("SQL");
                SqlConnection conn = new SqlConnection(conex);
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, conn);

                using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        CategoriasDTO categorias = new CategoriasDTO
                        {
                            nombre = dr["nombre"].ToString(),
                            descripcion = dr["descripcion"].ToString()
                        };
                        listaCategorias.Add(categorias);
                    }
                }
                return listaCategorias;
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR AL DEVOLVER LOS DATOS DEL SERVIDOR LISTAR CATEGORIAS: " + ex.Message.ToString());
            }
        }
    }
}
