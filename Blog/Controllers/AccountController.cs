using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.Services;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace Blog.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    private readonly TokenService _tokenService;

    public AccountController(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("/v1/accounts")]
    public async Task<IActionResult> CreateUser(
        [FromBody] RegisterViewModel model,
        [FromServices] BlogDataContext context)
    {
        if (!ModelState.IsValid) return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        var user = new User
        {
            Name = model.Name,
            Email = model.Email,
            Slug = model.Email.Replace("@", "-").Replace(".", "-")
        };

        var password = PasswordGenerator.Generate(25);
        user.PasswordHash = PasswordHasher.Hash(password);

        try
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<dynamic>(new
            {
                user = user.Email, password
            }));
        }

        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("/v1/accounts/login")]
    public async Task <IActionResult> Login(
        [FromBody] LoginViewModel? model,
        [FromServices] BlogDataContext context,
        [FromServices] TokenService tokenService)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
        }

        var user = await context
            .Users
            .AsNoTracking()
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(u => model != null && u.Email == model.Email);

        if (user == null)
        {
            return StatusCode(400, new ResultViewModel<string>("Usuário ou senha inválidos."));
        }

        if (!PasswordHasher.Verify(user.PasswordHash, model.Password))
        {
            return StatusCode(401, new ResultViewModel<string>("Usuário inválido."));
        }


        try
        {
            var token = tokenService.GenerateToken(user);

            return Ok(new ResultViewModel<string>(token, errors: null));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<string>("0XE00 - Falha interna na aplicação."));
        }
    }
}