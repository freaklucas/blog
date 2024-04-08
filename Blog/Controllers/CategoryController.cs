using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.ViewModels;
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

            return Ok(new ResultViewModel<List<Category>>(categories));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<List<Category>>("05EX01 Falha interna na aplicação."));
        }
    }

    [HttpGet("v1/categories/{id:int}")]
    public async Task<IActionResult> GetAsyncById([FromRoute] int id, [FromServices] BlogDataContext context)
    {
        try
        {
            var category = await context
                .Categories
                .FirstOrDefaultAsync(p => p.Id == id);

            if (category == null) return NotFound(new ResultViewModel<Category>("Categoria não encontrada."));

            return Ok(new ResultViewModel<Category>(category));
        }
        catch
        {
            return BadRequest(new ResultViewModel<Category>("05EX02 Falha interna na aplicação."));
        }
    }


    [HttpPost("v1/categories")]
    public async Task<IActionResult> PostAsync(
        [FromBody] EditorCategoryViewModel model,
        [FromServices] BlogDataContext context)
    {
        if (!ModelState.IsValid) return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));

        try
        {
            var category = new Category
            {
                Id = 0,
                Name = model.Name,
                Slug = model.Slug.ToLower()
            };

            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            return Created($"v1/categories/{category.Id}", new ResultViewModel<Category>(category));
        }
        catch
        {
            // Atenção para vazar muitos detalhes em 500.

            return StatusCode(500, new ResultViewModel<Category>("05EX03 Falha interna na aplicação."));
        }
    }

    [HttpPut("v1/categories/{id:int}")]
    public async Task<IActionResult> PutAsync(
        [FromRoute] int id,
        [FromBody] EditorCategoryViewModel model,
        [FromServices] BlogDataContext context)
    {
        try
        {
            var identify = await context.Categories.FirstOrDefaultAsync(p => p.Id == id);
            if (identify == null) return NotFound(new ResultViewModel<Category>("Identificador não encontrado."));

            identify.Name = model.Name;
            identify.Slug = model.Slug;

            context.Categories.Update(identify);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Category>(identify));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Category>("05EX04 Falha interna na aplicação."));
        }
    }

    [HttpDelete("v1/categories/{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] BlogDataContext context)
    {
        try
        {
            var identify = await context.Categories.FirstOrDefaultAsync(p => p.Id == id);

            if (identify == null) return NotFound(new ResultViewModel<Category>("Identificador não encontrado."));

            context.Categories.Remove(identify);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Category>(identify));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Category>("05EX05 Falha interna na aplicação."));
        }
    }
}