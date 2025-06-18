namespace ForumApp.Web.ViewModels.Post;

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

}
