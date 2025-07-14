using ForumApp.Web.ViewModels.Tag;

namespace ForumApp.Web.ViewModels.Post;

public class PostForBoardDetailsViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string CreatedAt { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string Handle { get; set; } = null!;
    public ICollection<TagViewModel> Tags { get; set; }
        = new HashSet<TagViewModel>();
}
