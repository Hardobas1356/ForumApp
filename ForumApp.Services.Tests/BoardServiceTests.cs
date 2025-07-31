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
        List<Board> boards = new List<Board>();
        IQueryable<Board> mock = boards.BuildMock();

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
    [Test]
    public async Task GetAllBoardsAsyncReturnsCorrectResult()
    {
        List<Board> boards = new List<Board>()
        {
            new Board
            {
                Id = Guid.Parse("54bf50ab-5f31-4075-805a-cff51060e0f4"),
                Name = "General",
                Description = "Post your general",
                Posts = new List<Post>(),
                IsApproved = true,
                BoardCategories = new List<BoardCategory>(),
            },
             new Board
            {
                Id = Guid.Parse("98a91ec5-5739-408d-9246-66287779bd73"),
                Name = "Tech",
                Description = "Talk about tech",
                IsDeleted = false,
                IsApproved = true,
                Posts = new List<Post>()
            },
            new Board
            {
                Id = Guid.Parse("99756c1d-2354-4ae2-98cf-ac997c6cae32"),
                Name = "Lorem ipsum",
                Description = "dolor sit amet, consectetur",
                IsDeleted = false,
                IsApproved = true,
                Posts = new List<Post>()
            }
        };
        IQueryable<Board> mock = boards.BuildMock();

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
        Assert.That(result.Items.Count, Is.EqualTo(boards.Count));
        Assert.That(result.PageIndex, Is.EqualTo(1));
        Assert.That(result.PageSize, Is.EqualTo(10));
        Assert.That(result.TotalItems, Is.EqualTo(boards.Count));
        Assert.That(result.TotalPages, Is.EqualTo(1));
    }
    [Test]
    public async Task GetAllBoardsAsyncReturnsFilteredResultByNameSearchTerm()
    {
        List<Board> boards = new List<Board>()
        {
            new Board
            {
                Id = Guid.Parse("54bf50ab-5f31-4075-805a-cff51060e0f4"),
                Name = "General",
                Description = "Post your general",
                Posts = new List<Post>(),
                IsApproved = true,
                BoardCategories = new List<BoardCategory>(),
            },
             new Board
            {
                Id = Guid.Parse("98a91ec5-5739-408d-9246-66287779bd73"),
                Name = "Tech",
                Description = "Talk about tech",
                IsApproved = true,
                Posts = new List<Post>()
            },
            new Board
            {
                Id = Guid.Parse("99756c1d-2354-4ae2-98cf-ac997c6cae32"),
                Name = "Lorem ipsum",
                Description = "dolor sit amet, consectetur",
                IsApproved = true,
                Posts = new List<Post>()
            }
        };
        IQueryable<Board> mock = boards.BuildMock();

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
            = await service.GetAllBoardsAsync(null, BoardSort.BoardAllSortBy.None, "General", 1, 10);

        Assert.IsNotNull(result);
        Assert.That(result.Items.Count, Is.EqualTo(1));
        Assert.That(result.PageIndex, Is.EqualTo(1));
        Assert.That(result.PageSize, Is.EqualTo(10));
        Assert.That(result.TotalItems, Is.EqualTo(1));
        Assert.That(result.TotalPages, Is.EqualTo(1));
    }
    [Test]
    public async Task GetAllBoardsAsyncReturnsFilteredResultByCategorySearchTerm()
    {
        Category category = new Category()
        {
            Id = Guid.NewGuid(),
            Name = "TestCategory",
            ColorHex = "fffff"
        };

        List<Board> boards = new List<Board>()
        {
            new Board
            {
                Id = Guid.Parse("54bf50ab-5f31-4075-805a-cff51060e0f4"),
                Name = "General",
                Description = "Post your general",
                Posts = new List<Post>(),
                IsApproved = true,
                BoardCategories = new List<BoardCategory>()
                {
                    new BoardCategory
                    {
                        CategoryId = category.Id,
                        Category = category,
                        BoardId = Guid.Parse("54bf50ab-5f31-4075-805a-cff51060e0f4"),
                    }
                },
            }
        };
        IQueryable<Board> mock = boards.BuildMock();

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
            = await service.GetAllBoardsAsync(null, BoardSort.BoardAllSortBy.None, category.Name, 1, 10);

        Assert.IsNotNull(result);
        Assert.That(result.Items.Count, Is.EqualTo(1));
        Assert.That(result.PageIndex, Is.EqualTo(1));
        Assert.That(result.PageSize, Is.EqualTo(10));
        Assert.That(result.TotalItems, Is.EqualTo(1));
        Assert.That(result.TotalPages, Is.EqualTo(1));

        Assert.That(result.Items.First().Name, Is.EqualTo(boards.First().Name));
    }
    [Test]
    public async Task GetAllBoardsAsyncReturnsEmptyFilteredResultBySearchTerm()
    {
        List<Board> boards = new List<Board>()
        {
            new Board
            {
                Id = Guid.Parse("54bf50ab-5f31-4075-805a-cff51060e0f4"),
                Name = "General",
                Description = "Post your general",
                Posts = new List<Post>(),
                IsApproved = true,
                BoardCategories = new List<BoardCategory>(),
            },
             new Board
            {
                Id = Guid.Parse("98a91ec5-5739-408d-9246-66287779bd73"),
                Name = "Tech",
                Description = "Talk about tech",
                IsApproved = true,
                Posts = new List<Post>()
            },
            new Board
            {
                Id = Guid.Parse("99756c1d-2354-4ae2-98cf-ac997c6cae32"),
                Name = "Lorem ipsum",
                Description = "dolor sit amet, consectetur",
                IsApproved = true,
                Posts = new List<Post>()
            }
        };
        IQueryable<Board> mock = boards.BuildMock();

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
            = await service.GetAllBoardsAsync(null, BoardSort.BoardAllSortBy.None, "Extremely Long String", 1, 10);

        Assert.IsNotNull(result);
        Assert.That(result.Items.Count, Is.EqualTo(0));
        Assert.That(result.PageIndex, Is.EqualTo(1));
        Assert.That(result.PageSize, Is.EqualTo(10));
        Assert.That(result.TotalItems, Is.EqualTo(0));
        Assert.That(result.TotalPages, Is.EqualTo(0));
    }
    [Test]
    public async Task GetAllBoardsAsyncReturnsSortedResultByName()
    {
        List<Board> boards = new List<Board>()
        {
            new Board
            {
                Id = Guid.Parse("54bf50ab-5f31-4075-805a-cff51060e0f4"),
                Name = "General",
                Description = "Post your general",
                Posts = new List<Post>(),
                IsApproved = true,
                BoardCategories = new List<BoardCategory>(),
            },
             new Board
            {
                Id = Guid.Parse("98a91ec5-5739-408d-9246-66287779bd73"),
                Name = "Tech",
                Description = "Talk about tech",
                IsApproved = true,
                Posts = new List<Post>()
            },
            new Board
            {
                Id = Guid.Parse("99756c1d-2354-4ae2-98cf-ac997c6cae32"),
                Name = "Lorem ipsum",
                Description = "dolor sit amet, consectetur",
                IsApproved = true,
                Posts = new List<Post>()
            }
        };
        IQueryable<Board> mock = boards.BuildMock();

        boardRepositoryMock
            .Setup(br => br.GetQueryable(true, false))
            .Returns(mock);

        boards = boards.OrderBy(b => b.Name).ToList();

        BoardService service = new BoardService(
            boardManagerRepositoryMock.Object,
            boardRepositoryMock.Object,
            postServiceMock.Object,
            categoryServiceMock.Object,
            userManagerMock.Object);

        PaginatedResult<BoardAllIndexViewModel> result
            = await service.GetAllBoardsAsync(null, BoardSort.BoardAllSortBy.NameAscending, null, 1, 10);

        Assert.IsNotNull(result);
        Assert.That(result.Items.Count, Is.EqualTo(3));
        Assert.That(result.PageIndex, Is.EqualTo(1));
        Assert.That(result.PageSize, Is.EqualTo(10));
        Assert.That(result.TotalItems, Is.EqualTo(3));
        Assert.That(result.TotalPages, Is.EqualTo(1));

        Assert.That(result.Items.First().Name, Is.EqualTo(boards.First().Name));
        Assert.That(result.Items.Last().Name, Is.EqualTo(boards.Last().Name));
    }
    [Test]
    public async Task GetAllBoardsAsyncReturnsSortedResultByDateTime()
    {
        List<Board> boards = new List<Board>()
        {
            new Board
            {
                Id = Guid.Parse("54bf50ab-5f31-4075-805a-cff51060e0f4"),
                Name = "General",
                Description = "Post your general",
                Posts = new List<Post>(),
                CreatedAt =new DateTime(2025, 7, 31, 12, 0, 0),
                IsApproved = true,
                BoardCategories = new List<BoardCategory>(),
            },
             new Board
            {
                Id = Guid.Parse("98a91ec5-5739-408d-9246-66287779bd73"),
                Name = "Tech",
                Description = "Talk about tech",
                CreatedAt = new DateTime(2025, 8, 1, 9, 30, 45),
                IsApproved = true,
                Posts = new List<Post>()
            },
            new Board
            {
                Id = Guid.Parse("99756c1d-2354-4ae2-98cf-ac997c6cae32"),
                Name = "Lorem ipsum",
                Description = "dolor sit amet, consectetur",
                CreatedAt = new DateTime(2025, 8, 15, 18, 45, 30),
                IsApproved = true,
                Posts = new List<Post>()
            }
        };
        IQueryable<Board> mock = boards.BuildMock();

        boardRepositoryMock
            .Setup(br => br.GetQueryable(true, false))
            .Returns(mock);

        boards = boards.OrderBy(b => b.CreatedAt).ToList();

        BoardService service = new BoardService(
            boardManagerRepositoryMock.Object,
            boardRepositoryMock.Object,
            postServiceMock.Object,
            categoryServiceMock.Object,
            userManagerMock.Object);

        PaginatedResult<BoardAllIndexViewModel> result
            = await service.GetAllBoardsAsync(null, BoardSort.BoardAllSortBy.CreateTimeAscending, null, 1, 10);

        Assert.IsNotNull(result);
        Assert.That(result.Items.Count, Is.EqualTo(3));
        Assert.That(result.PageIndex, Is.EqualTo(1));
        Assert.That(result.PageSize, Is.EqualTo(10));
        Assert.That(result.TotalItems, Is.EqualTo(3));
        Assert.That(result.TotalPages, Is.EqualTo(1));

        Assert.That(result.Items.First().Name, Is.EqualTo(boards.First().Name));
        Assert.That(result.Items.Last().Name, Is.EqualTo(boards.Last().Name));
    }
    [Test]
    public async Task GetAllBoardsAsyncReturnsSortedResultByPopularity()
    {
        List<Board> boards = new List<Board>()
        {
            new Board
            {
                Id = Guid.Parse("54bf50ab-5f31-4075-805a-cff51060e0f4"),
                Name = "General",
                Description = "Post your general",
                Posts = new List<Post>()
                {
                    new Post(),
                    new Post(),
                },
                CreatedAt =new DateTime(2025, 7, 31, 12, 0, 0),
                IsApproved = true,
                BoardCategories = new List<BoardCategory>(),
            },
             new Board
            {
                Id = Guid.Parse("98a91ec5-5739-408d-9246-66287779bd73"),
                Name = "Tech",
                Posts = new List<Post>()
                {
                    new Post(),
                },
                Description = "Talk about tech",
                CreatedAt = new DateTime(2025, 8, 1, 9, 30, 45),
                IsApproved = true,
            },
            new Board
            {
                Id = Guid.Parse("99756c1d-2354-4ae2-98cf-ac997c6cae32"),
                Name = "Lorem ipsum",
                Posts = new List<Post>(),
                Description = "dolor sit amet, consectetur",
                CreatedAt = new DateTime(2025, 8, 15, 18, 45, 30),
                IsApproved = true,
            }
        };
        IQueryable<Board> mock = boards.BuildMock();

        boardRepositoryMock
            .Setup(br => br.GetQueryable(true, false))
            .Returns(mock);

        boards = boards.OrderByDescending(b => b.Posts.Count).ToList();

        BoardService service = new BoardService(
            boardManagerRepositoryMock.Object,
            boardRepositoryMock.Object,
            postServiceMock.Object,
            categoryServiceMock.Object,
            userManagerMock.Object);

        PaginatedResult<BoardAllIndexViewModel> result
            = await service.GetAllBoardsAsync(null, BoardSort.BoardAllSortBy.Popularity, null, 1, 10);

        Assert.IsNotNull(result);
        Assert.That(result.Items.Count, Is.EqualTo(3));
        Assert.That(result.PageIndex, Is.EqualTo(1));
        Assert.That(result.PageSize, Is.EqualTo(10));
        Assert.That(result.TotalItems, Is.EqualTo(3));
        Assert.That(result.TotalPages, Is.EqualTo(1));

        Assert.That(result.Items.First().Name, Is.EqualTo(boards.First().Name));
        Assert.That(result.Items.Last().Name, Is.EqualTo(boards.Last().Name));
    }
    [Test]
    public async Task GetAllBoardsAsyncReturnsResultWithIsModeratorTrue()
    {
        List<Board> boards = new List<Board>()
        {
            new Board
            {
                Id = Guid.Parse("54bf50ab-5f31-4075-805a-cff51060e0f4"),
                Name = "General",
                Description = "Post your general",
                Posts = new List<Post>(),
                CreatedAt = new DateTime(2025, 7, 31, 12, 0, 0),
                IsApproved = true,
                BoardCategories = new List<BoardCategory>(),
                BoardManagers = new List<BoardManager>()
                {
                    new BoardManager()
                    {
                        BoardId = Guid.Parse("54bf50ab-5f31-4075-805a-cff51060e0f4"),
                        ApplicationUserId = Guid.Parse("d2f0f6af-7546-496d-841d-3a6f44209c01")
                    }
                }
            }
        };
        IQueryable<Board> mock = boards.BuildMock();

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
            = await service.GetAllBoardsAsync(Guid.Parse("d2f0f6af-7546-496d-841d-3a6f44209c01"), BoardSort.BoardAllSortBy.None, null, 1, 10);

        Assert.IsNotNull(result);
        Assert.That(result.Items.Count, Is.EqualTo(1));
        Assert.That(result.PageIndex, Is.EqualTo(1));
        Assert.That(result.PageSize, Is.EqualTo(10));
        Assert.That(result.TotalItems, Is.EqualTo(1));
        Assert.That(result.TotalPages, Is.EqualTo(1));

        Assert.That(result.Items.First().Name, Is.EqualTo(boards.First().Name));
        Assert.That(result.Items.Last().Name, Is.EqualTo(boards.Last().Name));

        Assert.That(result.Items.First().IsModerator, Is.True);
    }
    [Test]
    public async Task GetAllBoardsAsyncReturnsResultWithIsModeratorFalse()
    {
        List<Board> boards = new List<Board>()
        {
            new Board
            {
                Id = Guid.Parse("54bf50ab-5f31-4075-805a-cff51060e0f4"),
                Name = "General",
                Description = "Post your general",
                Posts = new List<Post>(),
                CreatedAt = new DateTime(2025, 7, 31, 12, 0, 0),
                IsApproved = true,
                BoardCategories = new List<BoardCategory>(),
                BoardManagers = new List<BoardManager>()
                {
                    new BoardManager()
                    {
                        BoardId = Guid.Parse("54bf50ab-5f31-4075-805a-cff51060e0f4"),
                        ApplicationUserId = Guid.Parse("d2f0f6af-7546-496d-841d-3a6f44209c01")
                    }
                }
            }
        };
        IQueryable<Board> mock = boards.BuildMock();

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
            = await service.GetAllBoardsAsync(Guid.Parse("d1f0f6af-7546-496d-841d-3a6f44209c01"), BoardSort.BoardAllSortBy.None, null, 1, 10);

        Assert.IsNotNull(result);
        Assert.That(result.Items.Count, Is.EqualTo(1));
        Assert.That(result.PageIndex, Is.EqualTo(1));
        Assert.That(result.PageSize, Is.EqualTo(10));
        Assert.That(result.TotalItems, Is.EqualTo(1));
        Assert.That(result.TotalPages, Is.EqualTo(1));

        Assert.That(result.Items.First().Name, Is.EqualTo(boards.First().Name));
        Assert.That(result.Items.Last().Name, Is.EqualTo(boards.Last().Name));

        Assert.That(result.Items.First().IsModerator, Is.False);
    }
}
