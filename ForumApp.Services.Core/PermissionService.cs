using ForumApp.Data.Models;
using ForumApp.Services.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using static ForumApp.GCommon.GlobalConstants.Roles;

namespace ForumApp.Services.Core;

public class PermissionService : IPermissionService
{
    private readonly IGenericRepository<Post> postRepository;
    private readonly IGenericRepository<Reply> replyRepository;
    private readonly IGenericRepository<BoardManager> managerRepository;
    private readonly UserManager<ApplicationUser> userManager;

    public PermissionService(IGenericRepository<Post> postRepository,
                             IGenericRepository<Reply> replyRepository,
                             IGenericRepository<BoardManager> managerRepository,
                             UserManager<ApplicationUser> userManager)
    {
        this.postRepository = postRepository;
        this.replyRepository = replyRepository;
        this.managerRepository = managerRepository;
        this.userManager = userManager;
    }

    public async Task<bool> CanManagePostAsync(Guid userId, Guid postId)
    {
        if (await IsAdminAsync(userId))
            return true;

        Post? post = await postRepository
            .SingleOrDefaultWithIncludeAsync(p => p.Id == postId,
                                             q => q.Include(p => p.Board),
                                             asNoTracking: true);
        if (post == null)
        {
            return false;
        }

        bool isManager = await managerRepository
            .AnyAsync(m => m.ApplicationUserId == userId
                           && m.BoardId == post.BoardId);
        return isManager;
    }

    public async Task<bool> CanManageReplyAsync(Guid userId, Guid replyId)
    {
        if (await IsAdminAsync(userId))
            return true;

        Reply? reply = await replyRepository
             .SingleOrDefaultWithIncludeAsync(r => r.Id == replyId,
                                              q => q.Include(r => r.Post),
                                              asNoTracking: true);

        if (reply == null)
            return false;

        if (reply.ApplicationUserId == userId)
            return true;

        bool isManager = await managerRepository.AnyAsync(m => m.BoardId == reply.Post.BoardId
                                                               && m.ApplicationUserId == userId);

        return isManager;
    }

    private async Task<bool> IsAdminAsync(Guid userId)
    {
        ApplicationUser? user = await userManager.FindByIdAsync(userId.ToString());

        return user != null && await userManager.IsInRoleAsync(user, ADMIN_ROLE_NAME);
    }
}
