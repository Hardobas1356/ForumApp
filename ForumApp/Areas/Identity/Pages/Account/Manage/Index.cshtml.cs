#nullable disable

using System.ComponentModel.DataAnnotations;
using ForumApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using static ForumApp.GCommon.ValidationConstants.ApplicationUserConstants;

namespace ForumApp.Web.Areas.Identity.Pages.Account.Manage;

public class IndexModel : PageModel
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly SignInManager<ApplicationUser> signInManager;

    public IndexModel(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
    }

    public string DisplayName { get; set; }
    public string Username { get; set; }
    [TempData]
    public string StatusMessage { get; set; }
    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        [Required]
        [MinLength(DisplayNameMinLength)]
        [MaxLength(DisplayNameMaxLength)]
        public string DisplayName { get; set; } = null!;
    }

    private async Task LoadAsync(ApplicationUser user)
    {
        string userName = await userManager.GetUserNameAsync(user);
        string displayName = user.DisplayName;

        Username = userName;
        DisplayName = displayName;

        Input = new InputModel
        {
            DisplayName = displayName
        };
    }

    public async Task<IActionResult> OnGetAsync()
    {
        ApplicationUser user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
        }

        await LoadAsync(user);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        ApplicationUser user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
        }

        if (!ModelState.IsValid)
        {
            await LoadAsync(user);
            return Page();
        }

        if (!String.Equals(user.DisplayName, Input.DisplayName))
        {
            user.DisplayName = Input.DisplayName;

            IdentityResult updateResult = await userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                foreach (IdentityError error in updateResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                await LoadAsync(user);
                return Page();
            }
        }

        await signInManager.RefreshSignInAsync(user);
        StatusMessage = "Your profile has been updated";
        return RedirectToPage();
    }
}
