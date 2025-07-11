namespace ForumApp.Web.ViewModels.Admin.Board;

public class BoardDeleteViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? ImageUrl { get; set; }
}
