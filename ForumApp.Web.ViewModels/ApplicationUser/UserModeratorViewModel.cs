namespace ForumApp.Web.ViewModels.ApplicationUser;

public class UserModeratorViewModel
{
    public Guid Id { get; set; }
    public string DisplayName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public bool IsModerator { get; set; }
}
