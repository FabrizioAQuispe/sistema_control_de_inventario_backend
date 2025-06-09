namespace SistemaControlDeInventario.Models
{
    public class PerfilDTO
    {
        public int usuario_id { get; set; }
        public string nombre_usuario { get; set; }
        public string email { get; set; }
        public int rol_id { get; set; }
        public string nombre_rol { get; set; }
        public string nombre_permiso { get; set; }
        public string? token { get; set; }  
    }
}
