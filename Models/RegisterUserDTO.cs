namespace SistemaControlDeInventario.Models
{
    public class RegisterUserDTO
    {
        public int usuario_id { get; set; }
        public string nombre_usuario { get; set; } 
        public string email { get; set; }   
        public string password { get; set; }
        public int rol_id { get; set; }
    }
}
