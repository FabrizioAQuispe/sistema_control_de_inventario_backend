namespace SistemaControlDeInventario.Models
{
    public class ListaMovimientosDTO
    {
        public int id_mant { get; set; }
        public string nombre { get; set; }
        public string fecha { get; set; }
        public string referencia { get; set; }
        public string ingresos { get; set; }
        public string salidas { get; set; }
        public string stock { get; set; }
        public string? usuario_master { get; set; }
    }
}
