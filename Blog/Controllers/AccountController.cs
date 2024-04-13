using Blog.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    private readonly TokenService _tokenService;
    
    public AccountController(TokenService tokenService)
    {
        _tokenService = tokenService;
    }
    
    [HttpPost("/v1/login")]
    public IActionResult Login()
    {
        var token = _tokenService.GenerateToken(null);

        return Ok(token);
    }

    [HttpGet("/v1/teste")]
    public IActionResult Teste()
    {
        return Ok("teste.");
    }
}