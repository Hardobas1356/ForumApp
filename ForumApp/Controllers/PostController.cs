using ForumApp.Services.Core;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Post;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var model = await postService.GetPostForEditAsync(id);

        if (model == null)
        {
            return NotFound();
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditPostViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (!await postService.EditPostAsync(model))
        {
            return NotFound();
        }

        return RedirectToAction("Details", new { Id = model.Id });
    }
}
