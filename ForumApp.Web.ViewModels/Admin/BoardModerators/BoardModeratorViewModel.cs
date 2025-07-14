namespace ForumApp.Web.ViewModels.Admin.BoardModerators;

public class BoardModeratorViewModel
{
    public Guid Id { get; set; }
    public string DisplayName { get; set; } = null!;
    public string Handle { get; set; } = null!;
}
