using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaControlDeInventario.Interface;
using SistemaControlDeInventario.Models;

namespace SistemaControlDeInventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductosInterface _productosService;
        public ProductosController(IProductosInterface productosService)
        {
            _productosService = productosService;
        }

        [HttpGet]
        [Route("/listar_productos")]
        public async Task<List<ProductosDTO>> ListProductos()
        {
            try
            {
                var result = await _productosService.ListProductos();
                return result;
            }catch (Exception ex)
            {
                throw new Exception("ERROR SERVER RESPONSE: " + ex.Message.ToString());
            }
        }
    }
}
