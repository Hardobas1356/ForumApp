using ForumApp.Data.Models;
using ForumApp.Services.Core;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Admin.ApplicationUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;

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
        var anotherUser = new ApplicationUser { Id = Guid.NewGuid(), UserName = "newname" };
        userManagerMock.Setup(um => um.FindByIdAsync(userId.ToString())).ReturnsAsync(testUser);
        userManagerMock.Setup(um => um.FindByNameAsync("newname")).ReturnsAsync(anotherUser);

        var model = new UserEditInputModel
        {
            Id = userId,
            UserName = "newname",
            DisplayName = "TestUser",
            Email = "test@example.com"
        };

        Assert.ThrowsAsync<InvalidOperationException>(() =>
            userService.EditUserAsync(model));
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

        var result = await userService.GetUserForEditAsync(userId);

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
}
