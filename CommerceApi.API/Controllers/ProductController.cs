using CommerceApi.BLL.Services;
using CommerceApi.BLL.Utilities;
using CommerceApi.DTO.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommerceApi.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        private ObjectResult NotFoundMsg(string id) => NotFound($"Product with the ProductId = {id} was not found");

        public ProductController(IProductService service)
        {
            _service = service;
        }

        public async Task<IActionResult> GetProductsAsync()
        {
            try
            {
                return Ok(await _service.GetProductsAsync());
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> AddProductAsync(ProductToAddDto product)
        {
            try
            {
                return Ok(await _service.AddProductAsync(product));
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        // GET: api/products/{item_id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(string id)
        {
            try
            {
                return Ok(await _service.GetProductAsync(id));
            }
            catch (NotFoundException)
            {
                return NotFoundMsg(id);
            }
            catch (Exception)
            { 
                return BadRequest("Something went wrong");
            }
        }

        // PUT: api/products/{item_id}
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> UpdateProductAsync(string id, ProductToUpdateDto update)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != update.ProductId)
                return BadRequest("Id does not match request body");

            try
            {
                return Ok(await _service.UpdateProductAsync(update));
            }
            catch (NotFoundException)
            {
                return NotFoundMsg(id);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        // DELETE: api/products/{item_id}
        [HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> DeleteProductAsync(string id)
        {
            try
            {
                await _service.DeleteProductAsync(id);
                return Ok($"Product with the ProductId = {id} was successfully deleted");
            }
            catch (NotFoundException)
            {
                return NotFoundMsg(id);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

    }
}
