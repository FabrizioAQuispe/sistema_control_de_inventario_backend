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
        public async Task<bool> CrearProductos(ProductosDTO productosInput)
        {
            try
            {
                string conn = _configuration.GetConnectionString("SQL");
                string query = @"INSERT INTO Productos (nombre, categoria,estado)
                                 VALUES (@nombre,@categoria,@estado)";

                SqlConnection conex = new SqlConnection(conn);
                await conex.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(query,conex))
                {
                    cmd.Parameters.AddWithValue("@nombre",productosInput.nombre);
                    cmd.Parameters.AddWithValue("@categoria", productosInput.categoria);
                    cmd.Parameters.AddWithValue("@estado", productosInput.estado);
                    
                    await cmd.ExecuteNonQueryAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR SERVER RESPONSE: " + ex.Message.ToString());
                return false;
            }
        }

        public async Task<bool> DeleteProductos(int id_prod)
        {
            try
            {
                string con = _configuration.GetConnectionString("SQL");
                string query = "DELETE FROM Productos WHERE id_prod = @id_prod";
                SqlConnection conex = new SqlConnection(con);
                await conex.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, conex))
                {
                    cmd.Parameters.AddWithValue("@id_prod", id_prod);
                    await cmd.ExecuteNonQueryAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR DELETE PRODUCTOS: " + ex.Message.ToString());
                return false;
            }
        }

        public async Task<bool> EditarProductos(ProductosDTO productosInput)
        {
            try
            {
                string conn = _configuration.GetConnectionString("SQL");

                string query = @"UPDATE Productos SET nombre = @nombre, categoria = @categoria, estado = @estado WHERE id_prod = @id_prod";

                SqlConnection conex = new SqlConnection(conn);
                await conex.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(query, conex))
                {
                    cmd.Parameters.AddWithValue("@id_prod", productosInput.id_prod);
                    cmd.Parameters.AddWithValue("@nombre", productosInput.nombre);
                    cmd.Parameters.AddWithValue("@categoria", productosInput.categoria);
                    cmd.Parameters.AddWithValue("@estado", productosInput.estado);
                    await cmd.ExecuteNonQueryAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR EDITAR PRODUCTOS: " + ex.Message.ToString());
            }
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
                        //int idTipo = Convert.ToInt32(dr["tipo"]);
                        //string tipoIngreso = idTipo == 1 ? "Ingreso" : idTipo == 2 ? "Salida" : "Desconocido";

                        ProductosDTO productos = new ProductosDTO()
                        {
                            id_prod = Convert.ToInt32(dr["id_prod"].ToString()),
                            nombre = dr["nombre"].ToString(),
                            categoria = dr["categoria"].ToString(),
                            estado = dr["estado"].ToString(),
                            //tipo = tipoIngreso
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
