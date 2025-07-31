using ForumApp.Data.Models;
using ForumApp.Services.Core;
using ForumApp.Services.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Moq;
using MockQueryable.Moq;

using ForumApp.GCommon;
using ForumApp.Web.ViewModels.Board;
using static ForumApp.GCommon.Enums.SortEnums;
using MockQueryable;

namespace ForumApp.Services.Tests;

[TestFixture]
public class BoardServiceTests
{
    private Mock<IGenericRepository<BoardManager>> boardManagerRepositoryMock;
    private Mock<IGenericRepository<Board>> boardRepositoryMock;
    private Mock<IPostService> postServiceMock;
    private Mock<ICategoryService> categoryServiceMock;
    private Mock<UserManager<ApplicationUser>> userManagerMock;

    [SetUp]
    public void Setup()
    {
        boardManagerRepositoryMock = new Mock<IGenericRepository<BoardManager>>();
        boardRepositoryMock = new Mock<IGenericRepository<Board>>();
        postServiceMock = new Mock<IPostService>();
        categoryServiceMock = new Mock<ICategoryService>();

        var store = new Mock<IUserStore<ApplicationUser>>();
        userManagerMock = new Mock<UserManager<ApplicationUser>>(
            store.Object,
            null, // IOptions<IdentityOptions>
            null, // IPasswordHasher<TUser>
            new IUserValidator<ApplicationUser>[0],
            new IPasswordValidator<ApplicationUser>[0],
            null, // ILookupNormalizer
            null, // IdentityErrorDescriber
            null, // IServiceProvider
            null  // ILogger<UserManager<TUser>>
        );
    }

    [Test]
    public void PassAlways()
    {
        Assert.Pass();
    }

    [Test]
    public async Task GetAllBoardsAsyncReturnResultWithNoItems()
    {
        var boards = new List<Board>();
        var mock = boards.BuildMock();

        boardRepositoryMock
            .Setup(br => br.GetQueryable(true, false))
            .Returns(mock);

        BoardService service = new BoardService(
            boardManagerRepositoryMock.Object,
            boardRepositoryMock.Object,
            postServiceMock.Object,
            categoryServiceMock.Object,
            userManagerMock.Object);

        PaginatedResult<BoardAllIndexViewModel> result
            = await service.GetAllBoardsAsync(null, BoardSort.BoardAllSortBy.None, null, 1, 10);

        Assert.IsNotNull(result);
        Assert.That(result.Items.Count, Is.EqualTo(0));
        Assert.That(result.PageIndex, Is.EqualTo(1));
        Assert.That(result.PageSize, Is.EqualTo(10));
        Assert.That(result.TotalItems, Is.EqualTo(0));
        Assert.That(result.TotalPages, Is.EqualTo(0));
    }
}
