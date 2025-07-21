using ForumApp.Data.Models;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.ApplicationUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ForumApp.Services.Core;

public class ApplicationUserService : IApplicationUserService
{
    private IGenericRepository<Board> boardRepository;
    private UserManager<ApplicationUser> userManager;

    public ApplicationUserService(IGenericRepository<Board> boardRepository, UserManager<ApplicationUser> userManager)
    {
        this.boardRepository = boardRepository;
        this.userManager = userManager;
    }

    public async Task<ICollection<UserModeratorViewModel>?> SearchUsersWithModeratorStatusAsync(Guid boardId, string handle)
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
}
