using ForumApp.Data.Models;
using ForumApp.GCommon;
using ForumApp.Services.Core;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Admin.Board;
using ForumApp.Web.ViewModels.Board;
using ForumApp.Web.ViewModels.Category;
using ForumApp.Web.ViewModels.Post;
using Microsoft.AspNetCore.Identity;
using MockQueryable;
using Moq;
using System.Linq.Expressions;
using static ForumApp.GCommon.Enums.FilterEnums;
using static ForumApp.GCommon.Enums.SortEnums;

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
            null!, // IOptions<IdentityOptions>
            null!, // IPasswordHasher<TUser>
            new IUserValidator<ApplicationUser>[0],
            new IPasswordValidator<ApplicationUser>[0],
            null!, // ILookupNormalizer
            null!, // IdentityErrorDescriber
            null!, // IServiceProvider
            null!  // ILogger<UserManager<TUser>>
        );
    }

    [Test]
    public void PassAlways()
    {
        Assert.Pass();
    }

    //GetAllBoardsAsync
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

    //GetAllBoardsForAdminAsync
    [Test]
    public async Task GetAllBoardsForAdminAsyncReturnResultWithNoItems()
    {
        List<Board> boards = new List<Board>();
        IQueryable<Board> mock = boards.BuildMock();

        boardRepositoryMock
            .Setup(br => br.GetQueryable(true, true))
            .Returns(mock);

        BoardService service = new BoardService(
            boardManagerRepositoryMock.Object,
            boardRepositoryMock.Object,
            postServiceMock.Object,
            categoryServiceMock.Object,
            userManagerMock.Object);

        PaginatedResult<BoardAdminViewModel> result
            = await service.GetAllBoardsForAdminAsync(BoardAdminFilter.All, BoardSort.BoardAllSortBy.None, null, 1, 10);

        Assert.IsNotNull(result);
        Assert.That(result.Items.Count, Is.EqualTo(0));
        Assert.That(result.PageIndex, Is.EqualTo(1));
        Assert.That(result.PageSize, Is.EqualTo(10));
        Assert.That(result.TotalItems, Is.EqualTo(0));
        Assert.That(result.TotalPages, Is.EqualTo(0));
    }
    [Test]
    public async Task GetAllBoardsForAdminAsyncReturnsCorrectResult()
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
                IsDeleted = true,
                IsApproved = true,
                Posts = new List<Post>()
            }
        };
        IQueryable<Board> mock = boards.BuildMock();

        boardRepositoryMock
            .Setup(br => br.GetQueryable(true, true))
            .Returns(mock);

        BoardService service = new BoardService(
            boardManagerRepositoryMock.Object,
            boardRepositoryMock.Object,
            postServiceMock.Object,
            categoryServiceMock.Object,
            userManagerMock.Object);

        PaginatedResult<BoardAdminViewModel> result
            = await service.GetAllBoardsForAdminAsync(BoardAdminFilter.All, BoardSort.BoardAllSortBy.None, null, 1, 10);

        Assert.IsNotNull(result);
        Assert.That(result.Items.Count, Is.EqualTo(boards.Count));
        Assert.That(result.PageIndex, Is.EqualTo(1));
        Assert.That(result.PageSize, Is.EqualTo(10));
        Assert.That(result.TotalItems, Is.EqualTo(boards.Count));
        Assert.That(result.TotalPages, Is.EqualTo(1));
    }
    [Test]
    public async Task GetAllBoardsForAdminAsyncReturnsCorrectResultFilteredByDeleted()
    {
        List<Board> boards = new List<Board>()
        {
            new Board
            {
                Id = Guid.Parse("54bf50ab-5f31-4075-805a-cff51060e0f4"),
                Name = "General",
                Description = "Post your general",
                Posts = new List<Post>(),
                IsDeleted = false,
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
                IsDeleted = true,
                IsApproved = true,
                Posts = new List<Post>()
            }
        };
        IQueryable<Board> mock = boards.BuildMock();

        boardRepositoryMock
            .Setup(br => br.GetQueryable(true, true))
            .Returns(mock);

        BoardService service = new BoardService(
            boardManagerRepositoryMock.Object,
            boardRepositoryMock.Object,
            postServiceMock.Object,
            categoryServiceMock.Object,
            userManagerMock.Object);

        PaginatedResult<BoardAdminViewModel> result
            = await service.GetAllBoardsForAdminAsync(BoardAdminFilter.Deleted, BoardSort.BoardAllSortBy.None, null, 1, 10);

        Assert.IsNotNull(result);
        Assert.That(result.Items.Count, Is.EqualTo(1));
        Assert.That(result.PageIndex, Is.EqualTo(1));
        Assert.That(result.PageSize, Is.EqualTo(10));
        Assert.That(result.TotalItems, Is.EqualTo(1));
        Assert.That(result.TotalPages, Is.EqualTo(1));
    }
    [Test]
    public async Task GetAllBoardsForAdminAsyncReturnsCorrectResultFilteredByApproved()
    {
        List<Board> boards = new List<Board>()
        {
            new Board
            {
                Id = Guid.Parse("54bf50ab-5f31-4075-805a-cff51060e0f4"),
                Name = "General",
                Description = "Post your general",
                Posts = new List<Post>(),
                IsDeleted = false,
                IsApproved = false,
                BoardCategories = new List<BoardCategory>(),
            },
             new Board
            {
                Id = Guid.Parse("98a91ec5-5739-408d-9246-66287779bd73"),
                Name = "Tech",
                Description = "Talk about tech",
                IsDeleted = false,
                IsApproved = false,
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
            .Setup(br => br.GetQueryable(true, true))
            .Returns(mock);

        BoardService service = new BoardService(
            boardManagerRepositoryMock.Object,
            boardRepositoryMock.Object,
            postServiceMock.Object,
            categoryServiceMock.Object,
            userManagerMock.Object);

        PaginatedResult<BoardAdminViewModel> result
            = await service.GetAllBoardsForAdminAsync(BoardAdminFilter.Approved, BoardSort.BoardAllSortBy.None, null, 1, 10);

        Assert.IsNotNull(result);
        Assert.That(result.Items.Count, Is.EqualTo(1));
        Assert.That(result.PageIndex, Is.EqualTo(1));
        Assert.That(result.PageSize, Is.EqualTo(10));
        Assert.That(result.TotalItems, Is.EqualTo(1));
        Assert.That(result.TotalPages, Is.EqualTo(1));

        Assert.That(result.Items.First().Name, Is.EqualTo(boards.First(b => b.IsApproved).Name));
    }
    [Test]
    public async Task GetAllBoardsForAdminAsyncReturnsCorrectResultSortedByName()
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
                IsDeleted = true,
                IsApproved = true,
                Posts = new List<Post>()
            }
        };
        IQueryable<Board> mock = boards.BuildMock();

        boardRepositoryMock
            .Setup(br => br.GetQueryable(true, true))
            .Returns(mock);

        boards = boards.OrderBy(b => b.Name).ToList();

        BoardService service = new BoardService(
            boardManagerRepositoryMock.Object,
            boardRepositoryMock.Object,
            postServiceMock.Object,
            categoryServiceMock.Object,
            userManagerMock.Object);

        PaginatedResult<BoardAdminViewModel> result
            = await service.GetAllBoardsForAdminAsync(BoardAdminFilter.All, BoardSort.BoardAllSortBy.NameAscending, null, 1, 10);

        Assert.IsNotNull(result);
        Assert.That(result.Items.Count, Is.EqualTo(boards.Count));
        Assert.That(result.PageIndex, Is.EqualTo(1));
        Assert.That(result.PageSize, Is.EqualTo(10));
        Assert.That(result.TotalItems, Is.EqualTo(boards.Count));
        Assert.That(result.TotalPages, Is.EqualTo(1));

        Assert.That(result.Items.First().Name, Is.EqualTo(boards.First().Name));
        Assert.That(result.Items.Last().Name, Is.EqualTo(boards.Last().Name));
    }
    [Test]
    public async Task GetAllBoardsForAdminAsyncReturnsCorrectResultSortedByPopularity()
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
                {
                    new Post(),
                }
            },
            new Board
            {
                Id = Guid.Parse("99756c1d-2354-4ae2-98cf-ac997c6cae32"),
                Name = "Lorem ipsum",
                Description = "dolor sit amet, consectetur",
                IsDeleted = true,
                IsApproved = true,
                Posts = new List<Post>()
            }
        };
        IQueryable<Board> mock = boards.BuildMock();

        boardRepositoryMock
            .Setup(br => br.GetQueryable(true, true))
            .Returns(mock);

        boards = boards.OrderByDescending(b => b.Posts.Count()).ToList();

        BoardService service = new BoardService(
            boardManagerRepositoryMock.Object,
            boardRepositoryMock.Object,
            postServiceMock.Object,
            categoryServiceMock.Object,
            userManagerMock.Object);

        PaginatedResult<BoardAdminViewModel> result
            = await service.GetAllBoardsForAdminAsync(BoardAdminFilter.All, BoardSort.BoardAllSortBy.Popularity, null, 1, 10);

        Assert.IsNotNull(result);
        Assert.That(result.Items.Count, Is.EqualTo(boards.Count));
        Assert.That(result.PageIndex, Is.EqualTo(1));
        Assert.That(result.PageSize, Is.EqualTo(10));
        Assert.That(result.TotalItems, Is.EqualTo(boards.Count));
        Assert.That(result.TotalPages, Is.EqualTo(1));

        Assert.That(result.Items.First().Name, Is.EqualTo(boards.First().Name));
        Assert.That(result.Items.Last().Name, Is.EqualTo(boards.Last().Name));
    }
    [Test]
    public async Task GetAllBoardsForAdminAsyncReturnsCorrectResultSortedByCreatedAt()
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
            .Setup(br => br.GetQueryable(true, true))
            .Returns(mock);

        boards = boards.OrderBy(b => b.CreatedAt).ToList();

        BoardService service = new BoardService(
            boardManagerRepositoryMock.Object,
            boardRepositoryMock.Object,
            postServiceMock.Object,
            categoryServiceMock.Object,
            userManagerMock.Object);

        PaginatedResult<BoardAdminViewModel> result
            = await service.GetAllBoardsForAdminAsync(BoardAdminFilter.All, BoardSort.BoardAllSortBy.CreateTimeAscending, null, 1, 10);

        Assert.IsNotNull(result);
        Assert.That(result.Items.Count, Is.EqualTo(boards.Count));
        Assert.That(result.PageIndex, Is.EqualTo(1));
        Assert.That(result.PageSize, Is.EqualTo(10));
        Assert.That(result.TotalItems, Is.EqualTo(boards.Count));
        Assert.That(result.TotalPages, Is.EqualTo(1));

        Assert.That(result.Items.First().Name, Is.EqualTo(boards.First().Name));
        Assert.That(result.Items.Last().Name, Is.EqualTo(boards.Last().Name));
    }
    [Test]
    public async Task GetAllBoardsForAdminAsyncReturnsCorrectResultSortedByCreatedAtDescending()
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
            .Setup(br => br.GetQueryable(true, true))
            .Returns(mock);

        boards = boards.OrderByDescending(b => b.CreatedAt).ToList();

        BoardService service = new BoardService(
            boardManagerRepositoryMock.Object,
            boardRepositoryMock.Object,
            postServiceMock.Object,
            categoryServiceMock.Object,
            userManagerMock.Object);

        PaginatedResult<BoardAdminViewModel> result
            = await service.GetAllBoardsForAdminAsync(BoardAdminFilter.All, BoardSort.BoardAllSortBy.CreateTimeDescending, null, 1, 10);

        Assert.IsNotNull(result);
        Assert.That(result.Items.Count, Is.EqualTo(boards.Count));
        Assert.That(result.PageIndex, Is.EqualTo(1));
        Assert.That(result.PageSize, Is.EqualTo(10));
        Assert.That(result.TotalItems, Is.EqualTo(boards.Count));
        Assert.That(result.TotalPages, Is.EqualTo(1));

        Assert.That(result.Items.First().Name, Is.EqualTo(boards.First().Name));
        Assert.That(result.Items.Last().Name, Is.EqualTo(boards.Last().Name));
    }

    //GetBoardDetailsAsync
    [Test]
    public async Task GetBoardDetailsAsyncReturnsCorrectResult()
    {
        List<PostForBoardDetailsViewModel> dummyPosts = CreateDummyPosts();
        List<CategoryViewModel> dummyCategories = CreateDummyCategories();
        Board dummyBoard = CreateDummyBoardWithPostsAndCategories(dummyPosts, dummyCategories);

        boardRepositoryMock
            .Setup(br => br.SingleOrDefaultAsync(It.IsAny<Expression<Func<Board, bool>>>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(dummyBoard);

        postServiceMock
            .Setup(ps => ps.GetPostsForBoardDetailsAsync(It.IsAny<Guid>(), It.IsAny<PostSort.PostSortBy>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new PaginatedResult<PostForBoardDetailsViewModel>(dummyPosts, dummyPosts.Count, 1, 10));

        categoryServiceMock
            .Setup(cs => cs.GetCategoriesAsyncByBoardId(It.IsAny<Guid>()))
            .ReturnsAsync(dummyCategories);

        BoardService service = new BoardService(
            boardManagerRepositoryMock.Object,
            boardRepositoryMock.Object,
            postServiceMock.Object,
            categoryServiceMock.Object,
            userManagerMock.Object);

        BoardDetailsViewModel result = await service
            .GetBoardDetailsAsync(dummyBoard.Id, PostSort.PostSortBy.Default, null, 1, 10);

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Posts);
        Assert.IsNotNull(result.Categories);

        Assert.That(result.Name, Is.EqualTo(dummyBoard.Name));
        Assert.That(result.Posts.Items.Count, Is.EqualTo(dummyPosts.Count));
        Assert.That(result.Categories.Count, Is.EqualTo(dummyCategories.Count));
    }
    [Test]
    public void GetBoardDetailsAsyncThrowsWhenBoardIdIsEmpty()
    {
        BoardService service = new BoardService(
            boardManagerRepositoryMock.Object,
            boardRepositoryMock.Object,
            postServiceMock.Object,
            categoryServiceMock.Object,
            userManagerMock.Object);

        var emptyGuid = Guid.Empty;

        var ex = Assert.ThrowsAsync<ArgumentException>(() =>
            service.GetBoardDetailsAsync(emptyGuid, PostSort.PostSortBy.Default, null, 1, 10));

        Assert.That(ex.ParamName, Is.EqualTo("boardId"));
        Assert.That(ex.Message, Does.Contain("BoardId is null"));
    }
    [Test]
    public void GetBoardDetailsAsyncThrowsWhenBoardNotFound()
    {
        Guid boardId = Guid.NewGuid();

        boardRepositoryMock
            .Setup(br => br.SingleOrDefaultAsync(It.IsAny<Expression<Func<Board, bool>>>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync((Board?)null);

        BoardService service = new BoardService(
            boardManagerRepositoryMock.Object,
            boardRepositoryMock.Object,
            postServiceMock.Object,
            categoryServiceMock.Object,
            userManagerMock.Object);

        var ex = Assert.ThrowsAsync<ArgumentException>(() =>
            service.GetBoardDetailsAsync(boardId, PostSort.PostSortBy.Default, null, 1, 10));

        Assert.That(ex.ParamName, Is.EqualTo("boardId"));
        Assert.That(ex.Message, Does.Contain("Board not found"));
    }

    //GetBoardDetailsAdminAsync
    [Test]
    public async Task GetBoardDetailsAdminAsyncReturnsCorrectResult()
    {
        List<PostForBoardDetailsViewModel> dummyPosts = CreateDummyPosts();
        List<CategoryViewModel> dummyCategories = CreateDummyCategories();
        Board dummyBoard = CreateDummyBoardWithPostsAndCategories(dummyPosts, dummyCategories);

        boardRepositoryMock
            .Setup(br => br.SingleOrDefaultWithIncludeAsync(It.IsAny<Expression<Func<Board, bool>>>(),
                It.IsAny<Func<IQueryable<Board>, IQueryable<Board>>>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(dummyBoard);

        postServiceMock
            .Setup(ps => ps.GetPostsForBoardDetailsAsync(It.IsAny<Guid>(), It.IsAny<PostSort.PostSortBy>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new PaginatedResult<PostForBoardDetailsViewModel>(dummyPosts, dummyPosts.Count, 1, 10));

        categoryServiceMock
            .Setup(cs => cs.GetCategoriesAsyncByBoardId(It.IsAny<Guid>()))
            .ReturnsAsync(dummyCategories);

        BoardService service = new BoardService(
            boardManagerRepositoryMock.Object,
            boardRepositoryMock.Object,
            postServiceMock.Object,
            categoryServiceMock.Object,
            userManagerMock.Object);

        BoardDetailsAdminViewModel result = await service
            .GetBoardDetailsAdminAsync(dummyBoard.Id, PostSort.PostSortBy.Default, 1, 10);

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Posts);
        Assert.IsNotNull(result.Categories);

        Assert.That(result.Name, Is.EqualTo(dummyBoard.Name));
        Assert.That(result.Posts.Items.Count, Is.EqualTo(dummyPosts.Count));
        Assert.That(result.Categories.Count, Is.EqualTo(dummyCategories.Count));
    }
    [Test]
    public void GetBoardDetailsAdminAsyncThrowsWhenBoardIdIsEmpty()
    {
        BoardService service = new BoardService(
            boardManagerRepositoryMock.Object,
            boardRepositoryMock.Object,
            postServiceMock.Object,
            categoryServiceMock.Object,
            userManagerMock.Object);

        var emptyGuid = Guid.Empty;

        var ex = Assert.ThrowsAsync<ArgumentException>(() =>
            service.GetBoardDetailsAdminAsync(emptyGuid, PostSort.PostSortBy.Default, 1, 10));

        Assert.That(ex.ParamName, Is.EqualTo("boardId"));
        Assert.That(ex.Message, Does.Contain("BoardId is null"));
    }
    [Test]
    public void GetBoardDetailsAdminAsyncThrowsWhenBoardNotFound()
    {
        Guid boardId = Guid.NewGuid();

        boardRepositoryMock
            .Setup(br => br.SingleOrDefaultWithIncludeAsync(It.IsAny<Expression<Func<Board, bool>>>(),
                It.IsAny<Func<IQueryable<Board>, IQueryable<Board>>>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync((Board?)null);

        BoardService service = new BoardService(
            boardManagerRepositoryMock.Object,
            boardRepositoryMock.Object,
            postServiceMock.Object,
            categoryServiceMock.Object,
            userManagerMock.Object);

        var ex = Assert.ThrowsAsync<ArgumentException>(() =>
            service.GetBoardDetailsAsync(boardId, PostSort.PostSortBy.Default, null, 1, 10));

        Assert.That(ex.ParamName, Is.EqualTo("boardId"));
        Assert.That(ex.Message, Does.Contain("Board not found"));
    }

    //GetBoardForDeletion
    [Test]
    public async Task GetBoardForDeletionReturnsCorrectBoard()
    {
        Board board = CreateDummyBoardWithPostsAndCategories(CreateDummyPosts(), CreateDummyCategories());

        boardRepositoryMock
            .Setup(br => br.GetByIdAsync(It.IsAny<Guid>(), true, false))
            .ReturnsAsync(board);

        BoardService service = new BoardService(
            boardManagerRepositoryMock.Object,
            boardRepositoryMock.Object,
            postServiceMock.Object,
            categoryServiceMock.Object,
            userManagerMock.Object);


        BoardDeleteViewModel result = await service.GetBoardForDeletionAsync(board.Id);

        Assert.IsNotNull(result);
        Assert.That(result.Name, Is.EqualTo(board.Name));
        Assert.That(result.Id, Is.EqualTo(board.Id));
    }
    [Test]
    public void GetBoardForDeletionThrowsExceptionWhenIdNull()
    {
        Board board = new Board();

        boardRepositoryMock
            .Setup(br => br.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(board);

        BoardService service = new BoardService(
            boardManagerRepositoryMock.Object,
            boardRepositoryMock.Object,
            postServiceMock.Object,
            categoryServiceMock.Object,
            userManagerMock.Object);

        var ex = Assert.ThrowsAsync<ArgumentException>(() =>
            service.GetBoardForDeletionAsync(Guid.Empty));

        Assert.That(ex.ParamName, Is.EqualTo("boardId"));
        Assert.That(ex.Message, Does.Contain("BoardId is null"));
    }
    [Test]
    public void GetBoardForDeletionThrowsExceptionWhenBoardNotFound()
    {
        boardRepositoryMock
            .Setup(br => br.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync((Board?)null);

        BoardService service = new BoardService(
            boardManagerRepositoryMock.Object,
            boardRepositoryMock.Object,
            postServiceMock.Object,
            categoryServiceMock.Object,
            userManagerMock.Object);

        var ex = Assert.ThrowsAsync<ArgumentException>(() =>
            service.GetBoardForDeletionAsync(Guid.NewGuid()));

        Assert.That(ex.ParamName, Is.EqualTo("boardId"));
        Assert.That(ex.Message, Does.Contain("Board not found with Id"));
    }

    //RestoreBoardAsync
    [Test]
    public async Task RestoreBoardAsyncWorksCorrectly()
    {
        Board board = CreateDummyBoardWithPostsAndCategories(CreateDummyPosts(), CreateDummyCategories());

        board.IsDeleted = true;

        boardRepositoryMock
            .Setup(br => br.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(board);

        BoardService service = new BoardService(
            boardManagerRepositoryMock.Object,
            boardRepositoryMock.Object,
            postServiceMock.Object,
            categoryServiceMock.Object,
            userManagerMock.Object);

        await service.RestoreBoardAsync(board.Id);

        Assert.IsFalse(board.IsDeleted);
    }
    [Test]
    public async Task RestoreBoardAsyncThrowsExceptionWhenBoardIsNotDeleted()
    {
        Board board = CreateDummyBoardWithPostsAndCategories(CreateDummyPosts(), CreateDummyCategories());
        board.IsDeleted = false;

        boardRepositoryMock
            .Setup(br => br.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(board);

        boardRepositoryMock
            .Setup(br => br.SaveChangesAsync())
            .ThrowsAsync(new Exception("DB Error"));

        BoardService service = new BoardService(
            boardManagerRepositoryMock.Object,
            boardRepositoryMock.Object,
            postServiceMock.Object,
            categoryServiceMock.Object,
            userManagerMock.Object);

        Exception? ex = Assert.ThrowsAsync<Exception>(() =>
            service.RestoreBoardAsync(board.Id));

        Assert.That(ex.Message, Is.EqualTo("Failed to restore board"));
    }
    [Test]
    public void RestoreBoardAsyncThrowsExceptionWhenIdNull()
    {
        Board board = new Board();

        boardRepositoryMock
            .Setup(br => br.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(board);

        BoardService service = new BoardService(
            boardManagerRepositoryMock.Object,
            boardRepositoryMock.Object,
            postServiceMock.Object,
            categoryServiceMock.Object,
            userManagerMock.Object);

        var ex = Assert.ThrowsAsync<ArgumentException>(() =>
            service.RestoreBoardAsync(Guid.Empty));

        Assert.That(ex.ParamName, Is.EqualTo("boardId"));
        Assert.That(ex.Message, Does.Contain("BoardId is null"));
    }
    [Test]
    public void RestoreBoardAsyncThrowsExceptionWhenBoardNotFound()
    {
        Board board = new Board();

        boardRepositoryMock
            .Setup(br => br.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(board);
        boardRepositoryMock
            .Setup(br => br.SaveChangesAsync())
            .ThrowsAsync(new Exception("DB Error"));

        BoardService service = new BoardService(
            boardManagerRepositoryMock.Object,
            boardRepositoryMock.Object,
            postServiceMock.Object,
            categoryServiceMock.Object,
            userManagerMock.Object);

        var ex = Assert.ThrowsAsync<ArgumentException>(() =>
            service.RestoreBoardAsync(Guid.NewGuid()));

        Assert.That(ex.ParamName, Is.EqualTo("boardId"));
        Assert.That(ex.Message, Does.Contain("Board not found with Id"));
    }

    //ApproveBoardAsync
    [Test]
    public async Task ApproveBoardAsyncWorksCorrectly()
    {
        Board board = CreateDummyBoardWithPostsAndCategories(CreateDummyPosts(), CreateDummyCategories());

        board.IsApproved = false;

        boardRepositoryMock
            .Setup(br => br.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(board);

        BoardService service = new BoardService(
            boardManagerRepositoryMock.Object,
            boardRepositoryMock.Object,
            postServiceMock.Object,
            categoryServiceMock.Object,
            userManagerMock.Object);

        await service.ApproveBoardAsync(board.Id);

        Assert.IsTrue(board.IsApproved);
    }
    [Test]
    public void ApproveBoardAsyncThrowsExceptionWhenIdNull()
    {
        Board board = new Board();

        boardRepositoryMock
            .Setup(br => br.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(board);

        BoardService service = new BoardService(
            boardManagerRepositoryMock.Object,
            boardRepositoryMock.Object,
            postServiceMock.Object,
            categoryServiceMock.Object,
            userManagerMock.Object);

        var ex = Assert.ThrowsAsync<ArgumentException>(() =>
            service.ApproveBoardAsync(Guid.Empty));

        Assert.That(ex.ParamName, Is.EqualTo("boardId"));
        Assert.That(ex.Message, Does.Contain("BoardId is null"));
    }
    [Test]
    public void ApproveBoardAsyncThrowsExceptionWhenBoardNotFound()
    {
        Board board = new Board();

        boardRepositoryMock
            .Setup(br => br.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(board);
        boardRepositoryMock
            .Setup(br => br.SaveChangesAsync())
            .ThrowsAsync(new Exception("DB Error"));

        BoardService service = new BoardService(
            boardManagerRepositoryMock.Object,
            boardRepositoryMock.Object,
            postServiceMock.Object,
            categoryServiceMock.Object,
            userManagerMock.Object);

        var ex = Assert.ThrowsAsync<ArgumentException>(() =>
            service.ApproveBoardAsync(Guid.NewGuid()));

        Assert.That(ex.ParamName, Is.EqualTo("boardId"));
        Assert.That(ex.Message, Does.Contain("Board not found"));
    }

    //SoftDeleteBoardAsync
    [Test]
    public async Task SoftDeleteBoardAsyncWorksCorrectly()
    {
        Board board = CreateDummyBoardWithPostsAndCategories(CreateDummyPosts(), CreateDummyCategories());

        board.IsDeleted = false;

        BoardDeleteViewModel dummyModel = new BoardDeleteViewModel
        {
            Id = board.Id,
            Name = board.Name,
            ImageUrl = board.ImageUrl
        };

        boardRepositoryMock
            .Setup(br => br.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(board);

        BoardService service = new BoardService(
            boardManagerRepositoryMock.Object,
            boardRepositoryMock.Object,
            postServiceMock.Object,
            categoryServiceMock.Object,
            userManagerMock.Object);

        await service.SoftDeleteBoardAsync(dummyModel);

        Assert.IsTrue(board.IsDeleted);
    }
    [Test]
    public void SoftDeleteBoardAsyncThrowsExceptionWhenBoardNotFound()
    {
        Board board = new Board();

        BoardDeleteViewModel dummyModel = new BoardDeleteViewModel
        {
            Id = Guid.NewGuid(),
            Name = "dummy",
            ImageUrl = board.ImageUrl
        };

        boardRepositoryMock
            .Setup(br => br.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(board);
        boardRepositoryMock
            .Setup(br => br.SaveChangesAsync())
            .ThrowsAsync(new Exception("DB Error"));

        BoardService service = new BoardService(
            boardManagerRepositoryMock.Object,
            boardRepositoryMock.Object,
            postServiceMock.Object,
            categoryServiceMock.Object,
            userManagerMock.Object);

        var ex = Assert.ThrowsAsync<ArgumentException>(() =>
            service.SoftDeleteBoardAsync(dummyModel));

        Assert.That(ex.ParamName, Is.EqualTo("Id"));
        Assert.That(ex.Message, Does.Contain("Board not found"));
    }

    private Board CreateDummyBoardWithPostsAndCategories(
        List<PostForBoardDetailsViewModel>? posts = null,
        List<CategoryViewModel>? categories = null)
    {
        posts ??= new List<PostForBoardDetailsViewModel>();
        categories ??= new List<CategoryViewModel>();

        var board = new Board
        {
            Id = Guid.Parse("54bf50ab-5f31-4075-805a-cff51060e0f4"),
            Name = "General",
            Description = "Post your general",
            CreatedAt = new DateTime(2025, 7, 31, 12, 0, 0),
            IsApproved = true,
            IsDeleted = false,
            BoardCategories = categories
                .Select(c => new BoardCategory
                {
                    CategoryId = c.Id,
                    Category = null!
                })
                .ToList(),
            Posts = posts
                .Select(p => new Post
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = "",
                    BoardId = Guid.Parse("54bf50ab-5f31-4075-805a-cff51060e0f4"),
                    CreatedAt = DateTime.UtcNow,
                    ModifiedAt = DateTime.UtcNow
                })
                .ToList(),
            BoardManagers = new List<BoardManager>(),
        };

        return board;
    }
    private List<PostForBoardDetailsViewModel> CreateDummyPosts(int count = 2)
    {
        return Enumerable.Range(1, count)
            .Select(i => new PostForBoardDetailsViewModel { Id = Guid.NewGuid(), Title = $"Post {i}" })
            .ToList();
    }
    private List<CategoryViewModel> CreateDummyCategories(int count = 2)
    {
        return Enumerable.Range(1, count)
            .Select(i => new CategoryViewModel
            {
                Id = Guid.NewGuid(),
                Name = $"Category {i}",
                ColorHex = i % 2 == 0 ? "#000000" : "#ffffff"
            }).ToList();
    }
}
