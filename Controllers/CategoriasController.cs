using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaControlDeInventario.Interface;
using SistemaControlDeInventario.Models;

namespace SistemaControlDeInventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : Controller
    {
        private readonly ICategoriaInterface _categoriaService;
        public CategoriasController(ICategoriaInterface categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet("ListarCategorias")]
        public async Task<List<CategoriasDTO>> ListarCategorias()
        {
            return await _categoriaService.ListarCategorias();  
        }
    }
}
