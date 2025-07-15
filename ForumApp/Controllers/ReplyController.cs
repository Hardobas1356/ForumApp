using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Reply;
using Microsoft.AspNetCore.Mvc;

namespace ForumApp.Web.Controllers;

public class ReplyController : BaseController
{
    private readonly IReplyService replyService;
    private readonly IPermissionService permissionService;
    private readonly ILogger<ReplyController> logger;

    public ReplyController(IReplyService replyService, IPermissionService permissionService,
        ILogger<ReplyController> logger)
    {
        this.replyService = replyService;
        this.permissionService = permissionService;
        this.logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Create(Guid postId)
    {
        try
        {
            //TODO: check if Post exists
            ReplyCreateInputModel model = new ReplyCreateInputModel()
            {
                PostId = postId,
            };

            return View(model);
        }
        catch (Exception e)
        {
            logger.LogError(e,"Error while loading create form");
            return RedirectToAction("Details", "Post", new { id = postId });
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ReplyCreateInputModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid reply model submitted by user {UserId} for post {PostId}", this.GetUserId(), model.PostId);
                return View(model);
            }

            bool createResult = await replyService
                .CreateReplyForPostAsync((Guid)GetUserId()!, model);

            if (!createResult)
            {
                logger.LogWarning("Failed to create reply for post {PostId} by user {UserId}", model.PostId, this.GetUserId());
                ModelState.AddModelError(String.Empty, "Fatal error while creating reply");
                return NotFound();
            }

            return RedirectToAction("Details", "Post", new { id = model.PostId });
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error occurred while creating reply for post {PostId}", model.PostId);
            return RedirectToAction("Details", "Post", new { id = model.PostId });
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid postId, Guid id)
    {
        try
        {
            if (!await permissionService.CanManageReplyAsync((Guid)this.GetUserId()!, id))
            {
                logger.LogWarning("User {UserId} attempted to delete unauthorized reply {ReplyId}", this.GetUserId(), id);
                return RedirectToAction("Details", "Post", new { id = postId });
            }

            ReplyDeleteViewModel? model = await replyService
                .GetReplyForDeleteAsync((Guid)this.GetUserId()!, postId, id);

            if (model == null)
            {
                logger.LogWarning("Reply {ReplyId} not found for deletion", id);
                return RedirectToAction("Details", "Post", new { id = postId });
            }

            return View(model);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error loading delete view for reply {ReplyId}", id);
            return RedirectToAction("Details", "Post", new { id = postId });
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(ReplyDeleteViewModel model)
    {
        try
        {
            if (!await permissionService.CanManageReplyAsync((Guid)this.GetUserId()!, model.Id))
            {
                logger.LogWarning("User {UserId} not authorized to delete reply {ReplyId}", this.GetUserId(), model.Id);
                return RedirectToAction("Details", "Post", new { id = model.PostId });
            }

            bool deleteResult = await replyService.SoftDeleteReplyAsync((Guid)this.GetUserId()!, model);
            if (!deleteResult)
            {
                logger.LogWarning("Failed to delete reply {ReplyId}", model.Id);
                ModelState.AddModelError(string.Empty, "Error while deleting reply!");
            }
            else
            {
                logger.LogInformation("Reply {ReplyId} deleted by user {UserId}", model.Id, this.GetUserId());
            }

            return RedirectToAction("Details", "Post", new { id = model.PostId });
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error deleting reply {ReplyId}", model.Id);
            return RedirectToAction("Details", "Post", new { id = model.PostId });
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid postId, Guid id)
    {
        try
        {
            if (!await permissionService.CanManageReplyAsync((Guid)this.GetUserId()!, id))
            {
                logger.LogWarning("User {UserId} unauthorized to edit reply {ReplyId}", this.GetUserId(), id);
                return RedirectToAction("Details", "Post", new { id = postId });
            }

            ReplyEditInputModel? model = await replyService
                .GetReplyForEditAsync((Guid)this.GetUserId()!, postId, id);

            if (model == null)
            {
                logger.LogWarning("Reply {ReplyId} not found for editing", id);
                return RedirectToAction("Details", "Post", new { id = postId });
            }

            return View(model);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error loading edit form for reply {ReplyId}", id);
            return RedirectToAction("Details", "Post", new { id = postId });
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ReplyEditInputModel model)
    {
        try
        {
            if (!await permissionService.CanManageReplyAsync((Guid)this.GetUserId()!, model.Id))
            {
                logger.LogWarning("User {UserId} unauthorized to edit reply {ReplyId}", this.GetUserId(), model.Id);
                return RedirectToAction("Details", "Post", new { id = model.PostId });
            }

            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid reply edit model submitted by user {UserId}", this.GetUserId());
                ModelState.AddModelError(String.Empty, "Error while editing reply");
                return View(model);
            }

            bool editResult = await replyService
                .EditReplyAsync((Guid)this.GetUserId()!,model);

            if (!editResult)
            {
                ModelState.AddModelError(String.Empty, "Error while editing reply");
                return View(model);
            }

            return RedirectToAction("Details", "Post", new { id = model.PostId });
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return RedirectToAction("Details", "Post", new { id = model.PostId });
        }
    }
}
