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
    public async Task<IActionResult> Create( ReplyCreateInputModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool createResult = await replyService
                .CreateReplyForPost((Guid)GetUserId()!, model);

            if (!createResult)
            {
                ModelState.AddModelError(String.Empty,"Fatal error while adding reply to post");
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
}
