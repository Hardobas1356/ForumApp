using System.ComponentModel.DataAnnotations;

namespace ForumApp.Web.ViewModels.Post;
using static ForumApp.Data.Common.PostConstants;

public class EditPostViewModel
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = null!;

    [Required]
    public string Content { get; set; } = null!;
}
