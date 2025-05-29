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

        [HttpPost]
        [Route("/crear_productos")]
        public async Task<bool> CrearProductos(ProductosDTO productosInput)
        {
            try
            {
                var result = await _productosService.CrearProductos(productosInput);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR HANDLE CREATE PROUDUCTOS: " + ex.Message.ToString());
            }
        }

        [HttpPut]
        [Route("/editar_productos")]
        public async Task<bool> EditarProductos(ProductosDTO productosInput)
        {
            try
            {
                var result = await _productosService.EditarProductos(productosInput);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR EDITAR PRODUCTOS: " + ex.Message.ToString());
            }
        }

        [HttpDelete]
        [Route("/eliminar/{id_prod}")]
        public async Task<bool> DeleteProductos(int id_prod)
        {
            return await _productosService.DeleteProductos(id_prod);
        }

    }
}
