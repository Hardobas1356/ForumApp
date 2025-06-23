using ForumApp.Data.Models;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Post;
using ForumApp.Web.ViewModels.Reply;
using Microsoft.AspNetCore.Mvc;

namespace ForumApp.Web.Controllers;

public class ReplyController : BaseController
{
    private readonly IReplyService replyService;
    public ReplyController(IReplyService replyService)
    {
        this.replyService = replyService;
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
            Console.WriteLine(e.Message);
            return RedirectToAction("Details", "Post", new { id = postId });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(ReplyCreateInputModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool createResult = await replyService
                .CreateReplyForPostAsync((Guid)GetUserId()!, model);

            if (!createResult)
            {
                ModelState.AddModelError(String.Empty, "Fatal error while adding reply to post");
                return NotFound();
            }

            return RedirectToAction("Details", "Post", new { id = model.PostId });
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return RedirectToAction("Details", "Post", new { id = model.PostId });
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id, Guid postId)
    {
        try
        {
            ReplyDeleteViewModel? model = await replyService
                .GetReplyForDeleteAsync((Guid)this.GetUserId()!,postId,id);

            if (model == null)
            {
                return RedirectToAction("Details", "Post", new { id = postId });
            }

            return View(model);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return RedirectToAction("Details", "Post", new { id = postId });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(ReplyDeleteViewModel model)
    {
        try
        {
            bool deleteResult = await replyService.SoftDeleteReplyAsync((Guid)this.GetUserId()!,model);
            if (!deleteResult)
            {
                ModelState.AddModelError(string.Empty,"Error while deleting reply!");
                return RedirectToAction("Details", "Post", new { id = model.PostId });
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
