using ForumApp.Data.Models;
using ForumApp.Services.Core;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Reply;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Linq.Expressions;

namespace ForumApp.Services.Tests;

[TestFixture]
public class ReplyServiceTests
{
    private Mock<IGenericRepository<Reply>> replyRepositoryMock;
    private Mock<IGenericRepository<Post>> postRepositoryMock;
    private Mock<UserManager<ApplicationUser>> userManagerMock;
    private ReplyService replyService;

    private Guid userId = Guid.NewGuid();
    private Guid postId = Guid.NewGuid();
    private ApplicationUser user;
    private Post post;

    [SetUp]
    public void SetUp()
    {
        replyRepositoryMock = new Mock<IGenericRepository<Reply>>();
        postRepositoryMock = new Mock<IGenericRepository<Post>>();
        userManagerMock = new Mock<UserManager<ApplicationUser>>(
            Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null
        );

        user = new ApplicationUser { Id = userId, DisplayName = "TestUser" };
        post = new Post { Id = postId };

        replyService = new ReplyService(replyRepositoryMock.Object, postRepositoryMock.Object, userManagerMock.Object);
    }

    //CreateReplyForPost
    [Test]
    public async Task CreateReplyForPostAsyncShouldCreateReplyWhenValid()
    {
        ReplyCreateInputModel inputModel = new ReplyCreateInputModel
        {
            PostId = postId,
            Content = "Test reply"
        };

        postRepositoryMock
            .Setup(p => p.GetByIdAsync(postId, It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(post);
        userManagerMock
            .Setup(u => u.FindByIdAsync(userId.ToString()))
            .ReturnsAsync(user);

        await replyService.CreateReplyForPostAsync(userId, inputModel);

        replyRepositoryMock.Verify(r => r.AddAsync(It.Is<Reply>(r =>
            r.PostId == postId &&
            r.ApplicationUserId == userId &&
            r.Content == "Test reply")), Times.Once);

        replyRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }
    [Test]
    public void CreateReplyForPostAsyncThrowsWhenPostNotFound()
    {
        postRepositoryMock.Setup(p => p.GetByIdAsync(postId, true, false))
            .ReturnsAsync((Post?)null);

        ReplyCreateInputModel model = new ReplyCreateInputModel { PostId = postId, Content = "Text" };

        Assert.ThrowsAsync<ArgumentException>(() =>
            replyService.CreateReplyForPostAsync(userId, model));
    }
    [Test]
    public void CreateReplyForPostAsyncThrowsWhenUserNotFound()
    {
        postRepositoryMock.Setup(p => p.GetByIdAsync(postId, true, false)).ReturnsAsync(post);
        userManagerMock.Setup(u => u.FindByIdAsync(userId.ToString())).ReturnsAsync((ApplicationUser?)null);

        ReplyCreateInputModel model = new ReplyCreateInputModel { PostId = postId, Content = "Text" };

        Assert.ThrowsAsync<ArgumentException>(() =>
            replyService.CreateReplyForPostAsync(userId, model));
    }


    //SoftDeleteReply
    [Test]
    public void SoftDeleteReplyAsyncThrowsWhenUserHasNoRights()
    {
        Reply reply = new Reply
        {
            Id = Guid.NewGuid(),
            PostId = postId,
            ApplicationUserId = Guid.NewGuid(),
            Post = new Post
            {
                Board = new Board
                {
                    BoardManagers = new List<BoardManager>()
                }
            }
        };

        replyRepositoryMock.Setup(r =>
            r.SingleOrDefaultWithIncludeAsync(
                It.IsAny<Expression<Func<Reply, bool>>>(),
                It.IsAny<Func<IQueryable<Reply>, IQueryable<Reply>>>(),
                It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(reply);

        userManagerMock.Setup(u => u.FindByIdAsync(userId.ToString())).ReturnsAsync(user);
        userManagerMock.Setup(u => u.IsInRoleAsync(user, It.IsAny<string>())).ReturnsAsync(false);

        Assert.ThrowsAsync<OperationCanceledException>(() =>
            replyService.SoftDeleteReplyAsync(userId, new ReplyDeleteViewModel { Id = reply.Id, PostId = postId }));
    }

    //EditReplyAsync
    [Test]
    public async Task EditReplyAsyncUpdatesContentWhenValid()
    {
        Reply reply = new Reply
        {
            Id = Guid.NewGuid(),
            PostId = postId,
            ApplicationUserId = userId,
            Content = "Old content"
        };

        replyRepositoryMock.Setup(r =>
            r.SingleOrDefaultAsync(
                It.IsAny<Expression<Func<Reply, bool>>>(),
                It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(reply);

        ReplyEditInputModel model = new ReplyEditInputModel
        {
            Id = reply.Id,
            PostId = postId,
            Content = "Updated content"
        };

        await replyService.EditReplyAsync(userId, model);

        Assert.That(reply.Content, Is.EqualTo("Updated content"));
        replyRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }
    [Test]
    public void EditReplyAsyncThrowsWhenReplyNotFound()
    {
        replyRepositoryMock.Setup(r =>
            r.SingleOrDefaultAsync(It.IsAny<Expression<Func<Reply, bool>>>(),
                                   It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync((Reply?)null);

        ReplyEditInputModel model = new ReplyEditInputModel
        {
            Id = Guid.NewGuid(),
            PostId = postId,
            Content = "Edited content"
        };

        Assert.ThrowsAsync<ArgumentException>(() =>
            replyService.EditReplyAsync(userId, model));
    }


    //GetReplyForEdit
    [Test]
    public void GetReplyForEditAsyncThrowsWhenReplyNotFound()
    {
        replyRepositoryMock.Setup(r =>
            r.SingleOrDefaultAsync(
                It.IsAny<Expression<Func<Reply, bool>>>(),
                It.IsAny<bool>(), It.IsAny<bool>()))
             .ReturnsAsync((Reply?)null);

        Assert.ThrowsAsync<ArgumentException>(() =>
            replyService.GetReplyForEditAsync(userId, postId, Guid.NewGuid()));
    }

    //GetReplyForDelete
    [Test]
    public async Task GetReplyForDeleteAsyncReturnsViewModelWhenValid()
    {
        Guid replyId = Guid.NewGuid();
        Board board = new Board
        {
            BoardManagers = new List<BoardManager>
            {
                new BoardManager { ApplicationUserId = userId }
            }
        };
        Post post = new Post { Id = postId, Board = board };
        Reply reply = new Reply
        {
            Id = replyId,
            PostId = postId,
            ApplicationUserId = Guid.NewGuid(),
            Content = "Some content",
            CreatedAt = DateTime.UtcNow,
            Post = post
        };

        replyRepositoryMock.Setup(r =>
            r.SingleOrDefaultWithIncludeAsync(
                It.IsAny<Expression<Func<Reply, bool>>>(),
                It.IsAny<Func<IQueryable<Reply>, IQueryable<Reply>>>(),
                It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(reply);

        ReplyDeleteViewModel result = await replyService
            .GetReplyForDeleteAsync(userId, postId, replyId);

        Assert.That(result.Id, Is.EqualTo(replyId));
        Assert.That(result.Content, Is.EqualTo(reply.Content));
        Assert.That(result.PostId, Is.EqualTo(postId));
    }
    [Test]
    public void GetReplyForDeleteAsyncThrowsWhenReplyNotFound()
    {
        replyRepositoryMock.Setup(r =>
            r.SingleOrDefaultWithIncludeAsync(
                It.IsAny<Expression<Func<Reply, bool>>>(),
                It.IsAny<Func<IQueryable<Reply>, IQueryable<Reply>>>(),
                It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync((Reply?)null);

        Assert.ThrowsAsync<ArgumentException>(() =>
            replyService.GetReplyForDeleteAsync(userId, postId, Guid.NewGuid()));
    }
    [Test]
    public void GetReplyForDeleteAsyncThrowsWhenUserHasNoRights()
    {
        Reply reply = new Reply
        {
            Id = Guid.NewGuid(),
            PostId = postId,
            ApplicationUserId = Guid.NewGuid(),
            Post = new Post
            {
                Board = new Board
                {
                    BoardManagers = new List<BoardManager>()
                }
            }
        };

        replyRepositoryMock.Setup(r =>
            r.SingleOrDefaultWithIncludeAsync(
                It.IsAny<Expression<Func<Reply, bool>>>(),
                It.IsAny<Func<IQueryable<Reply>, IQueryable<Reply>>>(),
                It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(reply);

        userManagerMock.Setup(u => u.FindByIdAsync(userId.ToString())).ReturnsAsync(user);
        userManagerMock.Setup(u => u.IsInRoleAsync(user, It.IsAny<string>())).ReturnsAsync(false);

        Assert.ThrowsAsync<OperationCanceledException>(() =>
            replyService.GetReplyForDeleteAsync(userId, postId, reply.Id));
    }
}
