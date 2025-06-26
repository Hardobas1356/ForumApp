using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Post;
using ForumApp.Web.ViewModels.Tag;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForumApp.Web.Controllers;

public class PostController : BaseController
{
    private readonly IPostService postService;
    private readonly ITagService tagService;

    public PostController(IPostService postService, ITagService tagService)
    {
        this.postService = postService;
        this.tagService = tagService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Details(Guid id)
    {
        try
        {
            var model = await postService.GetPostDetailsAsync(this.GetUserId(),id);

            if (model == null)
            {
                return RedirectToAction("Index", "Board");
            }

            return View(model);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return RedirectToAction("Index" , "Board");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        try
        {
            var model = await postService
                .GetPostForEditAsync((Guid)this.GetUserId()!, id);

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
    public async Task<IActionResult> Edit(PostEditInputModel model)
    {
        try
        {
            if (!this.ModelState.IsValid)
            {
                model.AvailableTags = await tagService
                    .GetTagsAsync();
                ModelState.AddModelError(string.Empty, "Error while editing Post");
                return this.View(model);
            }

            if (!await postService.EditPostAsync((Guid)this.GetUserId()!, model))
            {
                model.AvailableTags = await tagService
                    .GetTagsAsync();
                return this.View(model);
            }

            return RedirectToAction("Details", new { Id = model.Id });
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return RedirectToAction("Index", "Board");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Create(Guid id)
    {
        try
        {
            ICollection<TagViewModel> tags = await tagService
                .GetTagsAsync();

            PostCreateInputModel model = new PostCreateInputModel()
            {
                BoardId = id,
                AvailableTags = tags
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
                model.AvailableTags = await tagService
                    .GetTagsAsync();
                return View(model);
            }

            bool CreateResult =
                await postService.AddPostAsync((Guid)this.GetUserId()!,model);

            if (!CreateResult)
            {
                model.AvailableTags = await tagService
                    .GetTagsAsync();
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
                .GetPostForDeleteAsync((Guid)this.GetUserId()!, id);
            
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
                .DeletePostAsync((Guid)this.GetUserId()!, model);

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
