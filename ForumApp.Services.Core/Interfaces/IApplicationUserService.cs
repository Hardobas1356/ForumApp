using ForumApp.GCommon;
using ForumApp.Web.ViewModels.Admin.ApplicationUser;
using ForumApp.Web.ViewModels.ApplicationUser;

using static ForumApp.GCommon.Enums.SortEnums.User;

namespace ForumApp.Services.Core.Interfaces;

public interface IApplicationUserService
{
    public Task<ICollection<UserModeratorViewModel>?> SearchUsersByHandleFirstTenAsync(Guid boardId,
        string handle);
    public Task<PaginatedResult<UserAdminViewModel>> GetAllUsersAdminAsync(
        int pageNumber, int pageSize, string? searchTerm, UserSortBy sortOrder);
    public Task SoftDeleteUserAsync(Guid id);
    public Task RestoreUserAsync(Guid id);
    public Task ChangeDisplayNameAsync(Guid id,string newDisplayName);
}
