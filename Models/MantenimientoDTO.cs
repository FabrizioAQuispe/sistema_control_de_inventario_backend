using System.Numerics;

namespace SistemaControlDeInventario.Models
{
    [Serializable]
    public class MantenimientoDTO
    {
        public int id_mant { get; set; }    
        public int id_prod { get; set; }
        public int id_tipo { get; set; }
        public string nombre { get; set; }
        //public string tipo { get; set; }
        public DateTime fecha { get; set; }
        public string referencia { get; set; }
        public int cantidad { get; set; }
        public int estado { get; set; }
        public string? usuario_master { get; set; }
    }
}
