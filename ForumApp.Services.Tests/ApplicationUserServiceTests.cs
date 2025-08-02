using ForumApp.Data.Models;
using ForumApp.GCommon;
using ForumApp.Services.Core;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Admin.ApplicationUser;
using ForumApp.Web.ViewModels.ApplicationUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query;
using MockQueryable;
using Moq;
using System.Linq.Expressions;
using static ForumApp.GCommon.Enums.SortEnums.UserSort;

[TestFixture]
public class ApplicationUserServiceTests
{
    private Mock<IGenericRepository<Board>> boardRepositoryMock;
    private Mock<UserManager<ApplicationUser>> userManagerMock;
    private Mock<SignInManager<ApplicationUser>> signInManagerMock;
    private ApplicationUserService userService;

    private Guid userId = Guid.NewGuid();
    private ApplicationUser testUser;

    [SetUp]
    public void Setup()
    {
        boardRepositoryMock = new Mock<IGenericRepository<Board>>();

        userManagerMock = new Mock<UserManager<ApplicationUser>>(
            Mock.Of<IUserStore<ApplicationUser>>(),
            null!, null!, null!, null!, null!, null!, null!, null!);

        signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
            userManagerMock.Object,
            Mock.Of<IHttpContextAccessor>(),
            Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(),
            null!, null!, null!, null!);

        testUser = new ApplicationUser
        {
            Id = userId,
            DisplayName = "TestUser",
            Email = "test@example.com",
            UserName = "testuser",
            JoinDate = DateTime.UtcNow
        };

        userService = new ApplicationUserService(boardRepositoryMock.Object, userManagerMock.Object, signInManagerMock.Object);
    }

    //SoftDeleteUserAsync
    [Test]
    public async Task SoftDeleteUserAsyncMarksUserAsDeleted()
    {
        testUser.IsDeleted = false;
        userManagerMock.Setup(um => um.FindByIdAsync(userId.ToString())).ReturnsAsync(testUser);
        userManagerMock.Setup(um => um.UpdateAsync(testUser)).ReturnsAsync(IdentityResult.Success);

        await userService.SoftDeleteUserAsync(userId);

        Assert.That(testUser.IsDeleted, Is.True);
        userManagerMock.Verify(um => um.UpdateAsync(testUser), Times.Once);
    }
    [Test]
    public void SoftDeleteUserAsync_ThrowsIfAlreadyDeleted()
    {
        testUser.IsDeleted = true;
        userManagerMock.Setup(um => um.FindByIdAsync(userId.ToString())).ReturnsAsync(testUser);

        Assert.ThrowsAsync<ArgumentException>(() =>
            userService.SoftDeleteUserAsync(userId));
    }

    //RestoreUserAsync
    [Test]
    public async Task RestoreUserAsyncMarksUserAsNotDeleted()
    {
        testUser.IsDeleted = true;
        userManagerMock.Setup(um => um.FindByIdAsync(userId.ToString())).ReturnsAsync(testUser);
        userManagerMock.Setup(um => um.UpdateAsync(testUser)).ReturnsAsync(IdentityResult.Success);

        await userService.RestoreUserAsync(userId);

        Assert.That(testUser.IsDeleted, Is.False);
    }
    [Test]
    public void RestoreUserAsyncThrowsIfUserIsNotDeleted()
    {
        testUser.IsDeleted = false;
        userManagerMock.Setup(um => um.FindByIdAsync(userId.ToString())).ReturnsAsync(testUser);

        Assert.ThrowsAsync<InvalidOperationException>(() =>
            userService.RestoreUserAsync(userId));
    }

    //EditUserAsync
    [Test]
    public void EditUserAsyncThrowsIfUsernameTaken()
    {
        ApplicationUser anotherUser = new ApplicationUser { Id = Guid.NewGuid(), UserName = "newname" };
        userManagerMock.Setup(um => um.FindByIdAsync(userId.ToString())).ReturnsAsync(testUser);
        userManagerMock.Setup(um => um.FindByNameAsync("newname")).ReturnsAsync(anotherUser);

        UserEditInputModel model = new UserEditInputModel
        {
            Id = userId,
            UserName = "newname",
            DisplayName = "TestUser",
            Email = "test@example.com"
        };

        Assert.ThrowsAsync<InvalidOperationException>(() =>
            userService.EditUserAsync(model));
    }
    [Test]
    public async Task EditUserAsyncUpdatesDisplayNameAndEmail()
    {
        UserEditInputModel model = new UserEditInputModel
        {
            Id = userId,
            DisplayName = "NewDisplay",
            Email = "new@example.com",
            UserName = "testuser"
        };

        testUser.DisplayName = "OldDisplay";
        testUser.Email = "test@example.com";

        userManagerMock.Setup(um => um.FindByIdAsync(userId.ToString())).ReturnsAsync(testUser);
        userManagerMock.Setup(um => um.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser?)null);
        userManagerMock.Setup(um => um.FindByEmailAsync(model.Email)).ReturnsAsync((ApplicationUser?)null);
        userManagerMock.Setup(um => um.SetEmailAsync(testUser, model.Email)).ReturnsAsync(IdentityResult.Success);
        userManagerMock.Setup(um => um.UpdateAsync(testUser)).ReturnsAsync(IdentityResult.Success);

        await userService.EditUserAsync(model);

        Assert.That(testUser.DisplayName, Is.EqualTo("NewDisplay"));
    }
    [Test]
    public void EditUserAsyncThrowsIfEmailTaken()
    {
        UserEditInputModel model = new UserEditInputModel
        {
            Id = userId,
            Email = "duplicate@example.com",
            UserName = "testuser",
            DisplayName = "TestUser"
        };

        ApplicationUser otherUser = new ApplicationUser { Id = Guid.NewGuid(), Email = model.Email };

        userManagerMock.Setup(um => um.FindByIdAsync(userId.ToString())).ReturnsAsync(testUser);
        userManagerMock.Setup(um => um.FindByNameAsync(model.UserName)).ReturnsAsync((ApplicationUser?)null);
        userManagerMock.Setup(um => um.FindByEmailAsync(model.Email)).ReturnsAsync(otherUser);

        Assert.ThrowsAsync<InvalidOperationException>(() =>
            userService.EditUserAsync(model));
    }
    [Test]
    public void EditUserAsyncThrowsIfDisplayNameIsEmpty()
    {
        UserEditInputModel model = new UserEditInputModel
        {
            Id = userId,
            UserName = "testuser",
            DisplayName = "   ",
            Email = "test@example.com"
        };

        testUser.DisplayName = "OldName";

        userManagerMock.Setup(um => um.FindByIdAsync(userId.ToString())).ReturnsAsync(testUser);

        Assert.ThrowsAsync<ArgumentException>(() => userService.EditUserAsync(model));
    }

    //MakeAdminAsync
    [Test]
    public async Task MakeAdminAsyncAddsUserToRole()
    {
        userManagerMock.Setup(um => um.FindByIdAsync(userId.ToString())).ReturnsAsync(testUser);
        userManagerMock.Setup(um => um.AddToRoleAsync(testUser, "Admin")).ReturnsAsync(IdentityResult.Success);

        await userService.MakeAdminAsync(userId);

        userManagerMock.Verify(um => um.AddToRoleAsync(testUser, "Admin"), Times.Once);
    }

    //RemoveAdminAsync
    [Test]
    public async Task RemoveAdminAsyncRemovesUserFromRole()
    {
        userManagerMock.Setup(um => um.FindByIdAsync(userId.ToString())).ReturnsAsync(testUser);
        userManagerMock.Setup(um => um.RemoveFromRoleAsync(testUser, "Admin")).ReturnsAsync(IdentityResult.Success);

        await userService.RemoveAdminAsync(userId, Guid.NewGuid());

        userManagerMock.Verify(um => um.RemoveFromRoleAsync(testUser, "Admin"), Times.Once);
        signInManagerMock.Verify(sm => sm.RefreshSignInAsync(testUser), Times.Once);
    }

    //GetUserForEdit
    [Test]
    public async Task GetUserForEditAsyncReturnsCorrectModel()
    {
        testUser.DisplayName = "TestDisplay";
        testUser.Email = "test@example.com";
        testUser.UserName = "handle";

        userManagerMock.Setup(um => um.FindByIdAsync(userId.ToString()))
                       .ReturnsAsync(testUser);

        UserEditInputModel result = await userService.GetUserForEditAsync(userId);

        Assert.That(result.DisplayName, Is.EqualTo("TestDisplay"));
        Assert.That(result.Email, Is.EqualTo("test@example.com"));
        Assert.That(result.UserName, Is.EqualTo("handle"));
    }
    [Test]
    public void GetUserForEditAsyncThrowsWhenUserNotFound()
    {
        userManagerMock.Setup(um => um.FindByIdAsync(userId.ToString()))
                       .ReturnsAsync((ApplicationUser?)null);

        Assert.ThrowsAsync<ArgumentException>(() =>
            userService.GetUserForEditAsync(userId));
    }

    //SearchUserByHandleFirstTenAsync
    [Test]
    public async Task SearchUsersByHandleFirstTenAsyncReturnsMatchingWithModerators()
    {
        Guid boardId = Guid.NewGuid();

        List<ApplicationUser> users = new List<ApplicationUser>
        {
            new ApplicationUser { Id = Guid.NewGuid(), UserName = "alice", DisplayName = "Alice" },
            new ApplicationUser { Id = Guid.NewGuid(), UserName = "alicia", DisplayName = "Alicia" },
            new ApplicationUser { Id = Guid.NewGuid(), UserName = "bob", DisplayName = "Bob" }
        };

        IQueryable<ApplicationUser> usersMock = users.BuildMock();

        Board board = new Board
        {
            Id = boardId,
            BoardManagers = new List<BoardManager>
            {
                new BoardManager { ApplicationUserId = users[0].Id, IsDeleted = false }
            }
        };

        boardRepositoryMock
            .Setup(repo => repo.SingleOrDefaultWithIncludeAsync(
                It.IsAny<Expression<Func<Board, bool>>>(),
                It.IsAny<Func<IQueryable<Board>, IQueryable<Board>>>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()))
            .ReturnsAsync(board);

        userManagerMock
            .Setup(um => um.Users)
            .Returns(usersMock);

        ICollection<UserModeratorViewModel>? result
            = await userService.SearchUsersByHandleFirstTenAsync(boardId, "ali");

        Assert.That(result!.Count, Is.EqualTo(2));
        Assert.That(result.Any(u => u.UserName == "alice" && u.IsModerator));
        Assert.That(result.Any(u => u.UserName == "alicia" && !u.IsModerator));
    }
    [Test]
    public async Task SearchUsersByHandleFirstTenAsyncReturnsNullIfBoardNotFound()
    {
        boardRepositoryMock.Setup(repo => repo.SingleOrDefaultWithIncludeAsync(
            It.IsAny<Expression<Func<Board, bool>>>(),
            It.IsAny<Func<IQueryable<Board>, IIncludableQueryable<Board, object>>>(),
            true,
            true)).ReturnsAsync((Board?)null);

        ICollection<UserModeratorViewModel>? result
            = await userService.SearchUsersByHandleFirstTenAsync(Guid.NewGuid(), "anything");

        Assert.That(result, Is.Null);
    }

    //GetAllUsersAdminAsync
    [Test]
    public async Task GetAllUsersAdminAsync_ReturnsSortedByJoinDateAsc()
    {
        IQueryable<ApplicationUser> users = new List<ApplicationUser>
        {
            new ApplicationUser { Id = Guid.NewGuid(), UserName = "b", Email = "b@example.com", JoinDate = DateTime.UtcNow.AddDays(-1) },
            new ApplicationUser { Id = Guid.NewGuid(), UserName = "a", Email = "a@example.com", JoinDate = DateTime.UtcNow.AddDays(-2) }
        }
        .BuildMock();

        userManagerMock.Setup(um => um.Users).Returns(users);

        PaginatedResult<UserAdminViewModel> result
            = await userService.GetAllUsersAdminAsync(1, 10, null, UserSortBy.JoinDateAsc);

        Assert.That(result.Items.First().UserName, Is.EqualTo("a"));
    }
}
