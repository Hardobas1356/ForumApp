namespace ForumApp.Web.ViewModels.Post;

using ForumApp.Web.ViewModels.Tag;
using System.ComponentModel.DataAnnotations;
using static ForumApp.GCommon.ValidationConstants.PostConstants;

public class PostCreateInputModel
{
    [Required]
    public Guid BoardId { get; set; }

    [Required]
    [MinLength(TitleMinimumLength)]
    [MaxLength(TitleMaximumLength)]
    public string Title { get; set; } = null!;
    [Required]
    [MinLength(ContentMinimumLength)]
    [MaxLength(ContentMaximumLength)]
    public string Content { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public ICollection<TagViewModel>? Tags { get; set; }
    = new HashSet<TagViewModel>();

    public ICollection<Guid>? TagIds { get; set; }
        = new HashSet<Guid>();

    public ICollection<TagViewModel> AvailableTags { get; set; }
        = new HashSet<TagViewModel>();
}
