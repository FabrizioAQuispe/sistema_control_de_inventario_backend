using Microsoft.Data.SqlClient;
using SistemaControlDeInventario.Interface;
using SistemaControlDeInventario.Models;

namespace SistemaControlDeInventario.Service
{
    public class IDashboardService : IDashboardInterface
    {
        private readonly IConfiguration _configuration;
        public IDashboardService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<CantidadIngresosDTO>> ListarCantidadIngresos()
        {
            string conn = _configuration.GetConnectionString("SQL");
            List<CantidadIngresosDTO> listaIngresos = new List<CantidadIngresosDTO>();
            
            SqlConnection conex = new SqlConnection(conn);
           
            using (conex)
            {
                await conex.OpenAsync();
                SqlCommand cmd = new SqlCommand("SP_LIST_SALIDAS", conex);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        CantidadIngresosDTO cantidadIngresos = new CantidadIngresosDTO
                        {
                            ingresos = Convert.ToInt32(reader["SALIDAS"])
                        };
                        listaIngresos.Add(cantidadIngresos);
                    }
                }
            }
            return listaIngresos;
        }

        public async Task<List<CantidadSalidas>> ListarCantidadSalidas()
        {
            string conn = _configuration.GetConnectionString("SQL");
            List<CantidadSalidas> listaSalidas = new List<CantidadSalidas>();

            SqlConnection conex = new SqlConnection(conn);

            using (conex)
            {
                await conex.OpenAsync();
                SqlCommand cmd = new SqlCommand("SP_LIST_INGRESOS", conex);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        CantidadSalidas cantidadIngresos = new CantidadSalidas
                        {
                            salidas = Convert.ToInt32(reader["INGRESOS"])
                        };
                        listaSalidas.Add(cantidadIngresos);
                    }
                }
            }
            return listaSalidas;
        }

        public async Task<List<CantidadStockDTO>> ListarStockTotal()
        {
            string conn = _configuration.GetConnectionString("SQL");
            List<CantidadStockDTO> listaStock = new List<CantidadStockDTO>();
            SqlConnection conex = new SqlConnection(conn);
            using (conex)
            {
                await conex.OpenAsync();
                SqlCommand cmd = new SqlCommand("SP_LIST_STOCK_TOTAL", conex);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        CantidadStockDTO cantidadStock = new CantidadStockDTO()
                        {
                            stock_total = Convert.ToInt32(reader["STOCK_TOTAL"])
                        };
                    listaStock.Add(cantidadStock);
                    }
                }
            }
            return listaStock;
        }
    }
}
