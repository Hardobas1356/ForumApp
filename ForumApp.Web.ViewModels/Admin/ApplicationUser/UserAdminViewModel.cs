namespace ForumApp.Web.ViewModels.Admin.ApplicationUser;

public class UserAdminViewModel
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string JoinDate { get; set; } = null!;
    public bool IsDeleted { get; set; } = false;
    public bool IsModerator { get; set; } = false;
}
