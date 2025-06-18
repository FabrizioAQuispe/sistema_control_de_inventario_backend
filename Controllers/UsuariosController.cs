using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaControlDeInventario.Interface;
using SistemaControlDeInventario.Models;

namespace SistemaControlDeInventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : Controller
    {
        private readonly IUsuariosInterface _usuariosService;
        public UsuariosController(IUsuariosInterface usuariosService)
        {
            _usuariosService = usuariosService;
        }
        [HttpPost]
        [Route("/auth/users")]
        public async Task<List<PerfilDTO>> PerfilUsuario(LoginInputDTO loginInput)
        {
            try
            {
                var perfil = await _usuariosService.PerfilUsuario(loginInput);
                return perfil;
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR SERVER API AUTH: " + ex.Message.ToString());
            }
        }

        [HttpPost]
        [Route("/auth/register")]
        public async Task<bool> RegistrarUsuario(RegisterUserDTO user)
        {
            var response = await _usuariosService.RegistrarUsuario(user);
            return response;
        }
    }
}
