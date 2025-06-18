using System.ComponentModel.DataAnnotations;

namespace ForumApp.Web.ViewModels.Post;
using static ForumApp.GCommon.ValidationConstants.PostConstants;

public class EditPostInputModel
{
    public string Id { get; set; } = null!;

    [Required(ErrorMessage = TitleRequiredErrorMessage)]
    [MaxLength(TitleMaximumLength, ErrorMessage = TitleInvalidLengthErrorMessage)]
    [MinLength(TitleMinimumLength, ErrorMessage = TitleInvalidLengthErrorMessage)]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = ContentInvalidLengthErrorMessage)]
    [MaxLength(ContentMaximumLength, ErrorMessage = ContentInvalidLengthErrorMessage)]
    [MinLength(ContentMinimumLength, ErrorMessage = ContentInvalidLengthErrorMessage)]
    public string Content { get; set; } = null!;
}
