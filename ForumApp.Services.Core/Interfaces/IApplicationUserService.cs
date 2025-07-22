using ForumApp.Web.ViewModels.ApplicationUser;

namespace ForumApp.Services.Core.Interfaces;

public interface IApplicationUserService
{
    public Task<ICollection<UserModeratorViewModel>?> SearchUsersByHandleFirstTenAsync(Guid boardId, string handle);
}
