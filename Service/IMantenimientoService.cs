using Microsoft.Data.SqlClient;
using SistemaControlDeInventario.Interface;
using SistemaControlDeInventario.Models;

namespace SistemaControlDeInventario.Service
{
    public class IMantenimientoService : IMantenimientoInterface
    {
        private readonly IConfiguration _configuration;
        public IMantenimientoService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> CrearMantenimiento(MantenimientoDTO mantenimientoInput)
        {
            try
            {
                string query = @"INSERT INTO Mantenimiento(id_prod, fecha_ingreso,fecha_movimiento, cantidad,nombre,referencia) VALUES (@id_prod, @fecha_ingreso,@fecha_movimiento, @cantidad,@nombre,@referencia);";
                string conex = _configuration.GetConnectionString("SQL");
                SqlConnection conn = new SqlConnection(conex);
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id_prod", mantenimientoInput.id_prod);
                cmd.Parameters.AddWithValue("@fecha_ingreso", mantenimientoInput.fecha_ingreso);
                cmd.Parameters.AddWithValue("@fecha_movimiento", DateTime.Now);
                cmd.Parameters.AddWithValue("@cantidad", mantenimientoInput.cantidad);
                cmd.Parameters.AddWithValue("@nombre", mantenimientoInput.nombre);
                cmd.Parameters.AddWithValue("@referencia", mantenimientoInput.referencia);
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                await conn.CloseAsync();
                return rowsAffected > 0; // Retorna true si se insertó al menos una fila
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR CREAR MANTENIMIENTO SERVER: " + ex.Message.ToString());
            }
        }

        public async Task<List<MantenimientoTableDTO>> ListarMantenimiento()
        {
            try
            {
                string conex = _configuration.GetConnectionString("SQL");
                string query = @"SELECT P.nombre,M.cantidad,P.categoria,P.referencia,P.tipo AS tipo,M.fecha_ingreso AS fecha_ingreso
                                 FROM Mantenimiento M INNER JOIN Productos P ON P.id_prod = P.id_prod";

                List<MantenimientoTableDTO> mantenimientos = new List<MantenimientoTableDTO>();
                SqlConnection conn = new SqlConnection(conex);
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, conn);
                using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        int idTipo = Convert.ToInt32(dr["tipo"]);
                        string tipoIngreso = idTipo == 1 ? "Ingreso" : idTipo == 2 ? "Salida" : "Desconocido";

                        MantenimientoTableDTO mantenimiento = new MantenimientoTableDTO
                        {
                            nombre = dr["nombre"].ToString(),
                            cantidad = dr["cantidad"].ToString(),
                            referencia = dr["referencia"].ToString(),
                            categoria = dr["categoria"].ToString(),
                            tipo = tipoIngreso,
                            fecha_ingreso = dr["fecha_ingreso"].ToString()
                        };
                        mantenimientos.Add(mantenimiento);
                    }
                }
                await conn.CloseAsync();
                return mantenimientos;
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR LISTAR MANTENIMIENTOS: " + ex.Message.ToString());
            }
        }

        public async Task<List<ProductosDTO>> ListarNombreProducto(string nombre)
        {
            try
            {
                string conex = _configuration.GetConnectionString("SQL");
                string nombreBuscar = "%" + nombre + "%";
                string query = @"SELECT id_prod,nombre FROM Productos WHERE nombre LIKE @nombre";
                List<ProductosDTO> productos = new List<ProductosDTO>();

                SqlConnection conn = new SqlConnection(conex);
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nombre", nombreBuscar);
                using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                {
                    while(await dr.ReadAsync())
                    {
                        ProductosDTO producto = new ProductosDTO
                        {
                            id_prod = Convert.ToInt32(dr["id_prod"]),
                            nombre = dr["nombre"].ToString()
                        };
                        productos.Add(producto);
                    }
                }
                return productos;
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR ListarNombreProducto: " + ex.Message.ToString());
            }
        }
    }
}
