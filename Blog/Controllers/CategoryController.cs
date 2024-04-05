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
        var categories = await context.Categories.ToListAsync();
        
        return Ok(categories);
    }
    
    [HttpGet("v1/categories/{id:int}")]
    public async Task<IActionResult> GetAsyncById([FromRoute]int id,[FromServices] BlogDataContext context)
    {
        var category = await context.Categories.FirstOrDefaultAsync(p => p.Id == id);

        if (category == null)
        {
            return NotFound("Categoria não existe.");
        }
        
        return Ok(category);
    }
    
    
    [HttpPost("v1/categories")]
    public async Task<IActionResult> PostAsync([FromBody] Category model, [FromServices] BlogDataContext context)
    {
        await context.Categories.AddAsync(model);
        await context.SaveChangesAsync();
        
        return Created($"v1/categories/{model.Id}", model);
    }
    
    [HttpPut("v1/categories/{id:int}")]
    public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] Category model, [FromServices] BlogDataContext context)
    {
        var identify = await context.Categories.FirstOrDefaultAsync(p => p.Id == id);
        if (identify == null)
        {
            return NotFound("Identificador não encontrado.");
        }
        
        identify.Name = model.Name;
        identify.Slug = model.Slug;

        context.Categories.Update(identify);
        await context.SaveChangesAsync();
        
        return Ok(model);
    }

    [HttpDelete("v1/categories/{id:int}")]

    public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] BlogDataContext context)
    {
        var identify = await context.Categories.FirstOrDefaultAsync(p => p.Id == id);

        if (identify == null)
        {
            return BadRequest("Identificador não encontrado.");
        }

        context.Categories.Remove(identify);
        await context.SaveChangesAsync();
        
        return NoContent();
    }
}