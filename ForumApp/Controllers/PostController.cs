using ForumApp.Data.Models;
using ForumApp.Services.Core;
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
    private readonly IPermissionService permissionService;
    private readonly IBoardService boardService;

    public PostController(IPostService postService, ITagService tagService,
        IPermissionService permissionService, IBoardService boardService)
    {
        this.postService = postService;
        this.tagService = tagService;
        this.permissionService = permissionService;
        this.boardService = boardService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Details(Guid id)
    {
        try
        {
            PostDetailsViewModel? model = await postService
                .GetPostDetailsAsync(this.GetUserId(), id);

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

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        try
        {
            var model = await postService
                .GetPostForEditAsync((Guid)this.GetUserId()!, id);

            if (!await permissionService.CanManagePostAsync((Guid)this.GetUserId()!, id))
            {
                return RedirectToAction("Index", "Board");
            }

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
            if (!await permissionService.CanManagePostAsync((Guid)this.GetUserId()!, model.Id))
            {
                return RedirectToAction("Index", "Board");
            }

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
            string? boardName = await boardService.GetBoardNameByIdAsync(id);

            ICollection<TagViewModel> tags = await tagService
                .GetTagsAsync();

            PostCreateInputModel model = new PostCreateInputModel()
            {
                BoardId = id,
                AvailableTags = tags,
                BoardName = boardName,
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
                await postService.AddPostAsync((Guid)this.GetUserId()!, model);

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
            if (!await permissionService.CanManagePostAsync((Guid)this.GetUserId()!, id))
            {
                return RedirectToAction("Index", "Board");
            }

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
            if (!await permissionService.CanManagePostAsync((Guid)this.GetUserId()!, model.Id))
            {
                return RedirectToAction("Index", "Board");
            }

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
