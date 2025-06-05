using ForumApp.Services.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ForumApp.Web.Controllers;

public class PostController : Controller
{
    private readonly IPostService postService;

    public PostController(IPostService postService)
    {
        this.postService = postService;
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var model = await postService.GetPostDetailsAsync(id);

        if (model == null)
        {
            return NotFound();
        }

        return View(model);
    }
}
