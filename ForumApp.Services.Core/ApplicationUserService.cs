using ForumApp.Data.Models;
using ForumApp.GCommon;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Admin.ApplicationUser;
using ForumApp.Web.ViewModels.ApplicationUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using static ForumApp.GCommon.Enums.SortEnums.User;
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
    public async Task<ICollection<UserModeratorViewModel>?> SearchUsersByHandleFirstTenAsync(
        Guid boardId, string handle)
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
    public async Task<PaginatedResult<UserAdminViewModel>> GetAllUsersAdminAsync(
        int pageNumber, int pageSize, string? searchTerm, UserSortBy sortOrder)
    {
        IQueryable<ApplicationUser> query = userManager
            .Users
            .AsNoTracking()
            .IgnoreQueryFilters();

        if (!String.IsNullOrWhiteSpace(searchTerm))
        {
            string loweredSearchTerm = searchTerm.ToLower();

            query = query
                .Where(u => u.NormalizedUserName.Contains(loweredSearchTerm)
                    || u.NormalizedEmail.Contains(loweredSearchTerm));
        }

        switch (sortOrder)
        {
            case UserSortBy.JoinDateAsc:
                query = query.OrderBy(u => u.JoinDate);
                break;
            case UserSortBy.JoinDateDesc:
                query = query.OrderByDescending(u => u.JoinDate);
                break;
            case UserSortBy.UsernameAsc:
                query = query.OrderBy(u => u.UserName);
                break;
            case UserSortBy.UsernameDesc:
                query = query.OrderByDescending(u => u.UserName);
                break;
            case UserSortBy.EmailAsc:
                query = query.OrderBy(u => u.Email);
                break;
            case UserSortBy.EmailDesc:
                query = query.OrderByDescending(u => u.Email);
                break;
            case UserSortBy.IsDeletedFirst:
                query = query.OrderByDescending(u => u.IsDeleted);
                break;
            case UserSortBy.IsModeratorFirst:
                query = query.OrderByDescending(u => u.BoardManagers.Any());
                break;
        }

        IQueryable<UserAdminViewModel> users = query
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

    public async Task SoftDeleteUserAsync(Guid id)
    {
        ApplicationUser user = await ValidateUserExists(id);

        if (user.IsDeleted == true)
        {
            throw new ArgumentException($"User with id {id} already deleted");
        }

        user.IsDeleted = true;

        await SaveChangesForUser(user);
    }

    public async Task RestoreUserAsync(Guid id)
    {
        ApplicationUser user = await ValidateUserExists(id);

        if (!user.IsDeleted)
        {
            throw new InvalidOperationException($"User not deleted");
        }

        user.IsDeleted = false;

        await SaveChangesForUser(user);
    }

    public async Task ChangeDisplayNameAsync(Guid id, string newDisplayName)
    {
        ApplicationUser user = await ValidateUserExists(id);

        if (String.IsNullOrWhiteSpace(newDisplayName))
        {
            throw new ArgumentException($"Provided display name null or white space {newDisplayName}");
        }

        user.DisplayName = newDisplayName.Trim();

        await SaveChangesForUser(user);
    }
    public async Task ChangeEmailAsync(Guid id, string newEmail)
    {
        ApplicationUser user = await ValidateUserExists(id);

        if (String.IsNullOrWhiteSpace(newEmail))
        {
            throw new ArgumentException($"Provided email null or white space {newEmail}");
        }

        newEmail = newEmail.Trim();

        ApplicationUser? userWithEmailExists = await userManager.FindByEmailAsync(newEmail);

        if (userWithEmailExists != null)
        {
            throw new ArgumentException(
                $"Provided email already used by user: {userWithEmailExists.UserName}, {newEmail}");
        }

        //SetEmailAsync updates user
        IdentityResult result = await userManager.SetEmailAsync(user, newEmail);

        if (!result.Succeeded)
        {
            throw new InvalidOperationException($"Failed to change email for user with id: {user.Id}");
        }
    }
    public async Task ChangeUsernameAsync(Guid id, string newUsername)
    {
        ApplicationUser user = await ValidateUserExists(id);

        if (String.IsNullOrWhiteSpace(newUsername))
        {
            throw new ArgumentException($"Provided username null or white space {newUsername}");
        }

        newUsername = newUsername.Trim().ToLower();

        ApplicationUser? userWithUsernameExists = await userManager.FindByNameAsync(newUsername);
        if (userWithUsernameExists != null && userWithUsernameExists.Id != user.Id)
        {
            throw new InvalidOperationException(
                $"Provided username already used by user: {userWithUsernameExists.UserName}, {userWithUsernameExists.Email}");
        }

        //SetUserName saved user
        IdentityResult result = await userManager.SetUserNameAsync(user, newUsername);

        if (!result.Succeeded)
        {
            throw new InvalidOperationException($"Failed to change username for user with id: {user.Id}");
        }
    }

    private async Task<ApplicationUser> ValidateUserExists(Guid id)
    {
        ApplicationUser? user = await userManager
            .FindByIdAsync(id.ToString());

        if (user == null)
        {
            throw new ArgumentException($"User with id {id} not found");
        }

        return user;
    }
    private async Task SaveChangesForUser(ApplicationUser user)
    {
        IdentityResult result = await userManager
            .UpdateAsync(user);

        if (!result.Succeeded)
        {
            throw new InvalidOperationException($"Failed to update data for user with id: {user.Id}");
        }
    }

}
