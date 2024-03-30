using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class CategoryController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}