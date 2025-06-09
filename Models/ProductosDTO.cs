namespace SistemaControlDeInventario.Models
{
    [Serializable]
    public class ProductosDTO
    {
        public int id_prod { get; set; }
        public string nombre { get; set; }
        //public string descripcion { get; set; }
        public string categoria { get; set; }   
        //public string referencia { get; set; }  
        public string estado { get; set; }
        //public string estado_prod { get; set; }
        //public string tipo { get; set; }

    }
}
