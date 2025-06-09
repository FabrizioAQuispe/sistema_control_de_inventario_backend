namespace SistemaControlDeInventario.Models
{
    public class UsuariosDTO
    {
        public int usuario_id { get; set; }
        public string nombre_usuario { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int rol_id { get; set; }
        public string? token { get; set; }
    }
}
