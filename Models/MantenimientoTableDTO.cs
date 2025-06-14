namespace SistemaControlDeInventario.Models
{
    public class MantenimientoTableDTO
    {
        public int id_mant { get; set; }
        public string nombre { get; set; }
        public string cantidad { get; set; }
        public string categoria { get; set; }
        public string referencia { get; set; }
        public string tipo { get; set; }
        public string fecha { get; set; }
        public string? usuario_master { get; set; }
    }
}
