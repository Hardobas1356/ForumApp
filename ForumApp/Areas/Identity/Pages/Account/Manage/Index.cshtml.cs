#nullable disable

using ForumApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

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
    public string ImageUrl { get; set; }
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
        [Url]
        public string ImageUrl { get; set; }
    }

    private void LoadAsync(ApplicationUser user)
    {
        Username = user.UserName;
        DisplayName = user.DisplayName;
        ImageUrl = user.ImageUrl;

        Input = new InputModel
        {
            DisplayName = user.DisplayName,
            ImageUrl = user.ImageUrl
        };
    }

    public async Task<IActionResult> OnGetAsync()
    {
        ApplicationUser user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
        }

        LoadAsync(user);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        bool changed = false;

        ApplicationUser user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
        }

        if (!ModelState.IsValid)
        {
            LoadAsync(user);
            return Page();
        }

        if (!String.IsNullOrWhiteSpace(Input.ImageUrl))
        {
            if (!Uri.TryCreate(Input.ImageUrl, UriKind.RelativeOrAbsolute, out Uri validatedUrl))
            {
                ModelState.AddModelError(nameof(Input.ImageUrl), "Invalid image URL format.");
                LoadAsync(user);
                return Page();
            }

            if (!string.Equals(user.ImageUrl, Input.ImageUrl))
            {
                user.ImageUrl = Input.ImageUrl;
                changed = true;
            }
        }
        else if (!String.IsNullOrWhiteSpace(user.ImageUrl))
        {
            user.ImageUrl = null;
            changed = true;
        }

        if (!String.Equals(user.DisplayName, Input.DisplayName))
        {
            user.DisplayName = Input.DisplayName;
            changed = true;
        }

        if (changed)
        {
            IdentityResult updateResult = await userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                foreach (IdentityError error in updateResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                LoadAsync(user);
                return Page();
            }
        }

        await signInManager.RefreshSignInAsync(user);
        StatusMessage = "Your profile has been updated";

        return RedirectToPage();
    }
}
