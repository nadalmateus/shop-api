namespace ShopAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ShopAPI.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [ApiController]
    [Route("Categories")]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Category>>> Get()
        {
            return new List<Category>();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<List<Category>>> GetById()
        {
            return new List<Category>();
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Category>> Post([FromBody] Category model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(model);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> Put(int id, [FromBody] Category model)
        {
            if (id != model.Id)
            {
                return NotFound(new { message = "Category not found" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Category>> Delete()
        {
            return Ok();
        }
    }
}