using ForumApp.Web.ViewModels.Category;

namespace ForumApp.Web.ViewModels.Board;

public class BoardAllIndexViewModel
{
    public Guid Id { get; set; }
    public string? ImageUrl { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsModerator { get; set; }
    public ICollection<CategoryViewModel>? Categories { get; set; }
        = new HashSet<CategoryViewModel>();
}
