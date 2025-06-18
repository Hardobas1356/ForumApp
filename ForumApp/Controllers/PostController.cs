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
    public async Task<IActionResult> Details(Guid id)
    {
        try
        {
            var model = await postService.GetPostDetailsAsync(id);

            if (model == null)
            {
                return RedirectToAction(nameof(Details), "Board");
            }

            return View(model);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return RedirectToAction(nameof(Details), "Board");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        try
        {
            var model = await postService.GetPostForEditAsync(id);

            if (model == null)
            {
                return RedirectToAction(nameof(Details), "Board");
            }

            return View(model);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return RedirectToAction(nameof(Details), "Board");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(PostEditInputModel model)
    {
        if (!this.ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Error while editing Post");
            return this.View(model);
        }

        if (!await postService.EditPostAsync(model))
        {
            return this.View(model);
        }

        return RedirectToAction("Details", new { Id = model.Id });
    }
}
