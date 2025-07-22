using ForumApp.Data.Models;
using ForumApp.GCommon;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Admin.ApplicationUser;
using ForumApp.Web.ViewModels.ApplicationUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using static ForumApp.GCommon.GlobalConstants;

namespace ForumApp.Services.Core;

public class ApplicationUserService : IApplicationUserService
{
    public const int MAX_SEARCH_RESULTS = 10;

    private IGenericRepository<Board> boardRepository;
    private UserManager<ApplicationUser> userManager;

    public ApplicationUserService(IGenericRepository<Board> boardRepository, UserManager<ApplicationUser> userManager)
    {
        this.boardRepository = boardRepository;
        this.userManager = userManager;
    }
    public async Task<ICollection<UserModeratorViewModel>?> SearchUsersByHandleFirstTenAsync(Guid boardId, string handle)
    {
        Board? board = await boardRepository
            .SingleOrDefaultWithIncludeAsync(
                b => b.Id == boardId,
                q => q.Include(b => b.BoardManagers),
                asNoTracking: true,
                ignoreQueryFilters: true);

        if (board == null)
        {
            return null;
        }

        HashSet<Guid> moderatorIds = board.BoardManagers
            .Where(m => m.IsDeleted == false)
            .Select(m => m.ApplicationUserId)
            .ToHashSet();

        ICollection<UserModeratorViewModel> users = await userManager
            .Users
            .Where(u => u.UserName != null
                        && u.UserName.Contains(handle))
            .Take(MAX_SEARCH_RESULTS)
            .Select(u => new UserModeratorViewModel
            {
                Id = u.Id,
                DisplayName = u.DisplayName ?? "(No Display Name)",
                UserName = u.UserName!,
                IsModerator = moderatorIds.Contains(u.Id)
            })
            .ToArrayAsync();

        return users;
    }
    public async Task<PaginatedResult<UserAdminViewModel>> GetAllUsersAdminAsync(int pageNumber, int pageSize)
    {
        IQueryable<UserAdminViewModel> users = userManager
            .Users
            .AsNoTracking()
            .IgnoreQueryFilters()
            .Select(u => new UserAdminViewModel
            {
                Id = u.Id,
                DisplayName = u.DisplayName,
                UserName = u.UserName!,
                Email = u.Email!,
                JoinDate = u.JoinDate.ToString(APPLICATION_DATE_TIME_FORMAT),
                IsDeleted = u.IsDeleted,
                IsModerator = u.BoardManagers.Any(),
            });

        return await PaginatedResult<UserAdminViewModel>
            .CreateAsync(users, pageNumber, pageSize);
    }
}
