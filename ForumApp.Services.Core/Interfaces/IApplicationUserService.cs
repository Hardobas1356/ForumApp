using ForumApp.GCommon;
using ForumApp.Web.ViewModels.Admin.ApplicationUser;
using ForumApp.Web.ViewModels.ApplicationUser;

namespace ForumApp.Services.Core.Interfaces;

public interface IApplicationUserService
{
    public Task<ICollection<UserModeratorViewModel>?> SearchUsersByHandleFirstTenAsync(Guid boardId,
        string handle);
    public Task<PaginatedResult<UserAdminViewModel>> GetAllUsersAdminAsync(int pageNumber, int pageSize);
}
