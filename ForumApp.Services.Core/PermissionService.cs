using ForumApp.Data.Models;
using ForumApp.Services.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ForumApp.Services.Core;

public class PermissionService : IPermissionService
{
    private readonly IGenericRepository<Post> postRepository;
    private readonly IGenericRepository<Reply> replyRepository;
    private readonly IGenericRepository<BoardManager> managerRepository;

    public PermissionService(IGenericRepository<Post> postRepository,
                             IGenericRepository<Reply> replyRepository,
                             IGenericRepository<BoardManager> managerRepository)
    {
        this.postRepository = postRepository;
        this.replyRepository = replyRepository;
        this.managerRepository = managerRepository;
    }

    public async Task<bool> CanManagePostAsync(Guid userId, Guid postId)
    {
        Post? post = await postRepository
            .SingleOrDefaultWithIncludeAsync(p => p.Id == postId,
                                             q => q.Include(p => p.Board),
                                             asNoTracking: true);
        if (post == null)
        {
            return false;
        }

        if (post.ApplicationUserId == userId)
        {
            return true;
        }

        bool isManager = await managerRepository
            .AnyAsync(m => m.ApplicationUserId == userId
                           && m.BoardId == post.BoardId);
        return isManager;
    }

    public async Task<bool> CanManageReplyAsync(Guid userId, Guid replyId)
    {
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
}
