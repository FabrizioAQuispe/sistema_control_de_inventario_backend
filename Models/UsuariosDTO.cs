namespace SistemaControlDeInventario.Models
{
    [Serializable]
    public class UsuariosDTO
    {
        public int id_user { get; set; }
        public string nombre { get; set; }  
        public string username { get; set; }
        public string password { get; set; }
        public DateTime fecha_created { get; set; } 
        public string email { get; set; }
        public string rol { get; set; } 
    }
}
