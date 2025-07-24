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

            PostDetailsViewModel? model = await postService
                .GetPostDetailsAsync(this.GetUserId(), id, sortBy, pageNumber, REPLY_PAGE_SIZE);

            if (model == null)
            {
                logger.LogWarning("Post with id {postId} not found", id);
                return RedirectToAction("Index", "Board");
            }

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
            var model = await postService
                .GetPostForEditAsync((Guid)this.GetUserId()!, id);

            if (!await permissionService.CanManagePostAsync((Guid)this.GetUserId()!, id))
            {
                logger.LogWarning("User does not permission to edit");
                return RedirectToAction("Index", "Board");
            }

            if (model == null)
            {
                logger.LogWarning("Post not found");
                return RedirectToAction("Index", "Board");
            }

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
            if (!await permissionService.CanManagePostAsync((Guid)this.GetUserId()!, model.Id))
            {
                logger.LogWarning("User does not permission to edit");
                return RedirectToAction("Index", "Board");
            }

            if (!this.ModelState.IsValid)
            {
                logger.LogWarning("Model submitted is invalid");
                model.AvailableTags = await tagService
                    .GetTagsAsync();
                ModelState.AddModelError(string.Empty, "Error while editing Post");
                return this.View(model);
            }

            if (!await postService.EditPostAsync((Guid)this.GetUserId()!, model))
            {
                logger.LogWarning("Edit post failed");
                model.AvailableTags = await tagService
                    .GetTagsAsync();
                return this.View(model);
            }

            return RedirectToAction("Details", new { Id = model.Id });
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while editing post");
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

            bool CreateResult =
                await postService.AddPostAsync((Guid)this.GetUserId()!, model);

            if (!CreateResult)
            {
                model.AvailableTags = await tagService
                    .GetTagsAsync();
                logger.LogWarning("Post could not be created");
                ModelState.AddModelError(String.Empty, "Error occured while adding post to board");
                return View(model);
            }

            return RedirectToAction(nameof(Details), "Board", new { Id = model.BoardId });
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error occured while creating post");
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
                logger.LogWarning("User does not rights to delete");
                return RedirectToAction("Index", "Board");
            }

            PostDeleteViewModel? model = await postService
                .GetPostForDeleteAsync((Guid)this.GetUserId()!, id);

            if (model == null)
            {
                logger.LogWarning("Post with id {postId} not found", id);
                return RedirectToAction("Index", "Board");
            }

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
            if (!await permissionService.CanManagePostAsync((Guid)this.GetUserId()!, model.Id))
            {
                logger.LogWarning("User does not rights to delete");
                return RedirectToAction("Index", "Board");
            }

            bool postDeletionResult = await postService
                .DeletePostAsync((Guid)this.GetUserId()!, model);

            if (!postDeletionResult)
            {
                logger.LogWarning("Post could not be deleted");
                return RedirectToAction("Details", "Board", new { Id = model.BoardId });
            }

            return RedirectToAction("Index", "Board");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error occured while deleting post");
            return RedirectToAction("Index", "Board");
        }
    }
}
