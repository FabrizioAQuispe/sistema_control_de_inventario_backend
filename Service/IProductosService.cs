using Microsoft.Data.SqlClient;
using SistemaControlDeInventario.Interface;
using SistemaControlDeInventario.Models;

namespace SistemaControlDeInventario.Service
{
    [Serializable]
    public class IProductosService : IProductosInterface
    {
        private readonly IConfiguration _configuration;
        public IProductosService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task<bool> CrearProductos(ProductosDTO productosInput)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProductos(int id_prod)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditarProductos(ProductosDTO productosInput)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductosDTO>> ListProductos()
        {
            try
            {
                string conn = _configuration.GetConnectionString("SQL");
                string query = "SELECT * FROM Productos";
                List<ProductosDTO> listarProductos = new List<ProductosDTO>();
                SqlConnection conex = new SqlConnection(conn);
                await conex.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query,conex))
                {
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();
                    while (await dr.ReadAsync())
                    {
                        ProductosDTO productos = new ProductosDTO()
                        {
                            id_prod = Convert.ToInt32(dr["id_prod"].ToString()),
                            nombre = dr["nombre"].ToString(),
                            descripcion = dr["descripcion"].ToString(),
                            categoria = dr["categoria"].ToString(),
                            referencia = dr["referencia"].ToString()
                        };
                        listarProductos.Add(productos);
                    }
                }
                return listarProductos;
            }catch (Exception ex)
            {
                throw new Exception("ERROR LISTAR PRODUCTOS: " + ex.Message);
            }
        }
    }
}
