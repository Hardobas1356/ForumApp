using System.ComponentModel.DataAnnotations;

namespace ForumApp.Web.ViewModels.Post;
using static ForumApp.Data.Common.PostConstants;

public class EditPostViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = TitleRequiredErrorMessage)]
    [MaxLength(TitleMaximumLength, ErrorMessage = TitleInvalidLengthErrorMessage)]
    [MinLength(TitleMinimumLength, ErrorMessage = TitleInvalidLengthErrorMessage)]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = ContentInvalidLengthErrorMessage)]
    [MaxLength(ContentMaximumLength, ErrorMessage = ContentInvalidLengthErrorMessage)]
    [MinLength(ContentMinimumLength, ErrorMessage = ContentInvalidLengthErrorMessage)]
    public string Content { get; set; } = null!;
}
