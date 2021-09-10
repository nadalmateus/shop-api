namespace ShopAPI.Controllers
{
    using Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<List<User>>> Get([FromServices] DataContext context)
        {
            List<User> users = await context.Users.AsNoTracking().ToListAsync();
            return users;
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Post([FromServices] DataContext context, [FromBody] User model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                context.Users.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possivel criar o usuario" });
            }
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromServices] DataContext context, [FromBody] User model)
        {
            User user = await context.Users.AsNoTracking()
                .Where(x => x.Username == model.Username && x.Password == model.Password).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound(new { message = "Usuario ou senha invalidos" });
            }

            string token = TokenService.GenerateToken(user);
            return new { user, token };
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<User>> Put([FromServices] DataContext context, int id, [FromBody] User model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.Id)
            {
                return NotFound(new { message = "Usuario não encontrado" });
            }

            try
            {
                context.Entry(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return model;
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possivel criar o usuario" });
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<User>> Delete(int id, [FromServices] DataContext context)
        {
            Category user = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound(new { message = "Usuario não encontrada" });
            }

            try
            {
                context.Categories.Remove(user);
                await context.SaveChangesAsync();
                return Ok(new { message = "Usuario removido com sucesso" });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível remover o usuario" });
            }
        }
    }
}