using ForumApp.Data.Models;
using ForumApp.GCommon;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Admin.ApplicationUser;
using ForumApp.Web.ViewModels.ApplicationUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

    public async Task<UserEditInputModel> GetUserForEditAsync(Guid id)
    {
        ApplicationUser user = await ValidateUserExists(id);

        UserEditInputModel model = new UserEditInputModel()
        {
            Id = id,
            Email = user.Email!,
            DisplayName = user.DisplayName,
            UserName = user.UserName!,
        };

        return model;
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
    public async Task EditUserAsync(UserEditInputModel model)
    {
        bool displayNameChanged = false;
        ApplicationUser user = await ValidateUserExists(model.Id);

        if (!string.Equals(model.UserName, user.UserName, StringComparison.OrdinalIgnoreCase))
        {
            ApplicationUser? existingUser = await userManager.FindByNameAsync(model.UserName);
            if (existingUser != null && existingUser.Id != user.Id)
            {
                throw new InvalidOperationException("Username already taken");
            }

            IdentityResult userNameResult = await userManager.SetUserNameAsync(user, model.UserName);
            if (!userNameResult.Succeeded)
            {
                throw new InvalidOperationException("Failed to update username");
            }
        }

        if (!string.Equals(model.DisplayName, user.DisplayName))
        {
            if (string.IsNullOrWhiteSpace(model.DisplayName))
            {
                throw new ArgumentException("DisplayName cannot be empty");
            }

            user.DisplayName = model.DisplayName;
            displayNameChanged = true;
        }

        if (!string.Equals(model.Email, user.Email, StringComparison.OrdinalIgnoreCase))
        {
            ApplicationUser? existingUserWithEmail = await userManager.FindByEmailAsync(model.Email);
            if (existingUserWithEmail != null && existingUserWithEmail.Id != user.Id)
            {
                throw new InvalidOperationException("Email already used");
            }

            IdentityResult emailResult = await userManager.SetEmailAsync(user, model.Email);
            if (!emailResult.Succeeded)
            {
                throw new InvalidOperationException("Failed to update email");
            }
        }

        if (displayNameChanged)
        {
            await SaveChangesForUser(user);
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
