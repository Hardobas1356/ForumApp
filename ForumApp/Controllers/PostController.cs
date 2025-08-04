using ForumApp.Services.Core;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Post;
using ForumApp.Web.ViewModels.Tag;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static ForumApp.GCommon.Enums.SortEnums.ReplySort;
using static ForumApp.GCommon.GlobalConstants.Pages;

namespace ForumApp.Web.Controllers;

public class PostController : BaseController
{
    private readonly IPostService postService;
    private readonly ITagService tagService;
    private readonly IPermissionService permissionService;
    private readonly IBoardService boardService;
    private readonly ILogger<PostController> logger;

    public PostController(IPostService postService, ITagService tagService,
        IPermissionService permissionService, IBoardService boardService,
        ILogger<PostController> logger)
    {
        this.postService = postService;
        this.tagService = tagService;
        this.permissionService = permissionService;
        this.boardService = boardService;
        this.logger = logger;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Details(Guid id, ReplySortBy sortBy, int pageNumber = 1)
    {
        try
        {
            pageNumber = Math.Max(1, pageNumber);

            PostDetailsViewModel model = await postService
                .GetPostDetailsAsync(this.GetUserId(), id, sortBy, pageNumber, REPLY_PAGE_SIZE);

            ViewBag.ReplySortBy = sortBy;

            return View(model);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error occured while getting details for post");
            return RedirectToAction("Index", "Board");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        try
        {
            Guid userId = (Guid)this.GetUserId()!;

            if (!await permissionService.IsOwnerOfPost(userId, id))
            {
                logger.LogError("User does not have rights to edit post");
                return RedirectToAction(nameof(Details), new { Id = id });
            }

            PostEditInputModel model = await postService
                .GetPostForEditAsync((Guid)this.GetUserId()!, id);

            return View(model);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error occured while detting edit form for post");
            return RedirectToAction("Index", "Board");
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(PostEditInputModel model)
    {
        try
        {
            Guid userId = (Guid)this.GetUserId()!;

            if (!await permissionService.IsOwnerOfPost(userId, model.Id))
            {
                logger.LogError("User does not have rights to edit post");
                return RedirectToAction(nameof(Details), new { Id = model.Id });
            }

            if (!this.ModelState.IsValid)
            {
                logger.LogWarning("Model submitted is invalid");
                model.AvailableTags = await tagService
                    .GetTagsAsync();
                ModelState.AddModelError(string.Empty, "Error while editing Post");
                return this.View(model);
            }

            await postService.EditPostAsync(userId, model);

            return RedirectToAction("Details", new { Id = model.Id });
        }
        catch (ArgumentException ex) when (ex.ParamName == model.ImageUrl)
        {
            logger.LogWarning(ex, "Invalid image URL: {ImageUrl}", model.ImageUrl);

            ModelState.AddModelError(nameof(model.ImageUrl), ex.Message);
            model.AvailableTags = await tagService.GetTagsAsync();

            return View(model);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while editing post");

            model.AvailableTags = await tagService
                .GetTagsAsync();
            ModelState.AddModelError(string.Empty, "Unexpected error occurred while editing the post.");
            return this.View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Create(Guid id)
    {
        try
        {
            string boardName = await boardService.GetBoardNameByIdAsync(id);

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
            logger.LogError(e, "Error occured while getting create form for post");
            return RedirectToAction(nameof(Details), "Board", new { Id = id });
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PostCreateInputModel model)
    {
        try
        {
            if (!this.ModelState.IsValid)
            {
                logger.LogWarning("Model posted was invalid");
                model.AvailableTags = await tagService
                    .GetTagsAsync();
                return View(model);
            }

            await postService.AddPostAsync((Guid)this.GetUserId()!, model);

            return RedirectToAction(nameof(Details), "Board", new { Id = model.BoardId });
        }
        catch (ArgumentException ex) when (ex.ParamName == model.ImageUrl)
        {
            logger.LogWarning(ex, "Invalid image URL: {ImageUrl}", model.ImageUrl);

            ModelState.AddModelError(nameof(model.ImageUrl), ex.Message);
            model.AvailableTags = await tagService.GetTagsAsync();

            return View(model);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error occured while creating post");
            ModelState.AddModelError(String.Empty, "Error occured while adding post to board");

            model.AvailableTags = await tagService
                .GetTagsAsync();
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            Guid userId = (Guid)this.GetUserId()!;

            if (!await permissionService.IsOwnerOfPost(userId, id)
                && !await permissionService.CanManagePostAsync(userId, id))
            {
                logger.LogWarning("User does not rights to delete");
                return RedirectToAction("Index", "Board");
            }

            PostDeleteViewModel? model = await postService
                .GetPostForDeleteAsync(userId, id);

            return View(model);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error occured while getting delete form for post");
            return RedirectToAction("Index", "Board");
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(PostDeleteViewModel model)
    {
        try
        {
            Guid userId = (Guid)this.GetUserId()!;

            if (!await permissionService.IsOwnerOfPost(userId, model.Id)
                && !await permissionService.CanManagePostAsync(userId, model.Id))
            {
                logger.LogWarning("User does not rights to delete");
                return RedirectToAction("Index", "Board");
            }

            await postService
                .DeletePostAsync(userId, model);

            return RedirectToAction("Index", "Board");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error occured while deleting post");
            return RedirectToAction("Details", "Board", new { Id = model.BoardId });
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Pin(Guid id)
    {
        try
        {
            await postService.PinPostAsync((Guid)this.GetUserId()!, id);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error occured while pinning post");
        }

        return RedirectToAction(nameof(Details), new { Id = id });
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Unpin(Guid id)
    {
        try
        {
            await postService.UnpinPostAsync((Guid)this.GetUserId()!, id);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error occured while unpinning post");
        }

        return RedirectToAction(nameof(Details), new { Id = id });
    }
}
