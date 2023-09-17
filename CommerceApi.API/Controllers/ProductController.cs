using CommerceApi.BLL.Services;
using CommerceApi.BLL.Utilities;
using CommerceApi.DAL.Entities;
using CommerceApi.DTO.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace CommerceApi.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        
        private Expression<Func<Product, object>>[] includes = { e => e.ProductReviews };

        public ProductController(IProductService service)
        {
            _service = service;
        }

        public async Task<IActionResult> GetProductsAsync()
        {
            try
            {
                return Ok(await _service.GetEntitiesAsync<ICollection<ProductDto>>());
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
                return Ok(await _service.GetEntityAsync<ProductDto>(e => e.UID == id, includes));
            }
            catch (NotFoundException)
            {
                return NotFound($"Product with the ProductId = {id} was not found");
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

            if (id != update.UID)
                return BadRequest("Id does not match request body");

            try
            {
                return Ok(await _service.UpdateProductAsync(id, update));
            }
            catch (NotFoundException)
            {
                return NotFound($"Product with the ProductId = {id} was not found");
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
                await _service.DeleteEntityAsync(e => e.UID == id);
                return Ok($"Product with the ProductId = {id} was successfully deleted");
            }
            catch (NotFoundException)
            {
                return NotFound($"Product with the ProductId = {id} was not found");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

    }
}
