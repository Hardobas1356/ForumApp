using ForumApp.Data.Models;
using ForumApp.GCommon;
using ForumApp.Services.Core;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Post;
using ForumApp.Web.ViewModels.Reply;
using MockQueryable;
using Moq;
using System.Linq.Expressions;

using static ForumApp.GCommon.Enums.SortEnums;

namespace ForumApp.Services.Tests;

[TestFixture]
public class PostServiceTests
{
    private Mock<IGenericRepository<Post>> postRepositoryMock;
    private Mock<IGenericRepository<Board>> boardRepositoryMock;
    private Mock<IGenericRepository<PostTag>> postTagRepositoryMock;
    private Mock<IReplyService> replyServiceMock;
    private Mock<ITagService> tagServiceMock;
    private Mock<IPermissionService> permissionServiceMock;

    [SetUp]
    public void SetUp()
    {
        postRepositoryMock = new Mock<IGenericRepository<Post>>();
        boardRepositoryMock = new Mock<IGenericRepository<Board>>();
        postTagRepositoryMock = new Mock<IGenericRepository<PostTag>>();
        replyServiceMock = new Mock<IReplyService>();
        tagServiceMock = new Mock<ITagService>();
        permissionServiceMock = new Mock<IPermissionService>();
    }

    [Test]
    public void PassAlways()
    {
        Assert.Pass();
    }

    //PinPostAsync
    [Test]
    public void PinPostAsyncThrowsExceptionWhenPostNotFound()
    {
        Post? post = null;

        postRepositoryMock
            .Setup(pr => pr.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(post);

        PostService service = new PostService(postRepositoryMock.Object,
            boardRepositoryMock.Object,
            postTagRepositoryMock.Object,
            replyServiceMock.Object,
            tagServiceMock.Object,
            permissionServiceMock.Object);

        ArgumentException ex = Assert
            .ThrowsAsync<ArgumentException>(() => service.PinPostAsync(Guid.NewGuid(), Guid.NewGuid()));

        Assert.That(ex.Message, Does.Contain("Id"));
    }
    [Test]
    public void PinPostAsyncThrowsExceptionWhenNoPermissions()
    {
        Post post = new Post();

        postRepositoryMock
            .Setup(pr => pr.SingleOrDefaultAsync(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(post);

        permissionServiceMock
            .Setup(ps => ps.CanManagePostAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(false);

        PostService service = new PostService(postRepositoryMock.Object,
            boardRepositoryMock.Object,
            postTagRepositoryMock.Object,
            replyServiceMock.Object,
            tagServiceMock.Object,
            permissionServiceMock.Object);

        UnauthorizedAccessException ex = Assert
            .ThrowsAsync<UnauthorizedAccessException>(() => service.PinPostAsync(Guid.NewGuid(), Guid.NewGuid()));

        Assert.That(ex.Message, Does.Contain("User does not have rigths to pin post"));
    }
    [Test]
    public void PinPostAsyncThrowsExceptionWhenPostPinned()
    {
        Post post = new Post()
        {
            Id = Guid.NewGuid(),
            IsPinned = true,
        };

        postRepositoryMock
            .Setup(pr => pr.SingleOrDefaultAsync(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(post);

        permissionServiceMock
            .Setup(ps => ps.CanManagePostAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(true);

        PostService service = new PostService(postRepositoryMock.Object,
            boardRepositoryMock.Object,
            postTagRepositoryMock.Object,
            replyServiceMock.Object,
            tagServiceMock.Object,
            permissionServiceMock.Object);

        InvalidOperationException ex = Assert
            .ThrowsAsync<InvalidOperationException>(() => service.PinPostAsync(Guid.NewGuid(), post.Id));

        Assert.That(ex.Message, Does.Contain("Post is already pinned"));
    }
    [Test]
    public async Task PinPostAsyncWorksCorrectly()
    {
        Post post = new Post()
        {
            Id = Guid.NewGuid(),
            IsPinned = false,
        };

        postRepositoryMock
            .Setup(pr => pr.SingleOrDefaultAsync(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(post);

        permissionServiceMock
            .Setup(ps => ps.CanManagePostAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(true);

        PostService service = new PostService(postRepositoryMock.Object,
            boardRepositoryMock.Object,
            postTagRepositoryMock.Object,
            replyServiceMock.Object,
            tagServiceMock.Object,
            permissionServiceMock.Object);

        await service.PinPostAsync(Guid.NewGuid(), post.Id);

        Assert.That(post.IsPinned, Is.True);
    }

    //UnpinPostAsync
    [Test]
    public void UnpinPostAsyncThrowsExceptionWhenPostNotFound()
    {
        Post? post = null;

        postRepositoryMock
            .Setup(pr => pr.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(post);

        PostService service = new PostService(postRepositoryMock.Object,
            boardRepositoryMock.Object,
            postTagRepositoryMock.Object,
            replyServiceMock.Object,
            tagServiceMock.Object,
            permissionServiceMock.Object);

        ArgumentException ex = Assert
            .ThrowsAsync<ArgumentException>(() => service.UnpinPostAsync(Guid.NewGuid(), Guid.NewGuid()));

        Assert.That(ex.Message, Does.Contain("Id"));
    }
    [Test]
    public void UnpinPostAsyncThrowsExceptionWhenNoPermissions()
    {
        Post post = new Post();

        postRepositoryMock
            .Setup(pr => pr.SingleOrDefaultAsync(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(post);

        permissionServiceMock
            .Setup(ps => ps.CanManagePostAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(false);

        PostService service = new PostService(postRepositoryMock.Object,
            boardRepositoryMock.Object,
            postTagRepositoryMock.Object,
            replyServiceMock.Object,
            tagServiceMock.Object,
            permissionServiceMock.Object);

        UnauthorizedAccessException ex = Assert
            .ThrowsAsync<UnauthorizedAccessException>(() => service.UnpinPostAsync(Guid.NewGuid(), Guid.NewGuid()));

        Assert.That(ex.Message, Does.Contain("User does not have rigths to unpin post"));
    }
    [Test]
    public void UnpinPostAsyncThrowsExceptionWhenPostUnPinned()
    {
        Post post = new Post()
        {
            Id = Guid.NewGuid(),
            IsPinned = false,
        };

        postRepositoryMock
            .Setup(pr => pr.SingleOrDefaultAsync(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(post);

        permissionServiceMock
            .Setup(ps => ps.CanManagePostAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(true);

        PostService service = new PostService(postRepositoryMock.Object,
            boardRepositoryMock.Object,
            postTagRepositoryMock.Object,
            replyServiceMock.Object,
            tagServiceMock.Object,
            permissionServiceMock.Object);

        InvalidOperationException ex = Assert
            .ThrowsAsync<InvalidOperationException>(() => service.UnpinPostAsync(Guid.NewGuid(), post.Id));

        Assert.That(ex.Message, Does.Contain("Post is not pinned"));
    }
    [Test]
    public async Task UnpinPostAsyncWorksCorrectly()
    {
        Post post = new Post()
        {
            Id = Guid.NewGuid(),
            IsPinned = true,
        };

        postRepositoryMock
            .Setup(pr => pr.SingleOrDefaultAsync(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(post);

        permissionServiceMock
            .Setup(ps => ps.CanManagePostAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(true);

        PostService service = new PostService(postRepositoryMock.Object,
            boardRepositoryMock.Object,
            postTagRepositoryMock.Object,
            replyServiceMock.Object,
            tagServiceMock.Object,
            permissionServiceMock.Object);

        await service.UnpinPostAsync(Guid.NewGuid(), post.Id);

        Assert.That(post.IsPinned, Is.False);
    }

    //DeletePostAsync
    [Test]
    public void DeletePostAsyncThrowsExceptionWhenPostNotFound()
    {
        Post? post = null;

        postRepositoryMock
            .Setup(pr => pr.SingleOrDefaultAsync(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(post);

        PostService service = new PostService(postRepositoryMock.Object,
            boardRepositoryMock.Object,
            postTagRepositoryMock.Object,
            replyServiceMock.Object,
            tagServiceMock.Object,
            permissionServiceMock.Object);

        ArgumentException ex = Assert.ThrowsAsync<ArgumentException>(() =>
            service.DeletePostAsync(Guid.NewGuid(), new PostDeleteViewModel()));

        Assert.That(ex.Message, Does.Contain("Post not found"));
    }
    [Test]
    public void DeletePostAsyncThrowsExceptionWhenNoPermission()
    {
        Post? post = new Post();

        postRepositoryMock
            .Setup(pr => pr.SingleOrDefaultAsync(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(post);

        permissionServiceMock
            .Setup(ps => ps.CanManagePostAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(false);

        PostService service = new PostService(postRepositoryMock.Object,
            boardRepositoryMock.Object,
            postTagRepositoryMock.Object,
            replyServiceMock.Object,
            tagServiceMock.Object,
            permissionServiceMock.Object);

        UnauthorizedAccessException ex = Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
            service.DeletePostAsync(Guid.NewGuid(), new PostDeleteViewModel()));

        Assert.That(ex.Message, Is.EqualTo("User does not have permission to remove post"));
    }
    [Test]
    public async Task DeletePostAsyncWorksCorrectly()
    {
        Post? post = new Post()
        {
            Id = Guid.NewGuid(),
            IsDeleted = false,
        };

        postRepositoryMock
            .Setup(pr => pr.SingleOrDefaultAsync(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(post);

        permissionServiceMock
            .Setup(ps => ps.CanManagePostAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(true);

        PostService service = new PostService(postRepositoryMock.Object,
            boardRepositoryMock.Object,
            postTagRepositoryMock.Object,
            replyServiceMock.Object,
            tagServiceMock.Object,
            permissionServiceMock.Object);

        await service.DeletePostAsync(Guid.NewGuid(), new PostDeleteViewModel());

        Assert.IsTrue(post.IsDeleted);
    }

    //GetPostForDeleteAsync
    [Test]
    public void GetPostForDeleteAsyncThrowsExceptionWhenPostNotFound()
    {
        Post? post = null;

        postRepositoryMock
            .Setup(pr => pr.SingleOrDefaultWithIncludeAsync(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<Func<IQueryable<Post>, IQueryable<Post>>>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(post);

        PostService service = new PostService(postRepositoryMock.Object,
            boardRepositoryMock.Object,
            postTagRepositoryMock.Object,
            replyServiceMock.Object,
            tagServiceMock.Object,
            permissionServiceMock.Object);

        ArgumentException ex = Assert.ThrowsAsync<ArgumentException>(() =>
            service.GetPostForDeleteAsync(Guid.NewGuid(), Guid.NewGuid()));

        Assert.That(ex.Message, Does.Contain("Post not found"));
    }
    [Test]
    public void GetPostForDeleteAsyncThrowsExceptionWhenNoPermission()
    {
        Post? post = new Post();

        postRepositoryMock
            .Setup(pr => pr.SingleOrDefaultWithIncludeAsync(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<Func<IQueryable<Post>, IQueryable<Post>>>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(post);

        permissionServiceMock
            .Setup(ps => ps.CanManagePostAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(false);

        PostService service = new PostService(postRepositoryMock.Object,
            boardRepositoryMock.Object,
            postTagRepositoryMock.Object,
            replyServiceMock.Object,
            tagServiceMock.Object,
            permissionServiceMock.Object);

        UnauthorizedAccessException ex = Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
            service.GetPostForDeleteAsync(Guid.NewGuid(), Guid.NewGuid()));

        Assert.That(ex.Message, Is.EqualTo("User does not have permission to remove post"));
    }
    [Test]
    public async Task GetPostForDeleteAsyncWorksCorrectly()
    {
        Post post = CreateDummyPosts(1).First();

        post.IsDeleted = false;

        postRepositoryMock
            .Setup(pr => pr.SingleOrDefaultWithIncludeAsync(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<Func<IQueryable<Post>, IQueryable<Post>>>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(post);

        permissionServiceMock
            .Setup(ps => ps.CanManagePostAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(true);

        PostService service = new PostService(postRepositoryMock.Object,
            boardRepositoryMock.Object,
            postTagRepositoryMock.Object,
            replyServiceMock.Object,
            tagServiceMock.Object,
            permissionServiceMock.Object);

        var model = await service.GetPostForDeleteAsync(Guid.NewGuid(), post.Id);

        Assert.IsFalse(post.IsDeleted);

        Assert.That(model.Title, Is.EqualTo(post.Title));
        Assert.That(model.Content, Is.EqualTo(post.Content));
        Assert.That(model.Id, Is.EqualTo(post.Id));
        Assert.That(model.ImageUrl, Is.EqualTo(post.ImageUrl));
        Assert.That(model.BoardId, Is.EqualTo(post.BoardId));
    }

    //AddPostAsync
    [Test]
    public void AddPostAsyncThrowsExceptionWhenBoardNotFound()
    {
        boardRepositoryMock
            .Setup(br => br.AnyAsync(It.IsAny<Expression<Func<Board, bool>>>(),
                It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(false);

        PostService service = new PostService(postRepositoryMock.Object,
            boardRepositoryMock.Object,
            postTagRepositoryMock.Object,
            replyServiceMock.Object,
            tagServiceMock.Object,
            permissionServiceMock.Object);

        ArgumentException ex = Assert.ThrowsAsync<ArgumentException>(() =>
            service.AddPostAsync(Guid.NewGuid(), new PostCreateInputModel()));

        Assert.That(ex.Message, Does.Contain("Board not found"));
    }
    [Test]
    public void AddPostAsyncThrowsExceptionWhenImageUrlNotValid()
    {
        PostCreateInputModel model = new PostCreateInputModel();
        model.ImageUrl = "notValid";

        boardRepositoryMock
            .Setup(br => br.AnyAsync(It.IsAny<Expression<Func<Board, bool>>>(),
                It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(true);

        PostService service = new PostService(postRepositoryMock.Object,
            boardRepositoryMock.Object,
            postTagRepositoryMock.Object,
            replyServiceMock.Object,
            tagServiceMock.Object,
            permissionServiceMock.Object);

        ArgumentException ex = Assert.ThrowsAsync<ArgumentException>(() =>
            service.AddPostAsync(Guid.NewGuid(), model));

        Assert.That(ex.Message, Does.Contain("Image url is not valid"));
    }
    [Test]
    public async Task AddPostAsyncWorksCorrectly()
    {
        Post? returnedPost = null;
        Post post = CreateDummyPosts(1, Guid.NewGuid(), Guid.NewGuid()).First();
        PostCreateInputModel model = new PostCreateInputModel()
        {
            BoardName = "Test",
            BoardId = post.BoardId,
            Title = post.Title,
            Content = post.Content,
            ImageUrl = post.ImageUrl,
        };


        boardRepositoryMock
            .Setup(br => br.AnyAsync(It.IsAny<Expression<Func<Board, bool>>>(),
                It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(true);

        postRepositoryMock
            .Setup(br => br.AddAsync(It.IsAny<Post>()))
            .Callback<Post>(p => returnedPost = p)
            .Returns(Task.CompletedTask);

        PostService service = new PostService(postRepositoryMock.Object,
            boardRepositoryMock.Object,
            postTagRepositoryMock.Object,
            replyServiceMock.Object,
            tagServiceMock.Object,
            permissionServiceMock.Object);


        await service.AddPostAsync((Guid)post.ApplicationUserId!, model);

        Assert.IsNotNull(returnedPost);
        Assert.IsNotNull(post);

        Assert.That(returnedPost.Title, Is.EqualTo(post.Title));
        Assert.That(returnedPost.Content, Is.EqualTo(post.Content));
        Assert.That(returnedPost.ApplicationUserId, Is.EqualTo(post.ApplicationUserId));
        Assert.That(returnedPost.BoardId, Is.EqualTo(post.BoardId));
        Assert.That(returnedPost.ImageUrl, Is.EqualTo(post.ImageUrl));
    }

    //EditPostAsync
    [Test]
    public void EditPostAsyncThrowsExceptionWhenPostNotFound()
    {
        Post? post = null;

        postRepositoryMock
            .Setup(pr => pr.SingleOrDefaultWithIncludeAsync(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<Func<IQueryable<Post>, IQueryable<Post>>>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(post);

        PostService service = new PostService(postRepositoryMock.Object,
            boardRepositoryMock.Object,
            postTagRepositoryMock.Object,
            replyServiceMock.Object,
            tagServiceMock.Object,
            permissionServiceMock.Object);

        ArgumentException ex = Assert.ThrowsAsync<ArgumentException>(() =>
            service.EditPostAsync(Guid.NewGuid(), new PostEditInputModel()));

        Assert.That(ex.Message, Does.Contain("Post not found"));
    }
    [Test]
    public void EditPostAsyncThrowsExceptionWhenImageUrlNotValid()
    {
        Post? post = new Post();
        PostEditInputModel model = new PostEditInputModel();
        model.ImageUrl = "NotValid";

        postRepositoryMock
            .Setup(pr => pr.SingleOrDefaultWithIncludeAsync(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<Func<IQueryable<Post>, IQueryable<Post>>>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(post);

        PostService service = new PostService(postRepositoryMock.Object,
            boardRepositoryMock.Object,
            postTagRepositoryMock.Object,
            replyServiceMock.Object,
            tagServiceMock.Object,
            permissionServiceMock.Object);

        ArgumentException ex = Assert.ThrowsAsync<ArgumentException>(() =>
            service.EditPostAsync(Guid.NewGuid(), model));

        Assert.That(ex.Message, Does.Contain("Image url is not valid"));
    }
    [Test]
    public async Task EditPostAsyncWorksCorrectly()
    {
        Post post = CreateDummyPosts(1, Guid.NewGuid(), Guid.NewGuid()).First();
        PostEditInputModel model = new PostEditInputModel()
        {
            BoardId = post.BoardId,
            Title = "New title",
            Content = "New content",
            ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/7/7a/SpongeBob_SquarePants_character.png",
            Id = post.Id,
        };

        postRepositoryMock
            .Setup(pr => pr.SingleOrDefaultWithIncludeAsync(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<Func<IQueryable<Post>, IQueryable<Post>>>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(post);

        PostService service = new PostService(postRepositoryMock.Object,
            boardRepositoryMock.Object,
            postTagRepositoryMock.Object,
            replyServiceMock.Object,
            tagServiceMock.Object,
            permissionServiceMock.Object);


        await service.EditPostAsync((Guid)post.ApplicationUserId!, model);

        Assert.IsNotNull(post);

        Assert.That(model.Id, Is.EqualTo(post.Id));
        Assert.That(model.BoardId, Is.EqualTo(post.BoardId));

        Assert.That(model.Title, Is.EqualTo(post.Title));
        Assert.That(model.Content, Is.EqualTo(post.Content));
    }

    //GetPostDetails
    [Test]
    public void GetPostDetailsAsyncThrowsExceptionWhenPostNotFound()
    {
        const int index = 1;
        const int pagesize = 10;
        Post? post = null;

        postRepositoryMock
            .Setup(pr => pr.SingleOrDefaultWithIncludeAsync(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<Func<IQueryable<Post>, IQueryable<Post>>>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(post);

        PostService service = new PostService(postRepositoryMock.Object,
            boardRepositoryMock.Object,
            postTagRepositoryMock.Object,
            replyServiceMock.Object,
            tagServiceMock.Object,
            permissionServiceMock.Object);

        ArgumentException ex = Assert.ThrowsAsync<ArgumentException>(() =>
            service.GetPostDetailsAsync(Guid.NewGuid(), Guid.NewGuid(), ReplySort.ReplySortBy.Default, index, pagesize));

        Assert.That(ex.Message, Does.Contain("Post not found"));
    }
    [Test]
    public async Task GetPostDetailsAsyncWorksCorrectly()
    {
        const int index = 1;
        const int pagesize = 10;
        Post? post = CreateDummyPosts(1).First();
        CreateDummyTagsWithPostTags(post, 2);
        post.Replies = CreateDummyReplies(3, post.Id, post.Id);

        post.ApplicationUser!.DisplayName = "Test user";
        post.Board.Name = "Test board";

        postRepositoryMock
            .Setup(pr => pr.SingleOrDefaultWithIncludeAsync(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<Func<IQueryable<Post>, IQueryable<Post>>>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(post);

        IQueryable<ReplyForPostDetailViewModel> repliesQuariable = post
            .Replies
            .Select(r => new ReplyForPostDetailViewModel
            {
                Id = r.Id,
                Content = r.Content,
            })
            .ToArray()
            .BuildMock();

        replyServiceMock
            .Setup(rs => rs.GetRepliesForPostDetailsAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<bool>(),
            It.IsAny<ReplySort.ReplySortBy>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(await PaginatedResult<ReplyForPostDetailViewModel>
                .CreateAsync(repliesQuariable, index, pagesize));

        PostService service = new PostService(postRepositoryMock.Object,
            boardRepositoryMock.Object,
            postTagRepositoryMock.Object,
            replyServiceMock.Object,
            tagServiceMock.Object,
            permissionServiceMock.Object);

        PostDetailsViewModel model = await service
                .GetPostDetailsAsync(post.ApplicationUserId,
                    post.Id, ReplySort.ReplySortBy.Default, index, pagesize);

        Assert.That(model.Id, Is.EqualTo(post.Id));
        Assert.That(model.Title, Is.EqualTo(post.Title));
        Assert.That(model.Content, Is.EqualTo(post.Content));
        Assert.That(model.BoardId, Is.EqualTo(post.BoardId));
        Assert.That(model.IsPinned, Is.EqualTo(post.IsPinned));
        Assert.That(model.Tags!.Select(t => t.Id).ToArray(),
            Is.EqualTo(post.PostTags.Select(pt => pt.TagId).ToArray()));
        Assert.That(model.Replies.Items.Select(r => r.Id).ToList(),
            Is.EqualTo(post.Replies.Select(r => r.Id).ToList()));
    }

    //GetPostForEdit
    [Test]
    public void GetPostForEditAsyncThrowsExceptionWhenPostNotFound()
    {
        Post? post = null;

        postRepositoryMock
            .Setup(pr => pr.SingleOrDefaultWithIncludeAsync(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<Func<IQueryable<Post>, IQueryable<Post>>>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(post);

        PostService service = new PostService(postRepositoryMock.Object,
            boardRepositoryMock.Object,
            postTagRepositoryMock.Object,
            replyServiceMock.Object,
            tagServiceMock.Object,
            permissionServiceMock.Object);

        ArgumentException ex = Assert.ThrowsAsync<ArgumentException>(() =>
            service.GetPostForEditAsync(Guid.NewGuid(), Guid.NewGuid()));

        Assert.That(ex.Message, Does.Contain("Post not found"));
    }
    [Test]
    public async Task GetPostForEditAsyncWorksCorrectly()
    {
        Post? post = CreateDummyPosts(1).First();
        CreateDummyTagsWithPostTags(post, 2);

        post.ApplicationUser!.DisplayName = "Test user";
        post.Board.Name = "Test board";

        postRepositoryMock
            .Setup(pr => pr.SingleOrDefaultWithIncludeAsync(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<Func<IQueryable<Post>, IQueryable<Post>>>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(post);

        PostService service = new PostService(postRepositoryMock.Object,
            boardRepositoryMock.Object,
            postTagRepositoryMock.Object,
            replyServiceMock.Object,
            tagServiceMock.Object,
            permissionServiceMock.Object);

        PostEditInputModel model = await service
                .GetPostForEditAsync((Guid)post.ApplicationUserId!, post.Id);

        Assert.That(model.Id, Is.EqualTo(post.Id));
        Assert.That(model.Title, Is.EqualTo(post.Title));
        Assert.That(model.Content, Is.EqualTo(post.Content));
        Assert.That(model.BoardId, Is.EqualTo(post.BoardId));
        Assert.That(model.Tags!.Select(t => t.Id).ToArray(),
            Is.EqualTo(post.PostTags.Select(pt => pt.TagId).ToArray()));
    }

    private List<Post> CreateDummyPosts(int count = 3, Guid? boardId = null, Guid? userId = null)
    {
        var posts = new List<Post>();
        var fixedBoardId = boardId ?? Guid.NewGuid();
        var fixedUserId = userId ?? Guid.NewGuid();

        for (int i = 1; i <= count; i++)
        {
            posts.Add(new Post
            {
                Id = Guid.NewGuid(),
                Title = $"Test Post {i}",
                Content = $"This is the content of test post {i}.",
                ImageUrl = null,
                CreatedAt = DateTime.UtcNow.AddDays(-i),
                ModifiedAt = DateTime.UtcNow.AddDays(-i / 2.0),
                IsPinned = false,
                IsDeleted = false,
                BoardId = fixedBoardId,
                Board = new Board(),
                ApplicationUserId = fixedUserId,
                ApplicationUser = new ApplicationUser(),
                Replies = new List<Reply>(),
                PostTags = new List<PostTag>()
            });
        }

        return posts;
    }
    private List<Reply> CreateDummyReplies(int count = 2, Guid? postId = null, Guid? userId = null)
    {
        var replies = new List<Reply>();
        var fixedPostId = postId ?? Guid.NewGuid();
        var fixedUserId = userId ?? Guid.NewGuid();

        for (int i = 1; i <= count; i++)
        {
            replies.Add(new Reply
            {
                Id = Guid.NewGuid(),
                Content = $"This is reply {i} to post.",
                CreatedAt = DateTime.UtcNow.AddHours(-i),
                IsDeleted = false,
                PostId = fixedPostId,
                Post = new Post(),
                ApplicationUserId = fixedUserId,
                ApplicationUser = new ApplicationUser()
            });
        }

        return replies;
    }

    private (List<Tag> Tags, List<PostTag> PostTags) CreateDummyTagsWithPostTags(Post post, int tagCount = 2)
    {
        var tags = new List<Tag>();
        var postTags = new List<PostTag>();

        for (int i = 1; i <= tagCount; i++)
        {
            var tag = new Tag
            {
                Id = Guid.NewGuid(),
                Name = $"Tag{i}",
                ColorHex = "ffffff"
            };

            tags.Add(tag);

            postTags.Add(new PostTag
            {
                PostId = post.Id,
                Post = post,
                TagId = tag.Id,
                Tag = tag
            });
        }

        return (tags, postTags);
    }
}
