#nullable disable

using System.ComponentModel.DataAnnotations;
using ForumApp.Data.Models;
using ForumApp.Services.Core;
using ForumApp.Services.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ForumApp.Web.Areas.Identity.Pages.Account.Manage;

public class DeletePersonalDataModel : PageModel
{
    private readonly IGenericRepository<BoardManager> managerRepository;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly ILogger<DeletePersonalDataModel> logger;

    public DeletePersonalDataModel(IGenericRepository<BoardManager> managerRepository,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ILogger<DeletePersonalDataModel> logger)
    {
        this.managerRepository = managerRepository;
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.logger = logger;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
    public bool RequirePassword { get; set; }

    public async Task<IActionResult> OnGet()
    {
        ApplicationUser user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
        }

        RequirePassword = await userManager.HasPasswordAsync(user);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        ApplicationUser user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
        }

        RequirePassword = await userManager.HasPasswordAsync(user);
        if (RequirePassword)
        {
            if (!await userManager.CheckPasswordAsync(user, Input.Password))
            {
                ModelState.AddModelError(string.Empty, "Incorrect password.");
                return Page();
            }
        }

        IEnumerable<BoardManager> managerPositions = await managerRepository
            .GetWhereAsync(m => m.ApplicationUserId == user.Id);

        managerRepository.DeleteRange(managerPositions);
        await managerRepository.SaveChangesAsync();

        IdentityResult result = await userManager.DeleteAsync(user);
        string userId = await userManager.GetUserIdAsync(user);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException($"Unexpected error occurred deleting user.");
        }

        await managerRepository.SaveChangesAsync();
        await signInManager.SignOutAsync();

        logger.LogInformation("User with ID '{UserId}' deleted themselves.", userId);

        return Redirect("~/");
    }
}
