using System.Numerics;

namespace SistemaControlDeInventario.Models
{
    [Serializable]
    public class MantenimientoDTO
    {
        public int id_mant { get; set; }    
        public int id_prod { get; set; }
        public int id_tipo { get; set; }
        public DateTime fecha_ingreso { get; set; }
        public BigInteger cantidad { get; set; }    
        public int estado { get; set; }
    }
}
