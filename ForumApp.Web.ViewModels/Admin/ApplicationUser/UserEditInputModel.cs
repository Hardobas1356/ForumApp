using System.ComponentModel.DataAnnotations;

using static ForumApp.GCommon.ValidationConstants.ApplicationUserConstants;

namespace ForumApp.Web.ViewModels.Admin.ApplicationUser;

public class UserEditInputModel
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    [MaxLength(UserNameMaxLength)]
    [MinLength(UserNameMinLength)]
    public string UserName { get; set; } = null!;
    [Required]
    [MaxLength(DisplayNameMaxLength)]
    [MinLength(DisplayNameMinLength)]
    public string DisplayName { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;
    public bool IsAdmin { get; set; } = false;
}
