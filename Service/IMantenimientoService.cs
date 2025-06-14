using Microsoft.Data.SqlClient;
using SistemaControlDeInventario.Interface;
using SistemaControlDeInventario.Models;
using System.Data;

namespace SistemaControlDeInventario.Service
{
    public class MantenimientoService : IMantenimientoInterface
    {
        private readonly string _connectionString;

        public MantenimientoService(IConfiguration configuration) =>
            _connectionString = configuration.GetConnectionString("SQL")
                ?? throw new InvalidOperationException("Connection string 'SQL' not found.");

        public async Task<bool> CrearMantenimiento(MantenimientoDTO input) =>
            await ExecuteNonQueryAsync(
                "INSERT INTO Mantenimiento(id_prod, id_tipo, fecha, fecha_movimiento, cantidad, nombre, referencia,usuario_master) VALUES (@id_prod, @id_tipo, @fecha, @fecha_movimiento, @cantidad, @nombre, @referencia,@usuario_master)",
                cmd => {
                    cmd.Parameters.AddWithValue("@id_prod", input.id_prod);
                    cmd.Parameters.AddWithValue("@nombre", input.nombre ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@fecha", input.fecha);
                    cmd.Parameters.AddWithValue("@fecha_movimiento", DateTime.Now);
                    cmd.Parameters.AddWithValue("@cantidad", input.cantidad);
                    cmd.Parameters.AddWithValue("@referencia", input.referencia ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@usuario_master",input.usuario_master);
                    cmd.Parameters.AddWithValue("@id_tipo", input.id_tipo);
                });

        public async Task<List<MantenimientoTableDTO>> ListarMantenimiento() =>
            await ExecuteReaderAsync(
                "SELECT P.nombre, M.usuario_master,M.cantidad,M.id_mant, P.categoria, M.referencia, M.fecha, M.id_tipo AS tipo FROM Mantenimiento M INNER JOIN Productos P ON P.id_prod = M.id_prod",
                r => new MantenimientoTableDTO
                {
                    id_mant = Convert.ToInt32(r["id_mant"].ToString()),
                    nombre = r["nombre"]?.ToString() ?? "",
                    cantidad = r["cantidad"]?.ToString() ?? "",
                    tipo = Convert.ToInt32(r["tipo"]) switch { 1 => "Ingreso", 2 => "Salida", _ => "Desconocido" },
                    fecha = r["fecha"]?.ToString() ?? "",
                    categoria = r["categoria"]?.ToString() ?? "",
                    referencia = r["referencia"]?.ToString() ?? "",
                    usuario_master = r["usuario_master"].ToString() ?? ""
                    
                });

        public async Task<List<ListaMovimientosDTO>> BuscarMovimientos(string? nombre, DateTime?fecha_inicial, DateTime? fecha_final) =>
            await ExecuteStoredProcedureAsync("SP_BUSCAR_MOVIMIENTOS", MapMovimientos, cmd => {
                cmd.Parameters.AddWithValue("@nombre", nombre ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@fecha_inicial", fecha_inicial ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@fecha_final", fecha_final ?? (object)DBNull.Value);
            });

        public async Task<List<ListaMovimientosDTO>> ListarMovimientos() =>
            await ExecuteStoredProcedureAsync("SP_LIST_MOVIMIENTOS", MapMovimientos);

        public async Task<List<ProductosDTO>> ListarNombreProducto(string nombre) =>
            string.IsNullOrWhiteSpace(nombre) ? new() :
            await ExecuteReaderAsync(
                "SELECT id_prod, nombre FROM Productos WHERE nombre LIKE @nombre",
                r => new ProductosDTO
                {
                    id_prod = Convert.ToInt32(r["id_prod"]),
                    nombre = r["nombre"]?.ToString() ?? ""
                },
                cmd => cmd.Parameters.AddWithValue("@nombre", $"%{nombre}%"));

        private static ListaMovimientosDTO MapMovimientos(SqlDataReader r) => new()
        {
            fecha = r["fecha"]?.ToString() ?? "",
            nombre = r["nombre"]?.ToString() ?? "",
            referencia = r["referencia"]?.ToString() ?? "",
            ingresos = r["ingresos"]?.ToString() ?? "",
            salidas = r["salidas"]?.ToString() ?? "",
            stock = r["stock"]?.ToString() ?? "",
            usuario_master = r["usuario_master"].ToString() ?? ""

        };

        private async Task<bool> ExecuteNonQueryAsync(string query, Action<SqlCommand> setupParams)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                using var cmd = new SqlCommand(query, conn);
                setupParams(cmd);
                await conn.OpenAsync();
                var result = await cmd.ExecuteNonQueryAsync();
                return result > 0;
            }
            catch (Exception ex) { throw new Exception($"Database error: {ex.Message}", ex); }
        }

        private async Task<List<T>> ExecuteReaderAsync<T>(string query, Func<SqlDataReader, T> mapper, Action<SqlCommand>? setupParams = null)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                using var cmd = new SqlCommand(query, conn);
                setupParams?.Invoke(cmd);
                await conn.OpenAsync();
                using var reader = await cmd.ExecuteReaderAsync();
                var result = new List<T>();
                while (await reader.ReadAsync()) result.Add(mapper(reader));
                return result;
            }
            catch (Exception ex) { throw new Exception($"Database error: {ex.Message}", ex); }
        }

        private async Task<List<T>> ExecuteStoredProcedureAsync<T>(string spName, Func<SqlDataReader, T> mapper, Action<SqlCommand>? setupParams = null)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                using var cmd = new SqlCommand(spName, conn) { CommandType = CommandType.StoredProcedure };
                setupParams?.Invoke(cmd);
                await conn.OpenAsync();
                using var reader = await cmd.ExecuteReaderAsync();
                var result = new List<T>();
                while (await reader.ReadAsync()) result.Add(mapper(reader));
                return result;
            }
            catch (Exception ex) { throw new Exception($"Database error: {ex.Message}", ex); }
        }

        public async Task<MantenimientoDTO> UpdaterManenimiento(MantenimientoDTO mantenimientoInput)
        {
            string conn = _connectionString;
            string query = @"
                 UPDATE Mantenimiento 
                        SET fecha = @fecha,
                            fecha_movimiento = GETDATE(),
	                        cantidad = @cantidad,
	                        nombre = @nombre,
	                        referencia = @referencia
                 WHERE      id_mant = @id_mant;
            ";

            SqlConnection conex = new SqlConnection(conn);
            await conex.OpenAsync();

            using (SqlCommand cmd = new SqlCommand(query,conex))
            {
                cmd.Parameters.AddWithValue("@id_mant",mantenimientoInput.id_mant);
                cmd.Parameters.AddWithValue("@fecha",mantenimientoInput.fecha);
                cmd.Parameters.AddWithValue("@cantidad",mantenimientoInput.cantidad);
                cmd.Parameters.AddWithValue("@nombre",mantenimientoInput.nombre);
                cmd.Parameters.AddWithValue("@referencia", mantenimientoInput.referencia);

                await cmd.ExecuteNonQueryAsync();
            }
            return mantenimientoInput;
        }

        public async Task<bool> DeleteMantenimiento(int id_mantenimiento)
        {
            try
            {
                string conn = _connectionString;
                string query = "DELETE FROM Mantenimiento WHERE id_mant = @id_mant;";

                SqlConnection conex = new SqlConnection(conn);
                await conex.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, conex))
                {
                    cmd.Parameters.AddWithValue("@id_mant", id_mantenimiento);
                    await cmd.ExecuteNonQueryAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR SERVER DELETE MANTENIMIENTO: " + ex.Message.ToString());
            }
        }
    }
}