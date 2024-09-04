using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ProductosCRUD.Entities;
using ProductosCRUD.Services;

namespace ProductosCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoService _productoService;

        public ProductoController(ProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            List<Producto> list = await _productoService.Get();
            return StatusCode(StatusCodes.Status200OK, list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            Producto producto = await _productoService.GetById(id);
            return StatusCode(StatusCodes.Status200OK, producto);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Producto producto)
        {
            bool result = await _productoService.Create(producto);
            return StatusCode(StatusCodes.Status200OK, new {isSuccess = result});
        }

        [HttpPut]
        public async Task<ActionResult> Edit([FromBody] Producto producto)
        {
            bool result = await _productoService.Edit(producto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = result });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete (int id)
        {
            bool result = await _productoService.Delete(id);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = result });
        }
    }
}
