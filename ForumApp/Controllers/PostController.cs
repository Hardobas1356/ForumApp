using ForumApp.Data.Models;
using ForumApp.Services.Core;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;

namespace ForumApp.Web.Controllers;

public class PostController : BaseController
{
    private readonly IPostService postService;

    public PostController(IPostService postService)
    {
        this.postService = postService;
    }


    [HttpGet]
    [AllowAnonymous]
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
        try
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
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return RedirectToAction(nameof(Details), "Board");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Create(Guid id)
    {
        try
        {
            PostCreateInputModel model = new PostCreateInputModel()
            {
                BoardId = id,
            };

            return View(model);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return RedirectToAction(nameof(Details), "Board", new { Id = id });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(PostCreateInputModel model)
    {
        try
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            bool CreateResult =
                await postService.AddPostAsync(model);

            if (!CreateResult)
            {
                ModelState.AddModelError(String.Empty, "Error occured while adding post to board");
                return View(model);
            }

            return RedirectToAction(nameof(Details), "Board", new { Id = model.BoardId });
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return RedirectToAction(nameof(Details), "Board", new { Id = model.BoardId });
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            PostDeleteViewModel? model = await postService
                .GetPostForDeleteAsync(id);
            if (model == null)
            {
                return RedirectToAction("Index", "Board");
            }

            return View(model);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return RedirectToAction("Index", "Board");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(PostDeleteViewModel model)
    {
        try
        {
            bool postDeletionResult = await postService
                .DeletePostAsync(model);

            if (!postDeletionResult)
            {
                return RedirectToAction("Details", "Board", new { Id = model.BoardId });
            }

            return RedirectToAction("Index", "Board");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return RedirectToAction("Index", "Board");
        }
    }
}
