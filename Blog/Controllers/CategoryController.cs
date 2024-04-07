using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers;

[ApiController]
public class CategoryController : ControllerBase
{
    [HttpGet("v1/categories")]
    public async Task<IActionResult> GetAsync([FromServices] BlogDataContext context)
    {
        try
        {
            var categories = await context.Categories.ToListAsync();
            return Ok(categories);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("05EX01 Falha interna na aplicação.");
        }
    }

    [HttpGet("v1/categories/{id:int}")]
    public async Task<IActionResult> GetAsyncById([FromRoute] int id, [FromServices] BlogDataContext context)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(p => p.Id == id);

            if (category == null) return NotFound("Categoria não existe.");

            return Ok(category);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("05EX02 Falha interna na aplicação.");
        }
    }


    [HttpPost("v1/categories")]
    public async Task<IActionResult> PostAsync([FromBody] Category model, [FromServices] BlogDataContext context)
    {
        try
        {
            await context.Categories.AddAsync(model);
            await context.SaveChangesAsync();

            return Created($"v1/categories/{model.Id}", model);
        }
        catch (Exception e)
        {
            Console.WriteLine(e); // Atenção para vazar muitos detalhes em 500.

            return StatusCode(500, "05EX03 Falha interna na aplicação.");
        }
    }

    [HttpPut("v1/categories/{id:int}")]
    public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] Category model,
        [FromServices] BlogDataContext context)
    {
        try
        {
            var identify = await context.Categories.FirstOrDefaultAsync(p => p.Id == id);
            if (identify == null) return NotFound("Identificador não encontrado.");

            identify.Name = model.Name;
            identify.Slug = model.Slug;

            context.Categories.Update(identify);
            await context.SaveChangesAsync();

            return Ok(model);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "05EX04 Falha interna na aplicação.");
        }
    }

    [HttpDelete("v1/categories/{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] BlogDataContext context)
    {
        try
        {
            var identify = await context.Categories.FirstOrDefaultAsync(p => p.Id == id);

            if (identify == null) return BadRequest("Identificador não encontrado.");

            context.Categories.Remove(identify);
            await context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "05EX05 Falha interna na aplicação.");
        }
    }
}