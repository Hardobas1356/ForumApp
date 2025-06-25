namespace ForumApp.Web.ViewModels.Board;

public class BoardAllIndexViewModel
{
    public Guid Id { get; set; }
    public string? ImageUrl { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}
